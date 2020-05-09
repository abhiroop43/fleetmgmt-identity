using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;

namespace FleetMgmt.Identity.Interfaces
{
    public interface ILoginService
    {
        Task<ServiceResponse> LoginUser(LoginRequestDto loginRequest);
    }
}