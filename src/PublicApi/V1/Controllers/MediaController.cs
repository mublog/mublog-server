using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mublog.Server.Domain.Data.Entities;
using Mublog.Server.Domain.Data.Repositories;
using Mublog.Server.Domain.Enums;
using Mublog.Server.Infrastructure.Services.Interfaces;
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

        private readonly ICurrentUserService _currentUserService;
        private readonly IMediaRepository _mediaRepo;
        private readonly DirectoryInfo _mediaDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "media"));

        public MediaController(ICurrentUserService currentUserService, IMediaRepository mediaRepo)
        {
            _currentUserService = currentUserService;
            _mediaRepo = mediaRepo;

            if (!_mediaDirectory.Exists)
                _mediaDirectory.Create();
        }


        [HttpPost, RequestSizeLimit(5242880)]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (!_TryGetMediaType(file, out var mediaType))
                return StatusCode(422, ResponseWrapper.Error("This file type is not allowed"));

            if (file.Length <= 0)
                return BadRequest(ResponseWrapper.Error("Could not read file"));

            var guid = Guid.NewGuid();
            
            try
            {
                this._SaveFile(guid.ToString(), file);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResponseWrapper.Error(ex.Message));
            }


            var profile = await this._currentUserService.GetProfile();
            //todo: media isn't linked to a post atm
            var media = new Media { PublicId = guid, MediaType = mediaType, OwnerId = profile.Id, Owner = profile };
            var id = await this._mediaRepo.Create(media);

            if (id == default)
                return StatusCode(500, ResponseWrapper.Error("wrong id, dunno"));

            return Ok(ResponseWrapper.Success(new {guid = guid}));
        }


        /// <summary>
        /// Gets the mediaType of the file
        /// </summary>
        /// <param name="file">The File</param>
        /// <param name="mediaType">The MediaType of the file, if one was found</param>
        /// <returns>wether the file is of a support MediaType</returns>
        private static bool _TryGetMediaType(IFormFile file, out MediaType mediaType)
        {
            mediaType = MediaType.Jpg;
            var fileType = file.ContentType.ToLower();      
            var allowedMediaTypes = Enum.GetValues(typeof(MediaType)).Cast<MediaType>();
            //var allowedMediaTypes = Enum.GetNames(typeof(MediaType));

            foreach (var type in allowedMediaTypes)
                if (fileType.Contains(type.ToString().ToLower()))
                {
                    mediaType = type;
                    return true;
                }

            return false;
        }

        private async void _SaveFile(string name, IFormFile file)
        {
            var filePath = Path.Combine(this._mediaDirectory.FullName, name);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
        }

        private bool _TryGetFile(string name, out FileInfo fileInfo)
        {
            var filePath = Path.Combine(this._mediaDirectory.FullName, name);
            fileInfo = new FileInfo(filePath);

            return fileInfo.Exists;
        }

    
        //[HttpGet("{guid}")]
        [HttpGet("download")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInfoByGuid(Guid guid)
        {
            //if (!Guid.TryParse(guidString, out var guid))
            //    return StatusCode(500, ResponseWrapper.Error("wrong id, dunno"));

            //todo: doesnt search in repo atm
            //var media = await this._mediaRepo.FindByPublicId(guid);
            if (!this._TryGetFile(guid.ToString(), out var file))
                return StatusCode(500, ResponseWrapper.Error("File not found"));

            //todo: get correct filetype
            //var physicalFile = PhysicalFile(file.FullName, "png");
            //var data = file.OpenRead();
            var bytes = System.IO.File.ReadAllBytes(file.FullName);
            return Ok(ResponseWrapper.Success(File(bytes, "image/png")));
        }
        
        [HttpDelete("{guid}")]
        public async Task<IActionResult> DeleteImage([FromRoute] string guid)
        {
            throw new NotImplementedException();
        }
    }
}