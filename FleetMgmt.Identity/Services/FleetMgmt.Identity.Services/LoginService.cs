using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Interfaces;
using FleetMgmt.Identity.TokenGenerator;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Infrastructure.Exceptions;
using FleetMgmt.Identity.Services.Helper;

namespace FleetMgmt.Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenControllerRepository _tokenControllerRepository;
        private readonly IConfiguration _config;
        private readonly ITransactionalUnitOfWork _transactionalUnitOfWork;
        private readonly EncryptData _encryptData;
        private readonly IUsersGroupsRepository _usersGroupsRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly IGroupsOuRepository _groupsOuRepository;
        private readonly ICompanyRepository _companyRepository;

        public LoginService(IUserRepository userRepository,
            IConfiguration config,
            ITokenControllerRepository tokenControllerRepository,
            ITransactionalUnitOfWork transactionalUnitOfWork,
            IUsersGroupsRepository usersGroupsRepository,
            IGroupsRepository groupsRepository,
            IGroupsOuRepository groupsOuRepository,
            ICompanyRepository companyRepository)
        {
            _userRepository = userRepository;
            _config = config;
            _tokenControllerRepository = tokenControllerRepository;
            _transactionalUnitOfWork = transactionalUnitOfWork;
            _encryptData = new EncryptData();
            _usersGroupsRepository = usersGroupsRepository;
            _groupsRepository = groupsRepository;
            _groupsOuRepository = groupsOuRepository;
            _companyRepository = companyRepository;
        }
        
        public async Task<ServiceResponse> LoginUser(LoginRequestDto loginRequest)
        {
            var response = new ServiceResponse();

            var data = await UserLogin(loginRequest);
            var expiryMinutesVal = _config["SetExpiryTimeInterval:ExpiryInMinutes"];
            var expiryYearsVal = _config["SetLongExpiryTimeInterval:ExpiryInYears"];
            var isLongTokenExpiryChkEnabled = Convert.ToBoolean(_config["LongDurationRole:IsEnabled"]);
            var longTokenExpiryRole = _config["LongDurationRole:Role"];
            if (data.UserGroup != null && data.UserGroup.Contains(longTokenExpiryRole) &&
                isLongTokenExpiryChkEnabled == true)
            {
                var tokenVal = await GenerateLongDurationToken(data);

                var refreshTokenVal = await GenerateRefreshToken(data);

                data.Token = tokenVal;
                data.RefreshToken = refreshTokenVal;
                
                response.Data = data;
                response.Success = true;
                response.Msg = "User logged in successfully";

                return await Task.Run(() => response);
            }
            else
            {
                var tokenVal = await GenerateToken(data);

                var refreshTokenVal = await GenerateRefreshToken(data);
                
                data.Token = tokenVal;
                data.RefreshToken = refreshTokenVal;

                response.Data = data;
                response.Success = true;
                response.Msg = "User logged in successfully";

                return await Task.Run(() => response);
            }
        }

        /// <summary>
        /// Method added to provide long Token duration
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <returns></returns>
        private async Task<string> GenerateLongDurationToken(LoginResponseDto loginResponse)
        {
            var userName = loginResponse.UserName;
            var addExpiryYears = int.Parse(_config["SetLongExpiryTimeInterval:ExpiryInYears"]);
            // DateTime addLongExpiry = DateTime.Now.AddYears(addExpiryYears);

            var claims = GetSpecificAccountClaims(loginResponse, userName, addExpiryYears);

            var appSecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Audience:Secret"]));
            var token = new TokenBuilder()
                .AddSecurityKey(appSecretKey)
                .AddSubject(loginResponse.UserName)
                .AddSubject(userName)
                .AddIssuer(_config["Audience:Iss"])
                .AddAudience(_config["Audience:Aud"])
                .AddLongExpiry(
                    addExpiryYears) // expiry is in years now not in minutes aligned as in InternetServices project for 14 Days  60 * 24 * 14
                .AddClaims(claims)
                //.Build();
                .BuildForLongDuration(addExpiryYears);

            return await Task.Run(() => token.Value);
        }

        private async Task<string> GenerateRefreshToken(LoginResponseDto loginResponse)
        {
            var groupInfo = loginResponse.UserGroup;
            string[] group = loginResponse.UserGroup;
            var fullName = $"{loginResponse.FirstName} {loginResponse.LastName}";
            var appKey = _config["Audience:Secret"];

            var refreshTokenString = $"{loginResponse.UserName}|{fullName}|{appKey}";
            var md5Hash = EncryptData(refreshTokenString);
            return await Task.Run(() => md5Hash);
        }

        private async Task<string> GenerateToken(LoginResponseDto loginResponse)
        {
            var userName = loginResponse.UserName;
            var addExpiryMinutes = int.Parse(_config["SetExpiryTimeInterval:ExpiryInMinutes"]);


            var claims = GetClaims(loginResponse, userName, addExpiryMinutes);

            var appSecretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Audience:Secret"]));
            var token = new TokenBuilder()
                .AddSecurityKey(appSecretKey)
                .AddSubject(loginResponse.UserName)
                .AddSubject(userName)
                .AddIssuer(_config["Audience:Iss"])
                .AddAudience(_config["Audience:Aud"])
                .AddExpiry(addExpiryMinutes) // expiry is in minutes aligned as in InternetServices project for 14 Days  60 * 24 * 14
                .AddClaims(claims)
                .Build();
            // Below lines added to add user token information for further validation
            var userTokenData = new AddUserTokenControllerRequest
            {
                UserName = loginResponse.UserName, TokenValue = token.Value, CreatedBy = loginResponse.UserName
            };

            if (!string.IsNullOrEmpty(userTokenData.UserName))
                userTokenData.UserName = userTokenData.UserName.LowerCase();
            if (string.IsNullOrEmpty(userTokenData.TokenValue))
                throw new BadRequestException("Token not generated");

            var userTokenInfo = new IM_TOKENS_CONTROLLER
            {
                USER_NAME = userTokenData.UserName,
                VALUE = userTokenData.TokenValue,
                ISTOKENVALID = true,
                CreatedBy = userTokenData.CreatedBy,
                CreatedDate = DateTime.Now,
                ID = Guid.NewGuid().ToString()
            };

            _transactionalUnitOfWork.SetIsActive(true);

            _tokenControllerRepository.Add(userTokenInfo);

            await _transactionalUnitOfWork.CommitAsync();

            return token.Value;
        }

        /// <summary>
        /// Method added to serve User Login request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<LoginResponseDto> UserLogin(LoginRequestDto request)
        {
            var loginResponse = new LoginResponseDto();
            request.UserName = request.UserName.LowerCase();
            request.Password = _encryptData.EncryptPassword(request.Password);

            var getUser = await _userRepository.GetReadOnlyAsync(x =>
                x.ACTIVE == true &&
                x.USERNAME == request.UserName &&
                x.PASSWORD == request.Password);
            if (getUser == null)
                throw new BadRequestException("Either username or password you have entered is incorrect!");
            
            loginResponse.UserName = getUser.USERNAME;

            bool.TryParse(_config["AuthenticationMode:IsValidateOnlyLogin"], out var isValidateOnlyLogin);
            if (isValidateOnlyLogin)
            {
                loginResponse.IsUserAuthenticated = true;
                loginResponse.UserActive = getUser.ACTIVE;
                loginResponse.FirstName = getUser.FIRSTNAME;
                loginResponse.LastName = getUser.LASTNAME;
                loginResponse.Email = getUser.USEREMAIL;
                return await Task.Run(() => loginResponse);
            }

            var userGroups = new List<string>();

            var selectedGroupIds = _usersGroupsRepository
                .GetAllReadOnly(x => x.ACTIVE == true && x.USER_NAME == request.UserName).Select(x => x.GROUP_ID)
                .ToArray();

            foreach (var res in selectedGroupIds)
            {
                string groupName = (await _groupsRepository.GetReadOnlyAsync(x => x.ID == res)).NAME;
                userGroups.Add(groupName);
            }

            if (!userGroups.Any())
                throw new UnauthorizedAccessException("Not authorized to access");
            
            var orgUnitQuery = _groupsOuRepository.GetAllReadOnly(
                port => userGroups.Contains(port.Group.NAME) && port.ACTIVE, null, null, null, nameof(IM_GROUPS_OU.Group), nameof(IM_GROUPS_OU.Ou));
            
            var orgUnitList = orgUnitQuery.Select(p => new OUResponseDto {Code = p.Ou.CODE, Name = p.Ou.NAME})
                .Distinct().ToList();
            
            var userOrganization = _companyRepository
                .GetAllReadOnly(
                    item => item.ImOus.Any(a => orgUnitList.Select(b => b.Code).Contains(a.CODE)) &&
                            item.ACTIVE == true, null, null, null, nameof(IM_COMPANY.ImOus))
                .Select(a => new CompanyResponseDto {Name = a.NAME, Code = a.DOMAIN}).ToList();

            loginResponse.IsUserAuthenticated = userGroups.Any() ? true : false;
            loginResponse.UserGroup = userGroups.ToArray();
            loginResponse.UserActive = getUser.ACTIVE;
            loginResponse.FirstName = getUser.FIRSTNAME;
            loginResponse.LastName = getUser.LASTNAME;
            loginResponse.OrganizationUnits = orgUnitList;
            loginResponse.Email = getUser.USEREMAIL;
            loginResponse.Organizations = userOrganization;
            return await Task.Run(() => loginResponse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <param name="userName"></param>
        /// <param name="addExpiryInYears"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetSpecificAccountClaims(LoginResponseDto loginResponse,
            string userName,
            int addExpiryInYears)
        {
            Dictionary<string, string> claims = new Dictionary<string, string>();
            claims.Add("USERGROUP", string.Join(",", loginResponse.UserGroup ?? new List<string>().ToArray()));
            claims.Add("FIRSTNAME", loginResponse.FirstName);
            claims.Add("LASTNAME", loginResponse.LastName);
            claims.Add(ClaimTypes.Name, userName);
            claims.Add(ClaimTypes.Expired, DateTime.Now.AddYears(addExpiryInYears).ToString());
            claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            claims.Add("ORGANIZATIONUNITS",
                string.Join(",", loginResponse.OrganizationUnits?.Select(a => a.Code).ToList() ?? new List<string>()));
            claims.Add("ORGANIZATIONS",
                string.Join(",", loginResponse.Organizations?.Select(a => a.Code).ToList() ?? new List<string>()));
            claims.Add("EMAIL", loginResponse.Email);
            return claims;
        }

        /// <summary>
        /// GetCliams
        /// </summary>
        /// <param name="loginResponse"></param>
        /// <param name="userName"></param>
        /// <param name="addExpiryMinutes"></param>
        /// <returns></returns>
        private Dictionary<string, string> GetClaims(LoginResponseDto loginResponse,
            string userName,
            int addExpiryMinutes)
        {
            try
            {
                Dictionary<string, string> claims = new Dictionary<string, string>();
                claims.Add("USERGROUP", string.Join(",", loginResponse.UserGroup ?? new List<string>().ToArray()));
                claims.Add("FIRSTNAME", loginResponse.FirstName);
                claims.Add("LASTNAME", loginResponse.LastName);
                claims.Add(ClaimTypes.Name, userName);
                claims.Add(ClaimTypes.Expired, DateTime.Now.AddMinutes(addExpiryMinutes).ToString());
                claims.Add(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
                claims.Add("ORGANIZATIONUNITS",
                    string.Join(",",
                        loginResponse.OrganizationUnits?.Select(a => a.Code).ToList() ?? new List<string>()));
                claims.Add("ORGANIZATIONS",
                    string.Join(",", loginResponse.Organizations?.Select(a => a.Code).ToList() ?? new List<string>()));
                claims.Add("EMAIL", loginResponse.Email);
                return claims;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> EncryptData(string str)
        {
            try
            {
                string lowerStr = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
                System.Security.Cryptography.MD5CryptoServiceProvider x =
                    new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] bs = System.Text.Encoding.UTF8.GetBytes(lowerStr);
                bs = x.ComputeHash(bs);
                System.Text.StringBuilder s = new System.Text.StringBuilder();
                foreach (byte b in bs)
                {
                    s.Append(b.ToString("x2").ToLower());
                }

                return await Task.Run(() => s.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}