using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace TBC.Capital.Core.Utils
{
    public class Settings
    {
        public const string DefaultLang = "ka";
        public static string CurrentLang = "ka";
        public const string ImageUploadFolderPath = "/uploads";
        public static string siteUrl = System.Configuration.ConfigurationManager.AppSettings["site_url"];
    }
}
