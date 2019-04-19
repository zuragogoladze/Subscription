using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.Capital.Core.ViewModels;
using TBC.Capital.Dal.Models;
using TBC.Capital.Dal.Models.Content;


namespace TBC.Capital.Bll.Services.Common
{
    public class ParentService
    {
        protected ApplicationDbContext _db { get; set; }

        protected static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public ParentService(ApplicationDbContext db)
        {
            this._db = db;
        }
    }
}
