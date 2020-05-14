using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Domain.Domain
{
    public class Anunt
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public IdentityRole Role { get; set; }
    }
}
