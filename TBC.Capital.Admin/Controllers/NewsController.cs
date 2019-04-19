using TBC.Capital.Core.Utils;
using TBC.Capital.Core.ViewModels;
using TBC.Capital.Admin.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TBC.Capital.Admin.Controllers
{
    public class NewsController : BaseAdminController
    {
        // GET: Admin/News
        public ActionResult Index()
        {
            var model = Service.News.Get();
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(int? id)
        {
            ViewBag.Tags = Service.Tags.Get();
            ViewBag.Languages = Service.Language.Get();

            var model = Service.News.GetById(id);

            return View(model);
        }
        [HttpPost, ValidateInput(false), ValidateAntiForgeryToken]
        public ActionResult Update(NewsVM model)
        {
            if (ModelState.IsValid)
            {
                var isUpdated = Service.News.Update(model);
                if (isUpdated)
                {
                    SessionHelper.SaveMessage(true);
                    return RedirectToAction("Index");
                }
            }
            SessionHelper.SaveMessage();
            ViewBag.Tags = Service.Tags.Get();
            ViewBag.Languages = Service.Language.Get();
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id = 0)
        {
            if (id == 0)
            {
                return Content("მონაცემი ვერ მოიძებნა");
            }
            Service.News.Delete(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult GenerateSlug(string slug)
        {
            var index = 0;
            var generatedSlug = LanguageHelper.GenerateSlug(slug);
            var newSug = generatedSlug;
            while (CheckSlug(newSug))
            {
                index++;
                newSug = generatedSlug + "-" + index.ToString();
            }
            return Json(new
            {
                slug = newSug
            });
        }
        public bool CheckSlug(string slug)
        {
            var result = Service.News.CheckSlug(slug);
            return result;
        }
    }
}