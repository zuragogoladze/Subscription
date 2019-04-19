using System.Linq;
using System.Web.Http;
using TBC.Capital.Bll.Services.ApiServices.shared;
using TBC.Capital.Core.Utils;
using TBC.Capital.Dal.Models;

namespace TBC.Capital.Api.Controllers.Shared
{
    public class BaseApiController : ApiController
    {
        private APIBaseService _service;
        const string _header = "Accept-Language";

        public APIBaseService APIService => _service ?? (_service = new APIBaseService(new ApplicationDbContext()));

        public BaseApiController()
        {
            var lang = System.Web.HttpContext.Current.Request.Headers["Accept-Language"];
            if (!string.IsNullOrWhiteSpace(lang))
            {
                try
                {
                    var siteLangs = APIService.Language.Get().Select(x => x.Code).ToArray();
                    var langs = lang.Split(',', ';');
                    foreach (var lng in langs)
                    {
                        if (siteLangs.Contains(lng.ToString()))
                        {
                            Settings.CurrentLang = lng;
                            break;
                        }
                    }
                }
                catch
                {

                }
            }
        }


    }
}
