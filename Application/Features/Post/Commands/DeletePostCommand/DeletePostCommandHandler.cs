using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Post.Commands.DeletePostCommand
{
    internal class DeletePostCommandHandler(IAppDbContext _context,ICurrentUser _currentUser, IUnitOfWork _unitOfWork) : IRequestHandler<DeletePostCommand, Result>
    {
        public async Task<Result> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
                return Result.Failure(new Error("Forbbiden Operation"));

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId.Value);
            if (profile is null)
                return Result.Failure(new Error("UnExist Profile"));

            var post=await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.postId);
            if (post is null || post.ProfileId != profile.Id)
                return Result.Failure(new Error("Forbidden Action"));
            
            //delete post images and post Votes and post Comments

              var postVotes= await _context.PostVotes.Where(x=>x.PostId==post.Id).ToListAsync();
              var postImages= await _context.PostImages.Where(x=>x.PostId==post.Id).ToListAsync();
              var postComments= await _context.Comments.Where(x=>x.PostId==post.Id).ToListAsync();

            _unitOfWork.PostVoteRepository.DeleteMany(postVotes);
            _unitOfWork.PostImagesRepository.DeleteMany(postImages);
            _unitOfWork.CommentRepository.DeleteMany(postComments);

            _unitOfWork.PostRepository.Delete(post);

           await  _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();

        }
    }
}
