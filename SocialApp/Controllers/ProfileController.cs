using Application.Common.ResultPattern;
using Application.Features.Profile.Commands.CreateProfile;
using Application.Features.Profile.Commands.DeleteProfileCommand;
using Application.Features.Profile.Commands.UpdateProfileCommand;
using Application.Features.Profile.DTOS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.Controllers
{
    [Authorize]
    public class ProfileController(IMediator _mediator) : BaseController
    {
        [HttpPost]
        public async Task <IActionResult> Post(ProfileDto profileDto)
        {
            var id = GetLoggedUserId();
            if (id is null)
                return Unauthorized("UnLogged in user");
            var createProfileCommand = new CreateProfileCommand(profileDto, id.Value);
           Result result= await  _mediator.Send(createProfileCommand);
            if(!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }

        [HttpPut]
        public async Task<IActionResult> Put(ProfileDto profileDto)
        {
            var id = GetLoggedUserId();
            if (id is null)
                return Unauthorized("UnLogged in user");
            var updateProfileCommand = new UpdateProfileCommand(profileDto,id.Value);
            Result result = await _mediator.Send(updateProfileCommand);
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
            var deleteProfileCommand = new DeleteProfileCommand(id, userId.Value);
            Result result = await _mediator.Send(deleteProfileCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }
    }
}
