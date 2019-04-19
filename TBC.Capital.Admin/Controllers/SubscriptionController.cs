using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TBC.Capital.Admin.Controllers.Shared;

namespace TBC.Capital.Admin.Controllers
{
    public class SubscriptionController : BaseAdminController
    {
        // GET: Subscription
        public ActionResult Index()
        {
            var model = Service.Subscription.GetSubscribers();
            return View(model);
        }
        //Delete subscriber
        public ActionResult DeleteSubscriber(int id = 0)
        {
            if (id == 0)
            {
                return Content("მონაცემი ვერ მოიძებნა");
            }
            Service.Subscription.DeleteSubscriber(id);
            return RedirectToAction("Index");
        }
    }
}