using Application.Common.ResultPattern;
using Application.Features.Profile.Commands.CreateProfile;
using Application.Features.Profile.Commands.DeleteProfileCommand;
using Application.Features.Profile.Commands.LockAccountCommand;
using Application.Features.Profile.Commands.UpdateProfileCommand;
using Application.Features.Profile.DTOS;
using Application.Resources.AuthenticationResources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace SocialApp.Controllers
{
    [Authorize]
    public class ProfileController(IMediator _mediator, IStringLocalizer<AuthRes> _AuthLocalizer) : BaseController
    {
        [HttpPost]
        public async Task <IActionResult> Post(ProfileDto profileDto)
        {
            var id = GetLoggedUserId();
            if (id is null)
                return Unauthorized();
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
                return Unauthorized(_AuthLocalizer[nameof(AuthRes.UnAuthenticatedUser)]);
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
                return Unauthorized(_AuthLocalizer[nameof(AuthRes.UnAuthenticatedUser)]);
            var deleteProfileCommand = new DeleteProfileCommand(id, userId.Value);
            Result result = await _mediator.Send(deleteProfileCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return NoContent();

        }

        [HttpPost]
        public async Task<IActionResult> LockUnLockAccount(int profileId)
        {
           
            var lockUnLockAccountCommand = new LockUnLockAccountCommand(profileId);
            Result result = await _mediator.Send(lockUnLockAccountCommand);
            if (!result.IsSuccess)
                return Problem(detail: result.Errors?.FirstOrDefault()?.Message ?? "", statusCode: StatusCodes.Status400BadRequest);
            return NoContent();

        }
    }
}
