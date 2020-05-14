
using System;

namespace Anunturi.Mobile.Services.WebApi.Comments.Dto
{
    public class CommentItemDto
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}
