using TBC.Capital.Dal.Models;
using System.Collections.Generic;
using System.Linq;
using TBC.Capital.Bll.Services.Common;

namespace TBC.Capital.Bll.Services.Common
{
    public class LanguageService : ParentService
    {

        public LanguageService(ApplicationDbContext db) : base(db)
        {
        }

        public IEnumerable<Language> Get()
        {
            var data = _db.Language.OrderByDescending(i => i.Name).ToList();
            return data;
        }
    }
}
