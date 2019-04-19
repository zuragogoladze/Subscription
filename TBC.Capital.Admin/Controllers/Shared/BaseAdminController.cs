using TBC.Capital.Core.Utils;
using TBC.Capital.Dal.Models;
using System;
using System.Web.Hosting;
using System.Web.Mvc;
using TBC.Capital.Bll.Services.AdminServices.shared;

namespace TBC.Capital.Admin.Controllers.Shared
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class BaseAdminController : Controller
    {
        private BaseService _service;
        private string _Lang = Settings.DefaultLang;

        public BaseService Service => _service ?? (_service = new BaseService(new ApplicationDbContext()));

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            if (RouteData.Values.ContainsKey("lang"))
            {
                _Lang = RouteData.Values["lang"].ToString().ToLower();
                ViewBag.CurrentLang = _Lang;
                Settings.CurrentLang = _Lang;
            }
            ViewBag.Languages = Service.Language.Get();

            var AbsolutePath = Request.Url.AbsoluteUri;
            var CN = ControllerContext.RouteData.Values["controller"].ToString();
            ViewBag.CN = CN;
            ViewBag.AbsolutePath = AbsolutePath;
            InitializeCulture();
            ViewBag.message = SessionHelper.GetMessage();
            return base.BeginExecuteCore(callback, state);
        }

        private void InitializeCulture()
        {
            var culture = (System.Globalization.CultureInfo)System.Globalization.CultureInfo.GetCultureInfo(_Lang).Clone();

            culture.NumberFormat.CurrencyDecimalSeparator =
            culture.NumberFormat.PercentDecimalSeparator =
            culture.NumberFormat.NumberDecimalSeparator = ".";

            culture.DateTimeFormat = new System.Globalization.DateTimeFormatInfo
            {
                FirstDayOfWeek = DayOfWeek.Monday,
                ShortDatePattern = "dd/MM/yyyy",
                LongDatePattern = "MMM d, yyyy"
            };

            if (_Lang == "ka")
            {
                culture.DateTimeFormat = new System.Globalization.DateTimeFormatInfo()
                {
                    LongDatePattern = "dd.MM.yyyy hh:mm:ss",
                    AbbreviatedMonthGenitiveNames = new string[] { "იან", "თებ", "მარ",
                                                  "აპრ", "მაი", "ივნ",
                                                  "ივლ", "აგვ", "სექ",
                                                  "ოქტ", "ნოე", "დეკ", "" },
                    AbbreviatedMonthNames = new string[] { "იან", "თებ", "მარ",
                                                  "აპრ", "მაი", "ივნ",
                                                  "ივლ", "აგვ", "სექ",
                                                  "ოქტ", "ნოე", "დეკ", "" },
                    MonthNames = new string[] { "იანვარი", "თებერვალი", "მარტი",
                                                  "აპრილი", "მაისი", "ივნისი",
                                                  "ივლისი", "აგვისტო", "სექტემბერი",
                                                  "ოქტომბერი", "ნოემბერი", "დეკემბერი", "" },
                    AbbreviatedDayNames = new string[] {
                        "კვი", "ორშ", "სამ", "ოთხ", "ხუთ", "პარ", "შაბ"
                    }
                };
            }
            else if (_Lang == "ru")
            {
                culture.DateTimeFormat = new System.Globalization.DateTimeFormatInfo()
                {
                    LongDatePattern = "dd/MM/yyyy hh:mm:ss a",
                    AbbreviatedMonthGenitiveNames = new string[] { "янв", "фев", "март",
                                                  "апр", "май", "июн",
                                                  "июл", "авг", "сен",
                                                  "окт", "ноя", "дек", "" },
                    AbbreviatedMonthNames = new string[] {  "янв", "фев", "март",
                                                  "апр", "май", "июн",
                                                  "июл", "авг", "сен",
                                                  "окт", "ноя", "дек", ""  },
                    MonthNames = new string[] {  "январь", "февраль", "март",
                                                  "апрель", "май", "июнь",
                                                  "июль", "август", "сентябрь",
                                                  "октябрь", "ноябрь", "декабрь", ""  },
                    AbbreviatedDayNames = new string[] {
                        "Вос", "Пон", "Вто", "Сре", "Чет", "Пят", "Суб"
                    }
                };
            }

            System.Threading.Thread.CurrentThread.CurrentCulture =
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            ViewBag.CN = RouteData.Values["controller"].ToString().ToLower();
        }

        [HttpPost]
        public JsonResult GetYoutubeVideoId(string videoUrl)
        {
            var id = Helpers.GetYoutubeId(videoUrl);
            return Json(new
            {
                videoId = id,
            });
        }
        [HttpPost]
        public JsonResult GetVimeoVideoId(string videoUrl)
        {
            var id = Helpers.GetVimeoId(videoUrl);
            return Json(new
            {
                videoId = id,
            });
        }
    }
}