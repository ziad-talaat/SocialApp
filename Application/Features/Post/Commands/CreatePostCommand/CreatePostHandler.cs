
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.Entities;
using Domain.Enums;
using Domain.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Post.Commands.CreatePost
{
    internal class CreatePostHandler(IAppDbContext _context,ICurrentUser _currentUser,IUnitOfWork _unitOfWork) : IRequestHandler<CreatePostCommand, Result>
    {
        public async  Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            if(!_currentUser.UserId.HasValue)
                return Result.Failure(new Error("Forbbiden Action"));

            var profile = await _context.Profiles.FirstOrDefaultAsync(x=>x.UserId==_currentUser.UserId.Value,cancellationToken);
            if (profile is null)
                return Result.Failure(new Error("no such Profile"));

            bool isApproved = true;

           if(request.postDto.GroupId.HasValue)
            {
                var groupId = request.postDto.GroupId.Value;

                Group? group= await _context.Groups.AsNoTracking()
                    .FirstOrDefaultAsync(x=>x.Id==groupId,cancellationToken);
                if (group is null)
                    return Result.Failure(new Error("GroupNotFound"));


                var membership = await _context.GroupMembers.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ProfileId == profile.Id 
                    && x.GroupId == groupId
                    &&x.Status==GroupMemberStatus.Active, cancellationToken);

                if(membership is null)
                    return Result.Failure(new Error("No memberShip in this group"));

                bool isPrivillage = membership.GroupRole >= GroupRole.Admin;

                isApproved = isPrivillage || !group.RequirePostApproval;

            }


            var post = new Domain.Entities.Post
            {
                Description = request.postDto.Description,
                ProfileId = profile.Id,
                PostImages = (request.postDto.ImagesUrl ?? [])
                .Select(x=>new PostImages
                { 
                    ImageUrl=x
                }).ToList(),
                GroupId=request.postDto.GroupId,
                IsApproved= isApproved
            };

            await _unitOfWork.PostRepository.Insert(post);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();

        }
    }
}
