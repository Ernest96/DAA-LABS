
using System;

namespace Domain.Abstraction
{
    public class CommentItem
    {
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
    }
}
