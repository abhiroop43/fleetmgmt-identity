using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Domain.Entities;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Infrastructure.Exceptions;
using FleetMgmt.Identity.Interfaces;
using FleetMgmt.Identity.Services.Helper;

namespace FleetMgmt.Identity.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUsersGroupsRepository _usersGroupsRepository;
        private readonly IGroupsRepository _groupsRepository;
        private readonly ITransactionalUnitOfWork _transactionalUnitOfWork;
        private readonly EncryptData _encryptData;
        private readonly IMapper _mapper;

        public RegistrationService(IUserRepository userRepository,
            ITransactionalUnitOfWork transactionalUnitOfWork,
            IMapper mapper,
            IGroupsRepository groupsRepository,
            IUsersGroupsRepository usersGroupsRepository)
        {
            _userRepository = userRepository;
            _transactionalUnitOfWork = transactionalUnitOfWork;
            _encryptData = new EncryptData();
            _mapper = mapper;
            _groupsRepository = groupsRepository;
            _usersGroupsRepository = usersGroupsRepository;
        }

        public async Task<ServiceResponse> UserRegistration(UserRegistrationRequestDto request)
        {
            var response = new ServiceResponse {ErrorList = new List<ErrorMessage>()};

            request.FirstName = request.FirstName?.TitleCase();
            request.LastName = request.LastName?.TitleCase();
            request.UserName = request.UserName?.LowerCase();
            request.Password = _encryptData.EncryptPassword(request.Password);
            request.UserEmail = request.UserEmail?.LowerCase();
            request.Remarks = request.Remarks?.TitleCase();
            request.Active = true;
            var user = _mapper.Map<IM_USERS>(request);

            // to fix an issue related to claims generation during login
            if (string.IsNullOrEmpty(user.LASTNAME))
            {
                user.LASTNAME = "";
            }

            user.CreatedBy = "SYSTEM";
            user.CreatedDate = DateTime.Now;
            user.ID = Guid.NewGuid().ToString();

            _transactionalUnitOfWork.SetIsActive(false);
            
            await _transactionalUnitOfWork.CommitAsync();

            _userRepository.Add(user);

            var contributorGroup = await _groupsRepository.GetReadOnlyAsync(x => x.NAME == "CONTRIBUTORS");

            if (contributorGroup == null)
            {
                throw new BadRequestException("Unable to assign access to user");
            }
            
            var addUserGroup = new IM_USERS_GROUPS
            {
                ID = Guid.NewGuid().ToString(),
                CreatedBy = "ADMIN",
                CreatedDate = DateTime.Now,
                GROUP_ID = contributorGroup.ID,
                ACTIVE = true,
                USER_NAME = user.USERNAME
            };
            
            _transactionalUnitOfWork.SetIsActive(true);
            
            _usersGroupsRepository.Add(addUserGroup);

            var committedRows = await _transactionalUnitOfWork.CommitAsync();

            if (committedRows > 0)
            {
                response.Success = true;
                response.Msg = "User registered successfully";
            }
            else
            {
                response.Success = false;
                response.Msg = "Failed to register user";
            }

            return await Task.Run(() => response);
        }
    }
}