using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.Entities;
using Domain.IRepositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comment.Commands.CreateCommentCommand
{
    internal sealed class CreateCommentHandler(IAppDbContext _context ,ICurrentUser _currentUser,IUnitOfWork _unitOfWork) : IRequestHandler<CreateCommentCommand,Result>
    {
        public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (!_currentUser.UserId.HasValue)
              return  Result.Failure(new Error("unAuthorized User"));

           var post=  await  _context.Posts.FindAsync(request.createCommentDto.PostId);
            if(post is null)
                return  Result.Failure(new Error("UnExist Post"));

            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.UserId == _currentUser.UserId.Value);
            if (profile is null)
                return Result.Failure(new Error("UnExist Profile"));

            Domain.Entities.Comment comment = new Domain.Entities.Comment
            {
                Content = request.createCommentDto.Content,
                PostId = request.createCommentDto.PostId,
                ProfileId= profile.Id
            };

            await _unitOfWork.CommentRepository.Insert(comment);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();
        }
    }
}
