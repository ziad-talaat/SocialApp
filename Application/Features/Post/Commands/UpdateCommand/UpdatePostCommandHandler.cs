using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Post.Commands.Dtos;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Post.Commands.UpdateCommand
{
    internal class UpdatePostCommandHandler(IAppDbContext _context,IUnitOfWork _unitOfWork) : IRequestHandler<UpdatePostCommand, Result>
    {
        public async Task<Result> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.Include(x=>x.Profile.UserId).FirstOrDefaultAsync(x=>x.Id == request.postDto.PostId);
            if (post is null)
                return Result.Failure(new Error("no such post"));

            if (post.Profile.UserId != request.userId)
                return Result.Failure(new Error("Forbidden action"));

            post.Description= request.postDto.Description;
            await _unitOfWork.SaveAsync(cancellationToken);

            return Result.Success();

        }
    }
}
