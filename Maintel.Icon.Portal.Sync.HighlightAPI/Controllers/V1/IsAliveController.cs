using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Controllers.V1
{
    /// <summary>
    /// The main controller for the is alive test REST call
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IsAliveController : ControllerBase
    {
        /// <summary>
        /// Logger Instance
        /// </summary>
        ILogger<IsAliveController> logger;

        /// <summary>
        /// Overloaded controller logging instance
        /// </summary>
        public IsAliveController(ILogger<IsAliveController> logger)  {
         	this.logger = logger; 
	    }

        /// <summary>
        /// Returns success
        /// </summary>
        /// <example>GET api/v1/isalive/</example>
        [HttpGet("")]
        public ActionResult Get()
        {            
            logger.LogWarning("The GET call of the isalive controller");
            return StatusCode(200);
        }
    }
}