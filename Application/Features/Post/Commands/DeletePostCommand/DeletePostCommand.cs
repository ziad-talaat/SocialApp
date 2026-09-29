using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using MediatR;

namespace Application.Features.Post.Commands.DeletePostCommand
{
    public sealed  record DeletePostCommand(int postId,Guid userId):IRequest<Result>;
    
    
}
