using Domain;
using Domain.Abstraction;
using Domain.Domain;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class CommentsDal : GenericRepository<Comment>
    {
        public CommentsDal(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<CommentItem> GetComments(int pushId)
        {
            var comments = _context.Comments
                .Where(x => x.AnuntId == pushId)
                .OrderBy(x => x.Created)
                .Select(x => new CommentItem()
                {
                    Author = x.Author,
                    Content = x.Content,
                    Created = x.Created
                })
                .ToList();

            return comments;
        }
    }
}
