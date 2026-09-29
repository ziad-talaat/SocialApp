using Application.Common.ResultPattern;
using Application.Features.Post.Dtos;
using MediatR;
namespace Application.Features.Post.Commands.UpdateCommand
{
    public sealed record UpdatePostCommand(PostDto postDto,Guid userId):IRequest<Result>;
   
}
