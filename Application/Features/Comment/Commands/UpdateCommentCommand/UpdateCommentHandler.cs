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

namespace Application.Features.Comment.Commands.UpdateCommentCommand
{
    internal class UpdateCommentHandler(ICurrentUser _currentUser,IAppDbContext _context,IUnitOfWork _unitOfWork) : IRequestHandler<UpdateCommentCommand, Result>
    {
        public async Task<Result> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
                return Result.Failure(new Error("Use unExist"));
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId.Value);
            if (profile is null)
                return Result.Failure(new Error("UnExist Profile"));

            Domain.Entities.Comment? comment = await _context.Comments
                .FirstOrDefaultAsync(x => x.Id == request.CommentId);
            if (comment is null || comment.ProfileId != profile.Id)
                return Result.Failure(new Error("Invalid Operation"));

            comment.Content=request.Content;
            comment.IsUpdated=true;

            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
            

        }
    }
}
