using Application.Common.ResultPattern;
using Application.Features.Group.Commands.CreateGroupCommand;
using Application.Features.Group.Commands.DeleteGroupCommand;
using Application.Features.Group.Commands.UpdateGroupCommand;
using Application.Features.Group.DTOS;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupController(IMediator _mediator) : ControllerBase
    {
      

        [HttpPost]
        public async Task<IActionResult> Post(CreateGroupDto createGroupDto)
        {
            var createGroupCommand = new CreateGroupCommand(createGroupDto);
            var result = await _mediator.Send<Result>(createGroupCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int croupId)
        {
            var deleteGroupCommand = new DeleteGroupCommand(croupId);
            var result = await _mediator.Send<Result>(deleteGroupCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CreateGroupDto createGroupDto)
        {
            var updateGroupCommand = new UpdateGroupCommand(createGroupDto);
            var result = await _mediator.Send<Result>(updateGroupCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }

    }
}
