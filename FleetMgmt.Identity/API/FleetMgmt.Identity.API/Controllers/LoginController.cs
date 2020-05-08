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
    public class LoginController : IdentityBaseController<LoginController>
    {
        public LoginController(ILogger<LoginController> logger) : base(logger)
        {
        }
    }
}