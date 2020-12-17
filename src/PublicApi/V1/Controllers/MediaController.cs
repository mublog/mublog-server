using System;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.PublicApi.Common.Helpers;

namespace Mublog.Server.PublicApi.V1.Controllers
{
    /// <summary>
    ///     Handles all requests for posts in api v1
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/media")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class MediaController : ControllerBase
    {

        [HttpPost, RequestSizeLimit(5242880)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Upload()
        {
            var guid = new Guid();
            
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("wwwroot", "media");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length <= 0) return BadRequest(ResponseWrapper.Error("Could not read file"));
                
                var fileName = guid.ToString();
                var fullPath = Path.Combine(pathToSave, fileName);

                await using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);

                return Ok(ResponseWrapper.Success(new {mediaId = fileName}));

            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper.Error(ex.Message));
            }
        }
        
    
        [HttpGet("{guid}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoByGuid([FromRoute] string guid)
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