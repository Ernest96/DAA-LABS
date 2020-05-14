using System;

namespace BLL.DTO
{
    public class CommentCreateDto
    {
        public int PushId { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }

    }
}
