using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Post.DeletePost
{
    internal class DeletePostCommandHandler(IAppDbContext _context, IUnitOfWork _unitOfWork) : IRequestHandler<DeletePostCommand, Result>
    {
        public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
           var post=await _context.Posts.Include(x => x.Profile.UserId).FirstOrDefaultAsync(x => x.Id == request.postId);
            if (post is null)
                return Result.Failure(new Error("no such post"));
            if( request.userId  != post.Profile.UserId)
                return Result.Failure(new Error("Forbidden Action"));

            _unitOfWork.PostRepository.Delete(post);
           await  _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();

        }
    }
}
