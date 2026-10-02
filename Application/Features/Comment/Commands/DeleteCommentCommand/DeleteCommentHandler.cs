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

namespace Application.Features.Comment.Commands.DeleteCommentCommand
{
    internal sealed  class DeleteCommentHandler(ICurrentUser _currentUser,IUnitOfWork _unitOfWork,IAppDbContext _context) : IRequestHandler<DeleteCommentCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
                return Result<bool>.Failure(new Error("Forbbiden Operation"));

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId.Value);
            if (profile is null)
                return Result<bool>.Failure(new Error("UnExist Profile"));


            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == request.CommentId);
            if(comment is null || comment.ProfileId != profile.Id)
                return Result<bool>.Failure(new Error("Forbbiden Operation"));

            _unitOfWork.CommentRepository.Delete(comment);
            await  _unitOfWork.SaveAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
