using Anunturi.Mobile.Services.WebApi.Comments.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anunturi.Mobile.Services.WebApi.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IRestPoolService _restPoolService;

        public CommentsService(IRestPoolService restPoolService)
        {
            _restPoolService = restPoolService;
        }

        public Task<object> CreateComment(int pushId, string content)
        {
            return _restPoolService.CommentsApi.CreateComment(pushId, content);
        }

        public Task<IList<CommentItemDto>> GetComments(int pushId)
        {
            return _restPoolService.CommentsApi.GetComments(pushId);
        }
    }
}
