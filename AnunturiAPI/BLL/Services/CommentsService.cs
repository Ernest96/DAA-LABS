using BLL.DTO;
using DAL;
using Domain;
using Domain.Domain;
using System;
using System.Collections.Generic;

namespace BLL.Services
{
    public class CommentsService
    {
        private readonly IGenericRepository<Comment> _commentsRepo;
        private readonly CommentsDal _commentsDal;

        public CommentsService(ApplicationDbContext dbContext)
        {
            _commentsDal = new CommentsDal(dbContext);
            _commentsRepo = new GenericRepository<Comment>(dbContext);
        }

        public IEnumerable<CommentItemDto> GetComments(int pushId)
        {
            var comments = _commentsDal.GetComments(pushId);
            var result = new List<CommentItemDto>();

            foreach(var c in comments)
            {
                result.Add(new CommentItemDto()
                {
                    Author = c.Author,
                    Content = c.Content,
                    Created = c.Created
                });
            }

            return result;
        }

        public void CreateComment(CommentCreateDto dto)
        {
            var comment = new Comment()
            {
                AnuntId = dto.PushId,
                Author = dto.Author,
                Content = dto.Content,
                Created = dto.Created
            };

            _commentsRepo.Insert(comment);
            _commentsRepo.Save();
        }
    }
}
