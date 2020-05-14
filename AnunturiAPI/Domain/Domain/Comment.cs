using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain
{
    public class Comment
    {
        public int Id { get; set; }
        public int AnuntId { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public Anunt Anunt { get; set; }
    }
}
