using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.DTOS
{
    public sealed record CommentDto(string Description,DateTime AdditionDate,bool IsUpdated,string? ProfileImageUrl,string ProfileName);
  
}
