using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TBC.Capital.Api.Controllers.Shared;
using TBC.Capital.Core.ViewModels.APIRequest;

namespace TBC.Capital.Api.Controllers
{
    [RoutePrefix("api/subscription")]
    public class SubscriptionController : BaseApiController
    {
        [HttpPost]
        [Route]
        public IHttpActionResult Index([FromBody]SubscriberVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                APIService.Subscription.UpdateSubscriber(model);
                return Ok();
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }
        [HttpGet]
        [Route("{token}")]
        public IHttpActionResult Index(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest();
            }
            try
            {
                var model = APIService.Subscription.GetSubscriber(token);
                return Ok(new
                {
                    data = model,
                    meta = new { }
                });
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{token}")]
        public IHttpActionResult Index([FromUri]string token, [FromBody]SubscriberVM model)
        {
            if (string.IsNullOrWhiteSpace(token) || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                APIService.Subscription.UpdateSubscriber(model, token);
                return Ok();
            }
            catch
            {
                return BadRequest(ModelState);
            }
        }
    }
}
