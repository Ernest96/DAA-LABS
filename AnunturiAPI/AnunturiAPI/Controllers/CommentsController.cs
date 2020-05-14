using AnunturiAPI.Filters;
using AnunturiAPI.Helpers;
using AnunturiAPI.Models;
using BLL.DTO;
using BLL.Services;
using Domain;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace AnunturiAPI.Controllers
{
    [APIKeyFilter]
    [Authorize]
    [RoutePrefix("api/Comments")]
    public class CommentsController : ApiController
    {
        private readonly CommentsService _commentsService;
        private readonly AnuntService _anuntService;
        private readonly SignalRHelper _signalRHelper;



        public CommentsController()
        {
            var dbContext = new ApplicationDbContext();
            _commentsService = new CommentsService(dbContext);
            _anuntService = new AnuntService(dbContext);
            _signalRHelper = new SignalRHelper();
        }

        [HttpGet]
        [Route("GetComments")]
        public IEnumerable<CommentItemDto> GetComments(int pushId)
        {
            var anunt = _anuntService.GetAnuntWithRole(pushId);

            if (User.IsInRole(anunt.Role))
            {
                var result = _commentsService.GetComments(pushId);
                return result;
            }

            return null;
        }

        [HttpPost]
        [Route("CreateComment")]
        public IHttpActionResult CreateComment(int pushId, string content)
        {
            var anunt = _anuntService.GetAnuntWithRole(pushId);

            if (User.IsInRole(anunt.Role))
            {
                var dto = new CommentCreateDto()
                {
                    Content = content,
                    Author = User.Identity.GetUserName(),
                    Created = DateTime.Now,
                    PushId = pushId
                };

                _commentsService.CreateComment(dto);

                var commentItem = new CommentItemDto()
                {
                    Author = dto.Author,
                    Content = dto.Content,
                    Created = dto.Created
                };

                _signalRHelper.NotifyCommentsForPush(commentItem, pushId);

                return Ok();
            }

            return BadRequest();
        }
    }
}
