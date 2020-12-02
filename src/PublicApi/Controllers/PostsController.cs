using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Repositories;
using Newtonsoft.Json;

namespace Mublog.Server.PublicApi.Controllers
{
    /// <summary>
    ///     Handles all requests for posts in api v1
    /// </summary>
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/posts")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepo;

        public PostsController(IPostRepository postRepo)
        {
            _postRepo = postRepo;
        }
        
        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts([FromQuery] QueryParameters queryParams)
        {
            var posts = _postRepo.GetPaged(queryParams);

            var metaData = new
            {
                posts.TotalCount,
                posts.PageSize,
                posts.CurrentPage,
                posts.HasNext,
                posts.HasPrevious
            };
            
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            
            return Ok(new {Page = queryParams.Page, Size = queryParams.Size});
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> Get([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public Task<IActionResult> CreatePost([FromBody] object post)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> Update([FromRoute] int id, [FromBody] object dto)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> Delete([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("like/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> LikePost([FromRoute] int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("like/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public Task<IActionResult> RemoveLike([FromRoute] int id)
        {
            throw new NotImplementedException();
        }
    }
}