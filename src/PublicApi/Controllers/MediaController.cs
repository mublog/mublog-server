using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mublog.Server.PublicApi.Controllers
{
    /// <summary>
    ///     Handles all requests for posts in api v1
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/media")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MediaController : ControllerBase
    {
        [HttpGet("{guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByGuid([FromRoute] string guid)
        {
            throw new NotImplementedException();
        }
        
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteImage([FromRoute] string guid)
        {
            throw new NotImplementedException();
        }
    }
}