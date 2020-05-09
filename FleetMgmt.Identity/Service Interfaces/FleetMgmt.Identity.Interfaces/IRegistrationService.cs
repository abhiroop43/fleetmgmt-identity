using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;

namespace FleetMgmt.Identity.Interfaces
{
    public interface IRegistrationService
    {
        Task<ServiceResponse> UserRegistration(UserRegistrationRequestDto request);
    }
}