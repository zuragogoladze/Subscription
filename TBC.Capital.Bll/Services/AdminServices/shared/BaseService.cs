using TBC.Capital.Bll.Services.Common;
using TBC.Capital.Dal.Models;

namespace TBC.Capital.Bll.Services.AdminServices.shared
{

    public class BaseService
    {
        protected ApplicationDbContext _db;


        public BaseService(ApplicationDbContext db)
        {
            _db = db;
        }

        private LanguageService _LanguageService;
        public LanguageService Language => _LanguageService ?? (_LanguageService = new LanguageService(_db));

        private NewsService _NewsService;
        public NewsService News => _NewsService ?? (_NewsService = new NewsService(_db));

        private SubscriptionService _SubscriptionService;
        public SubscriptionService Subscription => _SubscriptionService ?? (_SubscriptionService = new SubscriptionService(_db));
        
    }
}
