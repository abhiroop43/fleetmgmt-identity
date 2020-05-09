using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FleetMgmt.Identity.Domain.Dto;
using FleetMgmt.Identity.Infrastructure.Exceptions;
using FleetMgmt.Identity.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleetMgmt.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : IdentityBaseController<LoginController>
    {
        private readonly ILoginService _loginService;
        public LoginController(ILogger<LoginController> logger, ILoginService loginService) : base(logger)
        {
            _loginService = loginService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            ServiceResponse result = null;
            try
            {
                result = await _loginService.LoginUser(loginRequest);
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