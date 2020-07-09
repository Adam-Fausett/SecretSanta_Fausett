using Microsoft.AspNetCore.Mvc;
using SecretSanta.Models;
using System.Collections.Generic;
using System.Web.Http.Description;

namespace SecretSanta.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretSantaController : ControllerBase
    {
        /// <summary>
        /// Draws the names.
        /// </summary>
        /// <returns>The names.</returns>
        /// <param name="participants">Participants.</param>
        [HttpPost]
        [Route("DrawNames")]
        [ResponseType(typeof(List<Models.SecretSanta>))]
        public IActionResult DrawNames([FromBody]Participant[][] participants)
        {
            return Ok(Core.SecretSanta.DrawNames(participants));
        }
    }
}
