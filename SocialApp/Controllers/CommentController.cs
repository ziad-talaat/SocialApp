using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Comment.Commands.CreateCommentCommand;
using Application.Features.Comment.Commands.DeleteCommentCommand;
using Application.Features.Comment.Commands.UpdateCommentCommand;
using Application.Features.Comment.DTOS;
using Application.Features.Comment.Queries.GetCommentsPerPost;
using Application.Features.Post.Commands.DeletePostCommand;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace SocialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CommentController(IMediator _mediator) : ControllerBase
    {
        [HttpGet("getCommentsByPost")]
        public async Task<IActionResult> GetCommentsPerPost(int postId)
        {
            var getCommentsPerPostQuery=new GetCommentsPerPostQuery(postId);
            var result = await _mediator.Send<List<CommentDto>>(getCommentsPerPostQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCommentDto createCommentDto)
        {
            

            var createCommentCommand = new CreateCommentCommand(createCommentDto);
            var result = await _mediator.Send<Result>(createCommentCommand);
            if(!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
          
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid commentId)
        {
            var deleteCommentCommand = new DeleteCommentCommand(commentId);
            var result = await _mediator.Send<Result<bool>>(deleteCommentCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Error);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put( Guid commentId, [FromBody] string content)
        {
            var updateCommentCommand = new UpdateCommentCommand(commentId,content);
            var result = await _mediator.Send<Result>(updateCommentCommand);
            if (!result.IsSuccess)
                return BadRequest(result.Errors);
            return Ok(result);
        }


    }
}
