
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Domain.Entities;
using Domain.IRepositories;
using MediatR;

namespace Application.Features.Post.Commands.CreatePost
{
    internal class CreatePostHandler(IAppDbContext _context,IUnitOfWork _unitOfWork) : IRequestHandler<CreatePostCommand, Result>
    {
        public async  Task<Result> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
           var profile=await _context.Profiles.FindAsync(request.ProfileId, cancellationToken);
            if (profile is null)
                return Result.Failure(new Error("no such Profile"));

            var post = new Domain.Entities.Post
            {
                Description = request.postDto.Description,
                ProfileId = request.ProfileId,
                PostImages=request.postDto.ImagesUrl
                .Select(x=>new PostImages
                { 
                    ImageUrl=x
                }).ToList()
            };

            await _unitOfWork.PostRepository.Insert(post);
            await _unitOfWork.SaveAsync(cancellationToken);
            return Result.Success();

        }
    }
}
