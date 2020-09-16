using System;
using System.Collections.Generic;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using mublog_server.Api.Mock;

namespace mublog_server.Api.Controllers
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
            var respone = new List<Post>();
            
            respone.Add(new Post{Datetime = DateTime.UtcNow, Id = Guid.NewGuid(), text = "helloli", User = new User{Alias = "max", Name = "Max"}});
            respone.Add(new Post{Datetime = DateTime.UtcNow.AddMonths(1), Id = Guid.NewGuid(), text = "Mistakes were made.", User = new User{Alias = "bitsuki", Name = "Illya"}});
            
            return Ok(respone);
        }
    }
}    