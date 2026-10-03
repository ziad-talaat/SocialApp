using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.ResultPattern;
using Application.Features.Post.Dtos;
using MediatR;

namespace Application.Features.Post.Commands.CreatePost
{
    public sealed record CreatePostCommand(PostDto postDto):IRequest<Result>;
}
