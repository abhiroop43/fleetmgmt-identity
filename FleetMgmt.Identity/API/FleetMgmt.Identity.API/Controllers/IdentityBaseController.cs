using System;
using FleetMgmt.Identity.Domain.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleetMgmt.Identity.API.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class IdentityBaseController<T> : ControllerBase
    {
        private readonly ILogger<T> _logger;
        public IdentityBaseController(ILogger<T> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleError(Exception ex, string methodName)
        {
            _logger.LogError(ex, $"Error in {methodName}");
            var result = new ServiceResponse { Msg = $"{ex.Message} InnerException: {ex.InnerException?.Message}", Success = false };
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}