using Application.Common.ResultPattern;
using Application.Features.Post.Commands.CreatePost;
using Application.Features.Post.Commands.Dtos;
using Application.Features.Post.Commands.UpdateCommand;
using Application.Features.Post.DeletePost;
using Application.Features.Profile.Commands.CreateProfile;
using Application.Features.Profile.Commands.DeleteProfileCommand;
using Application.Features.Profile.Commands.UpdateProfileCommand;
using Application.Features.Profile.DTOS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.Controllers
{
    [Authorize]
    public class PostController(IMediator _mediator) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto,int profileId)
        {
            var createPostCommand = new CreatePostCommand(postDto, profileId);
            Result result = await _mediator.Send(createPostCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> Put(PostDto postDto)
        {
            var id = GetLoggedUserId();
            if (id is null)
                return Unauthorized("UnLogged in user");
            var updatePostCommand = new UpdatePostCommand(postDto, id.Value);
            Result result = await _mediator.Send(updatePostCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetLoggedUserId();
            if (userId is null)
                return Unauthorized("UnLogged in user");
            var deletePostCommand = new DeletePostCommand(id, userId.Value);
            Result result = await _mediator.Send(deletePostCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }
    }
}
