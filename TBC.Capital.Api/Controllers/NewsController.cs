using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TBC.Capital.Api.Controllers.Shared;

namespace TBC.Capital.Api.Controllers
{
    [RoutePrefix("api/news")]
    public class NewsController : BaseApiController
    {
        [HttpGet]
        [Route("{slug}")]
        public IHttpActionResult Index(string Slug)
        {
            try
            {
                var model = APIService.News.GetNewsBySlug(Slug);
                return Ok(new
                {
                    data = model,
                    meta = new { }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route]
        public IHttpActionResult Index([FromUri]List<int> years, [FromUri]List<int> monthes, [FromUri]List<int> tags, int page = 0, int limit = 6, bool orderByDescending = false, string excludeSlug = "")
        {
            try
            {
                var model = APIService.News.FilterNews(years, monthes, tags, page, limit, orderByDescending, excludeSlug);
                return Ok(new
                {
                    data = model.News,
                    meta = model.Pagination
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("years")]
        public IHttpActionResult Years()
        {
            try
            {
                var model = APIService.News.GetNewsYears();
                return Ok(new
                {
                    data = model,
                    meta = new { }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("monthes")]
        public IHttpActionResult Monthes()
        {
            try
            {
                var model = APIService.News.GetNewsMonthes();
                return Ok(new
                {
                    data = model,
                    meta = new { }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("tags")]
        public IHttpActionResult Tags()
        {
            try
            {
                var model = APIService.News.GetNewsTags();
                return Ok(new
                {
                    data = model,
                    meta = new { }
                });
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
