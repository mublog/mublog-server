using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mublog.Server.PublicApi.DTOs.V1.Responses;

namespace Mublog.Server.PublicApi.Controllers
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/posts")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class PostsController : ControllerBase
    {
        private readonly ILogger<PostsController> _logger;

        public PostsController(ILogger<PostsController> logger)
        {
            _logger = logger;
        }
        
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            var user = new PostUserResponseDto {Name = "Flayms", Alias = "Anton", ProfileImageUrl = ""};
            var posts = new List<PostResponseDto> {
                new PostResponseDto {Id = 1, TextContent = "This is my first Post!", DatePosted = 1605087318, DateEdited = 1605087318, LikeAmount = 2, User = user},
                new PostResponseDto {Id = 2, TextContent = "This is my second Post!", DatePosted = 1605087381, DateEdited = 1605087381, LikeAmount = 7, User = user}
                };
        }
    }
}    