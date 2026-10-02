
using Application.Common.Interfaces;
using Application.Common.ResultPattern;
using Application.Features.Comment.DTOS;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Comment.Queries.GetCommentsPerPost
{
    internal sealed  class GetCommentsPerPostHandler(IAppDbContext _context) : IRequestHandler<GetCommentsPerPostQuery, List<CommentDto>>
    {
        public async Task<List<CommentDto>> Handle(GetCommentsPerPostQuery request, CancellationToken cancellationToken)
        {
            return  await _context.Comments.AsNoTracking().Where(x => x.PostId == request.postId)
               .Select(x=>new CommentDto(x.Content,x.CreationDate,x.IsUpdated,x.Profile.ImageUrl,x.Profile.ProfileName) )
               .ToListAsync(cancellationToken);
        }
    }
}