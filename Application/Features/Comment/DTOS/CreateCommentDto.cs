using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.DTOS
{
    public sealed record CreateCommentDto(string Content,int PostId);
  
}
