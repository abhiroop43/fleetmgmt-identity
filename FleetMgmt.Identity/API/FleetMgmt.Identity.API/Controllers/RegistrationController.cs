using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FleetMgmt.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : IdentityBaseController<RegistrationController>
    {
        public RegistrationController(ILogger<RegistrationController> logger) : base(logger)
        {
        }
    }
}