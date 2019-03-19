using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SecretSanta.Models;

namespace SecretSanta.Controllers.API
{
    /// <summary>
    /// Secret santa controller.
    /// </summary>
    [RoutePrefix("api/SecretSanta")]
    public class SecretSantaController : ApiController
    {
        /// <summary>
        /// Draws the names.
        /// </summary>
        /// <returns>The names.</returns>
        /// <param name="participants">Participants.</param>
        [HttpPost]
        [Route("DrawNames")]
        [ResponseType(typeof(List<Models.SecretSanta>))]
        public IHttpActionResult DrawNames([FromBody]Participant[][] participants)
        {
            return Ok(Core.SecretSanta.DrawNames(participants));
        }
    }
}
