using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Features.Comment.DTOS;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comment.Queries.GetCommentsPerPost
{
    public sealed  record GetCommentsPerPostQuery(int postId):IRequest<List<CommentDto>>;
    
}
