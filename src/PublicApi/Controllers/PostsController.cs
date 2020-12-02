using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.PublicApi.Common.DTOs;
using Mublog.Server.PublicApi.Common.DTOs.V1.Posts;
using Newtonsoft.Json;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

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
        private readonly IMapper _mapper;

        public PostsController(IPostRepository postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts([FromQuery] QueryParameters queryParams = null)
        {
            // var posts = _postRepo.GetPaged(queryParams);
            //
            // var metaData = new
            // {
            //     posts.TotalCount,
            //     posts.PageSize,
            //     posts.CurrentPage,
            //     posts.HasNext,
            //     posts.HasPrevious
            // };
            //
            // Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));
            //
            // return Ok(new {Page = queryParams.Page, Size = queryParams.Size});

            var posts = new List<PostResponseDto>
            {
                new PostResponseDto
                {
                    Id = 1,
                    TextContent = "Test 1",
                    DateEdited = DateTime.Now,
                    DatePosted = DateTime.Now,
                    LikeAmount = 4,
                    User = new PostUserResponseDto
                    {
                        Alias = "testuser",
                        DisplayName = "Test User",
                        ProfileImageUrl = "#"
                    }

                },
                new PostResponseDto
                {
                    Id = 2,
                    TextContent = "#Test 2",
                    DateEdited = DateTime.UtcNow,
                    DatePosted = DateTime.UtcNow,
                    LikeAmount = 9999,
                    User = new PostUserResponseDto
                    {
                        Alias = "testuser",
                        DisplayName = "Test User",
                        ProfileImageUrl = "#"
                    }

                }
            };

            return Ok(ResponseWrapper.Success<List<PostResponseDto>>(posts));
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
        public async Task<IActionResult> CreatePost([FromBody] PostCreateRequestDto request)
        {
            var post = _mapper.Map<Post>(request);
            var success = await _postRepo.AddAsync(post);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error("Error adding post to database"));
            }

            return Ok(ResponseWrapper.Success("Successfully created post"));
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