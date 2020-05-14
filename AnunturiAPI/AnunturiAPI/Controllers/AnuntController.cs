using AnunturiAPI.Filters;
using AnunturiAPI.Helpers;
using AnunturiAPI.Hubs;
using AnunturiAPI.Models;
using BLL.DTO;
using BLL.Services;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Security;

namespace AnunturiAPI.Controllers
{
    [APIKeyFilter]
    [RoutePrefix("api/Anunt")]
    public class AnuntController : ApiController
    {
        private readonly AnuntService _anuntService;
        private readonly SmsService _smsService;
        private readonly SignalRHelper _signalRHelper;

        public AnuntController()
        {
            var dbContext = new ApplicationDbContext();

            _anuntService = new AnuntService(dbContext);
            _smsService = new SmsService();
            _signalRHelper = new SignalRHelper();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [IPFilter]
        [Route("Publish")]
        public IHttpActionResult Publish(AnuntModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = new AnuntDto()
                {
                    Content = model.Content,
                    Subject = model.Subject,
                    Role = model.Role
                };

                var anuntId = _anuntService.AddAnunt(dto);
                var pushInfo = new PushInfo() { Id = anuntId, Subject = dto.Subject };
                _signalRHelper.NotifyAnunturiForRole(pushInfo, dto.Role);

                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [IPFilter]
        [Route("PublishMany")]
        public IHttpActionResult PublishMany(IList<AnuntModel> anunturi)
        {
            
            foreach(var anunt in anunturi)
            {
                try
                {
                    var dto = new AnuntDto()
                    {
                        Content = anunt.Content,
                        Subject = anunt.Subject,
                        Role = anunt.Role
                    };

                    var anuntId = _anuntService.AddAnunt(dto);
                    var pushInfo = new PushInfo() { Id = anuntId, Subject = dto.Subject };
                    _signalRHelper.NotifyAnunturiForRole(pushInfo, dto.Role);
                }
                catch(Exception)
                {
                    // log exception
                }
            }

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [IPFilter]
        [Route("SendSms")]
        public IEnumerable<string> SendSms(string text)
        {
            var errors = Enumerable.Empty<string>();

            if (!string.IsNullOrWhiteSpace(text))
            {
                errors = _smsService.SendSms(text, APIEnvironment.SMS_RECEIVERS);
            }

            return errors;
        }

        [HttpGet]
        [Authorize]
        [Route("GetAnunturi")]
        public IEnumerable<AnuntInfoDto> GetAnunturi(string role)
        {
            var result = _anuntService.GetAnunturiForRole(role);

            return result;
        }

        [HttpGet]
        [Authorize]
        [Route("GetAnunt")]
        public AnuntDto GetAnunt(int pushId)
        {
            var result = _anuntService.GetAnuntWithRole(pushId);

            if (User.IsInRole(result.Role))
            {
                return result;
            }

            return null;
        }
    }
}
