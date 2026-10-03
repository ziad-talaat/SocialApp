using Application.Common.ResultPattern;
using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.DeletePostCommand;
using Application.Features.Post.Commands.UpdateCommand;
using Application.Features.Post.Dtos;
using Application.Features.Profile.Commands.CreateProfile;
using Application.Features.Profile.Commands.DeleteProfileCommand;
using Application.Features.Profile.Commands.UpdateProfileCommand;
using Application.Features.Profile.DTOS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SocialApp.Controllers
{
    [Authorize]
    public class PostController(IMediator _mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var createPostCommand = new CreatePostCommand(postDto);
            Result result = await _mediator.Send(createPostCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> Put(PostDto postDto)
        {
           
            var updatePostCommand = new UpdatePostCommand(postDto);
            Result result = await _mediator.Send(updatePostCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var deletePostCommand = new DeletePostCommand(id);
            Result result = await _mediator.Send(deletePostCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();
        }
        
    }
}
