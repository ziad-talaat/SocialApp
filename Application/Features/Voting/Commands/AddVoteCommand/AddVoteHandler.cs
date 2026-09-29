
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Voting.Commands.AddVoteCommand
{
    internal class AddVoteHandler(IAppDbContext _context,IUnitOfWork _unitOfWork,ICurrentUser _currentUser) : IRequestHandler<AddVoteCommand, Result>
    {
        public async Task<Result> Handle(AddVoteCommand request, CancellationToken cancellationToken)
        {


            if (!_currentUser.UserId.HasValue)
                return Result.Failure(new Error("invalid user Info"));


            var user = await _context.AppUsers.FindAsync(_currentUser.UserId.Value);
            if (user == null)
                return Result.Failure(new Error("No such User"));

            var post = await _context.Posts.FindAsync(request.voteDto.PostId);
            if (post == null)
                return Result.Failure(new Error("No such post"));

            bool isAlreadyVoted =await   _context.PostVotes
                .AnyAsync(v => v.PostId == request.voteDto.PostId 
                && v.UserId == _currentUser.UserId, cancellationToken);
            if (isAlreadyVoted)
                return Result.Failure(new Error("User has already voted for this post")); 
            
            await _unitOfWork.PostVoteRepository.Insert(new PostVote
            {
                PostId = request.voteDto.PostId,
                UserId = _currentUser.UserId.Value,
                VoteType = request.voteDto.VoteType
            });

            return Result.Success();
        }
    }
}
