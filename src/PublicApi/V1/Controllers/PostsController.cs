using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Infrastructure.Common.Helpers;
using Mublog.Server.Infrastructure.Services.Interfaces;
using Mublog.Server.PublicApi.Common.Helpers;
using Mublog.Server.PublicApi.V1.DTOs.Posts;
using Newtonsoft.Json;

namespace Mublog.Server.PublicApi.V1.Controllers
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
        private readonly AutoMapper.IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public PostsController(
            IPostRepository postRepo,
            AutoMapper.IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPosts([FromQuery] PostQueryParameters queryParams = null)
        {
            var user = _currentUserService.Get();

            queryParams ??= new PostQueryParameters();

            var posts = await _postRepo.GetPaged(queryParams, user?.ToProfile);

            if (posts.Count == 0)
            {
                return NotFound(ResponseWrapper.Error("No posts were found."));
            }
            
            var metaData = new
            {
                posts.TotalCount,
                posts.PageSize,
                posts.CurrentPage,
                posts.HasNext,
                posts.HasPrevious
            };

            var response = posts.Select(p =>
                {
                    return new PostResponseDto
                    {
                        Id = p.PublicId,
                        TextContent = p.Content,
                        DatePosted = p.CreatedDate.ToUnixTimestamp(),
                        DateEdited = p.PostEditedDate.ToUnixTimestamp(),
                        LikeAmount = p.LikesCount,
                        Liked = user != null && p.Likes.Any(pfl => pfl?.Id == user.ProfileId),
                        User = new PostUserResponseDto
                        {
                            Alias = p.Owner?.Username ?? "unknown",
                            DisplayName = p.Owner?.DisplayName ?? "Unknown",
                            ProfileImageUrl = p.Owner?.ProfileImage?.PublicId.ToString() ?? ""
                        }
                    };
                })
                .ToList();

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metaData));

            return Ok(ResponseWrapper.Success(response));
        }

        [HttpGet("{id:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPublicId([FromRoute] int id)
        {
            var user = _currentUserService.Get();

            var post = await _postRepo.FindByPublicId(id, user?.ToProfile);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Post with id {id} could not be found."));
            }


            var response = new PostResponseDto
            {
                Id = post.PublicId,
                TextContent = post.Content,
                DatePosted = post.CreatedDate.ToUnixTimestamp(),
                DateEdited = post.UpdatedDate.ToUnixTimestamp(),
                LikeAmount = post.LikesCount,
                Liked = user != null && post.Likes.Any(p => p?.Id == user.ProfileId),
                User = new PostUserResponseDto
                {
                    Alias = post.Owner?.Username ?? "unknown",
                    DisplayName = post.Owner?.DisplayName ?? "Unknown",
                    ProfileImageUrl = ""
                }
            };

            return Ok(ResponseWrapper.Success(response));
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreatePost([FromBody] PostCreateRequestDto request)
        {
            var post = _mapper.Map<Post>(request);
            post.OwnerId = _currentUserService.Get().ProfileId;
            var id = await _postRepo.AddAsync(post);

            if (id == default)
            {
                return StatusCode(500, ResponseWrapper.Error("Error adding post to database."));
            }

            return Ok(ResponseWrapper.Success("Successfully created post."));
        }

        [HttpPatch("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] PostUpdateRequestDto request, [FromRoute] int id)
        {
            var user = _currentUserService.Get();

            if (request.Id != id)
            {
                return BadRequest(ResponseWrapper.Error($"ID {id} in route did not match ID {request.Id} in body"));
            }

            var post = await _postRepo.FindByPublicId(id);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Could not find post with ID {id}"));
            }

            if (post.Owner.Username != user.Username)
            {
                return Unauthorized(ResponseWrapper.Error("This post does not belong to you."));
            }

            post.Content = request.Content;

            var success = await _postRepo.ChangeContent(post);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error($"An error occured pushing update to DB."));
            }

            var response = new PostResponseDto
            {
                Id = post.PublicId,
                TextContent = post.Content,
                DatePosted = post.CreatedDate.ToUnixTimestamp(),
                DateEdited = post.UpdatedDate.ToUnixTimestamp(),
                LikeAmount = post.LikesCount,
                Liked = post.Likes.Any(p => p?.Id == user?.ProfileId),
                User = new PostUserResponseDto
                {
                    Alias = post.Owner?.Username ?? "unknown",
                    DisplayName = post.Owner?.DisplayName ?? "Unknown",
                    ProfileImageUrl = ""
                }
            };

            return Ok(ResponseWrapper.Success(response));
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var post = await _postRepo.FindByPublicId(id);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Could not find post with ID {id}"));
            }

            if (post.OwnerId != _currentUserService.Get().ProfileId)
            {
                return Unauthorized(ResponseWrapper.Error("This post does not belong to you."));
            }

            var success = await _postRepo.Remove(post);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error("An error occured pushing update to DB."));
            }

            return Ok(ResponseWrapper.Success("Successfully removed post."));
        }

        [HttpPost("like/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LikePost([FromRoute] int id)
        {
            var user = _currentUserService.Get();

            var post = await _postRepo.FindByPublicId(id, user.ToProfile);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Could not find post with ID {id}"));
            }

            if (post.Likes.Any(p => p.Id == user?.ProfileId))
            {
                return BadRequest(ResponseWrapper.Error("The post is already liked by you."));
            }

            var profile = _currentUserService.Get().ToProfile;

            var success = await _postRepo.AddLike(post, profile);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error("An error occured pushing update to DB."));
            }

            return Ok(ResponseWrapper.Success($"Added like to post {post.PublicId}"));
        }

        [HttpDelete("like/{id:int}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveLike([FromRoute] int id)
        {
            var user = _currentUserService.Get();

            var post = await _postRepo.FindByPublicId(id, _currentUserService.Get().ToProfile);

            if (post == null)
            {
                return NotFound(ResponseWrapper.Error($"Could not find post with ID {id}"));
            }

            if (post.Likes.Any(p => p.Id != user?.ProfileId))
            {
                return BadRequest(ResponseWrapper.Error("The post did not have a like from you."));
            }

            var success = await _postRepo.RemoveLike(post, user.ToProfile);

            if (!success)
            {
                return StatusCode(500, ResponseWrapper.Error("An error occured pushing update to DB."));
            }

            return Ok(ResponseWrapper.Success($"Removed like from post {post.PublicId}"));
        }
    }
}