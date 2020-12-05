using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Helpers;
using Mublog.Server.Infrastructure.Services.Interfaces;
using Mublog.Server.PublicApi.Common.DTOs;
using Mublog.Server.PublicApi.Common.DTOs.V1.Posts;
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
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public PostsController(IPostRepository postRepo, IMapper mapper, ICurrentUserService currentUserService)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts([FromQuery] QueryParameters queryParams = null)
        {
            queryParams ??= new QueryParameters();

            var posts = _postRepo.GetPaged(queryParams);

            var metaData = new
            {
                posts.TotalCount,
                posts.PageSize,
                posts.CurrentPage,
                posts.HasNext,
                posts.HasPrevious
            };

            var response = posts.Select(p => new PostResponseDto
                {
                    Id = p.PublicId,
                    TextContent = p.Content,
                    DatePosted = p.CreatedDate.ToUnixTimeStamp(),
                    DateEdited = p.CreatedDate.ToUnixTimeStamp(),
                    LikeAmount = 0 /*p.Likes.Count*/,
                    User = new PostUserResponseDto
                    {
                        Alias = p.Owner?.Username,
                        DisplayName = p.Owner?.DisplayName,
                        ProfileImageUrl = ""
                    }
                })
                .ToList();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(ResponseWrapper.Success<List<PostResponseDto>>(response));
        }

        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var post = await _postRepo.GetByPublicId(id);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Post with id {id} could not be found."));
            }
            
            var response = new PostResponseDto
            {
                Id = post.PublicId,
                TextContent = post.Content,
                DatePosted = post.CreatedDate.ToUnixTimeStamp(),
                DateEdited = post.UpdatedDate.ToUnixTimeStamp(),
                LikeAmount = 0 /*p.Likes.Count*/,
                User = new PostUserResponseDto
                {
                    Alias = post.Owner?.Username,
                    DisplayName = post.Owner?.DisplayName,
                    ProfileImageUrl = ""
                }
            };

            return Ok(ResponseWrapper.Success(response, "Success"));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateRequestDto request)
        {
            var post = _mapper.Map<Post>(request);
            post.Owner = await _currentUserService.GetProfile();
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
        public async Task<IActionResult> Update([FromBody] PostUpdateDto request, [FromRoute] int id)
        {
            if (request.Id != id)
            {
                return BadRequest(ResponseWrapper.Error($"ID {id} in route did not match ID {request.Id} in body"));
            }

            var post = await _postRepo.GetByPublicId(id);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Could not find post with ID {id}"));
            }

            if (post.Owner.Username != _currentUserService.GetUsername())
            {
                return Unauthorized(ResponseWrapper.Error("This post does not belong to you."));
            }
            
            post.Content = request.Content;
            post.PostEditedDate = DateTime.UtcNow;

            var success = await _postRepo.Update(post);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error($"An error occured pushing update to DB."));
            }
            
            var response = new PostResponseDto
            {
                Id = post.PublicId,
                TextContent = post.Content,
                DatePosted = post.CreatedDate.ToUnixTimeStamp(),
                DateEdited = post.UpdatedDate.ToUnixTimeStamp(),
                LikeAmount = 0 /*p.Likes.Count*/,
                User = new PostUserResponseDto
                {
                    Alias = post.Owner?.Username,
                    DisplayName = post.Owner?.DisplayName,
                    ProfileImageUrl = ""
                }
            };

            return Ok(ResponseWrapper.Success(response));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var post = await _postRepo.GetByPublicId(id);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Could not find post with ID {id}"));
            }
            
            if (post.Owner.Username != _currentUserService.GetUsername())
            {
                return Unauthorized(ResponseWrapper.Error("This post does not belong to you."));
            }
            
            var success = await _postRepo.Remove(post);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error($"An error occured pushing update to DB."));
            }

            return Ok(ResponseWrapper.Success("Successfully removed post."));
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