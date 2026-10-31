using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Resources.CommentResources;
using Application.Resources.GlobalResources;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
namespace Application.Features.Comment.Commands.DeleteCommentCommand
{
    internal sealed  class DeleteCommentHandler(ICurrentUser _currentUser,IUnitOfWork _unitOfWork,IAppDbContext _context,IStringLocalizer<CommentValidation>_commentLocalizer, IStringLocalizer<GlobalValidationMessagesRes> _globalLocalizer) : IRequestHandler<DeleteCommentCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
                return Result<bool>.Failure(new Error(_globalLocalizer[nameof(GlobalValidationMessagesRes.UnAuthorizedUser)]));

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId.Value);
            if (profile is null)
                return Result<bool>.Failure(new Error("UnExist Profile"));


            var comment = await _context.Comments.Include(x=>x.Post).FirstOrDefaultAsync(x => x.Id == request.CommentId);
            if(comment is null)
                return Result<bool>.Failure(new Error(_commentLocalizer[nameof(CommentValidation.UnexistComment)]));

            if(   comment.ProfileId != profile.Id  && comment.Post.ProfileId!=profile.Id) 
                return Result<bool>.Failure(new Error("Forbbiden Operation"));

            _unitOfWork.CommentRepository.Delete(comment);
            await  _unitOfWork.SaveAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
