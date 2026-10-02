using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Comment.Commands.DeleteCommentCommand
{
    public sealed record  DeleteCommentCommand(Guid CommentId):IRequest<Result<bool>>;
    
}
