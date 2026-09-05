using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Post.Commands.Dtos
{
    public sealed class PostDto
    {
        public int? PostId { get; set; }
        public string? Description { get; set; }
        public List<string> ImagesUrl { get; set; }=new List<string>();
    }
}
