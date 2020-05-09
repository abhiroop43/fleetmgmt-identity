using System;
using System.Reflection;
using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Infrastructure.Exceptions;
using FleetMgmt.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleetMgmt.Identity.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationController : IdentityBaseController<RegistrationController>
    {
        private readonly IRegistrationService _registrationService;
        public RegistrationController(ILogger<RegistrationController> logger, IRegistrationService registrationService) : base(logger)
        {
            _registrationService = registrationService;
        }

        [AllowAnonymous]
        [HttpPost]
        // [Route("RegisterUser")]
        public async Task<IActionResult> Post(UserRegistrationRequestDto request)
        {
            ServiceResponse result = null;
            try
            {
                result = await _registrationService.UserRegistration(request);
                return Ok(result);
            }
            catch (BadRequestException brEx)
            {
                result = new ServiceResponse { Msg = brEx.Message, Success = false };
                return BadRequest(result);
            }
            catch (NotFoundException nfEx)
            {
                result = new ServiceResponse { Msg = nfEx.Message, Success = false };
                return NotFound(result);
            }
            catch (Exception ex)
            {
                return HandleError(ex, MethodBase.GetCurrentMethod()?.Name);
            }
        }
    }
}