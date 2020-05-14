
using Anunturi.Mobile.Services.WebApi.Comments.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Comments
{
    public interface ICommentsService
    {
        Task<IList<CommentItemDto>> GetComments(int pushId);
        Task<object> CreateComment(int pushId, string content);
    }
}
