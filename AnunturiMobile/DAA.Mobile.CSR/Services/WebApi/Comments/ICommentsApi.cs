using Anunturi.Mobile.Services.WebApi.Comments.Dto;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Comments
{
    public interface ICommentsApi
    {
        [Get("/api/Comments/GetComments")]
        Task<IList<CommentItemDto>> GetComments(int pushId);

        [Post("/api/Comments/CreateComment")]
        Task<object> CreateComment(int pushId, string content);
    }
}
