using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Comment.DTOS;
using MediatR;

namespace Application.Features.Comment.Commands.CreateCommentCommand
{
    public sealed record CreateCommentCommand(CreateCommentDto createCommentDto):IRequest<Result>;
    
}
