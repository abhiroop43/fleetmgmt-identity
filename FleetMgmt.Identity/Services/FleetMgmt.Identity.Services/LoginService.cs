using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Domain.Interfaces;
using FleetMgmt.Identity.Interfaces;

namespace FleetMgmt.Identity.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GenerateLongDurationToken(LoginRequestDto loginRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GenerateRefreshToken(LoginRequestDto loginRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<string> GenerateToken(LoginRequestDto loginRequest)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ServiceResponse> LoginUser(LoginRequestDto loginRequest)
        {
            var response = new ServiceResponse();

            return await Task.Run(() => response);
        }
    }
}