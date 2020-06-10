using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Maintel.Icon.Portal.Sync.HighlightAPI.Models;
using Maintel.Icon.Portal.Sync.HighlightAPI.Helpers;
using Maintel.Icon.Portal.Sync.HighlightAPI.DataAccess;

namespace Maintel.Icon.Portal.Sync.HighlightAPI.Controllers.V1
{
    /// <summary>
    /// The main controller for the sites from the portal database 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WebHookController : ControllerBase
    {


        private HighlightAlertLayer _HighlightDB = new HighlightAlertLayer();

        /// <summary>
        /// Logger Instance
        /// </summary>
        ILogger<WebHookController> logger;

        /// <summary>
        /// Overloaded controller logging instance
        /// </summary>
        public WebHookController(ILogger<WebHookController> logger)  {
         	this.logger = logger; 
	    }

        /// <summary>
        /// Receives a Highlight webhook request
        /// </summary>
        /// <param name="alertDTO">The populated HighlightAlertDTO object to process</param>
        [HttpPost()]
        public ActionResult Post([FromBody] HighlightAlertDTO alertDTO) {
            logger.LogInformation("The Post call of the web hook controller");
            try {
                if(alertDTO != null) {
                    HighlightAlert highlightAlert = DTOConvert.GetHighlightAlertFromDTO(alertDTO);
                    logger.LogInformation("The Post call of the web hook controller contains alert: {alert}", JsonConvert.SerializeObject(highlightAlert));
                    var id = _HighlightDB.CreateEmailAlert(highlightAlert);
                    logger.LogInformation("Created new highlight alert with Id: {id}", id);
                    return StatusCode(204);
                } else {
                    return StatusCode(400);
                }
            }
            catch (System.Exception ex)
            {
                logger.LogCritical("The POST method of the web hook contoller produced the following error: {err}", ex.Message);
                throw ex;
            }
        } 
    
    }
}
