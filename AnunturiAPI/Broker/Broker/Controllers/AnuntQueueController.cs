using BLL.DTO;
using Broker.Filters;
using Broker.Services;
using System.Web.Http;

namespace Broker.Controllers
{
    [RoutePrefix("api/AnuntQueue")]
    [APIKeyFilter]
    [IPFilter]
    public class AnuntQueueController : ApiController
    {
        public IHttpActionResult Enqueue(AnuntDto anunt)
        {
            PushQueue.Enqueue(anunt);
            return Ok();
        }
    }
}