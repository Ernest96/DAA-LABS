using System;
using System.Collections.Generic;
using System.Text;

namespace Anunturi.Mobile.Services.WebApi.Comments.Dto
{
    public class CommentCreateDto
    {
        public int PushId { get; set; }
        public string Content { get; set; }
    }
}
