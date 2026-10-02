using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Comment.Commands.UpdateCommentCommand
{
    public sealed record UpdateCommentCommand(Guid CommentId,string Content):IRequest<Result>;
   
}
