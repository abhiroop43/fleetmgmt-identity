using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;

namespace FleetMgmt.Identity.Interfaces
{
    public interface ILoginService
    {
        Task<string> GenerateLongDurationToken(LoginRequestDto loginRequest);
        
        Task<string> GenerateRefreshToken(LoginRequestDto loginRequest);
        
        Task<string> GenerateToken(LoginRequestDto loginRequest);

        Task<ServiceResponse> LoginUser(LoginRequestDto loginRequest);
    }
}