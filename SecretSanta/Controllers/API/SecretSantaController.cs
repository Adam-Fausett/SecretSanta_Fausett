using Microsoft.AspNetCore.Mvc;
using SecretSanta.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace SecretSanta.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretSantaController : ControllerBase
    {
        /// <summary>
        /// Draws the names.
        /// </summary>
        /// <returns></returns>
        /// <param name="participants">Participants.</param>
        [HttpPost("DrawNames")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Models.SecretSanta>))]
        public IActionResult DrawNames([FromBody]Participant[][] participants)
        {
            return Ok(Core.SecretSanta.DrawNames(participants));
        }
    }
}
