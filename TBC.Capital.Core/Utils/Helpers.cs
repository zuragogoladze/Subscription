using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TBC.Capital.Core.ViewModels;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using System.Drawing;
using ImageResizer;
using System.Drawing.Imaging;
using System.Web;
using TBC.Capital.Core.ViewModels.API;
using NLog;

namespace TBC.Capital.Core.Utils
{
    public static class Helpers
    {
        public static string StrimHTML(string html)
        {
            if (string.IsNullOrEmpty(html)) return "";
            return HttpUtility.HtmlDecode(Regex.Replace(html, "<.*?>", string.Empty));
        }

        public static string GetYoutubeId(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var youtubeMatch = new Regex(@"(?:youtube\.com\/\S*(?:(?:\/e(?:mbed))?\/|watch\?(?:\S*?&?v\=))|youtu\.be\/)([a-zA-Z0-9_-]{11})").Match(url);
                if (youtubeMatch.Success)
                {
                    return youtubeMatch.Groups[1].Value;
                }
            }
            return null;
        }
        public static PaginationVM GetPaginationData(IQueryable<object> query, int page, int limit)
        {
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / limit);
            if (totalPages < page + 1)
            {
                throw new KeyNotFoundException();
            }
            var prevPageLink = page > 0 ? GetPaginationUrl("team-members", page - 1, limit) : "";
            var nextPageLink = page < totalPages - 1 ? GetPaginationUrl("team-members", page + 1, limit) : "";
            var pagination = new PaginationVM()
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrPage = page,
                NextPageLink = nextPageLink,
                PrevPageLink = prevPageLink,
            };
            return pagination;
        }

        public static string GetPaginationUrl(string controller, int page, int limit)
        {
            return $"/{controller}?page={page}&limit={limit}";
        }

        public static string GetVimeoId(string url)
        {
            if (!string.IsNullOrWhiteSpace(url))
            {
                var youtubeMatch = new Regex(@"vimeo\.com/(?:.*#|.*/videos/)?([0-9]+)").Match(url);
                if (youtubeMatch.Success)
                {
                    return youtubeMatch.Groups[1].Value;
                }
            }
            return null;
        }

      
        public static string PartialView(Controller controller, string viewName, object model)
        {
            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);

                return sw.ToString();
            }
        }
        public static string Tokenizer()
        {
            var random = Guid.NewGuid().ToString();
            return random;
        }

        public static int GenerateRandomNombers()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

    }

    public static class LanguageHelper
    {

        public static string GenerateSlug(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;

            //str = StripHTML(str);

            var geoAlpha = new List<string> { "ა", "ბ", "გ", "დ", "ე", "ვ", "ზ", "თ", "ი", "კ", "ლ", "მ", "ნ", "ო", "პ", "ჟ", "რ", "ს", "ტ", "უ", "ფ", "ქ", "ღ", "ყ", "შ", "ჩ", "ც", "ძ", "წ", "ჭ", "ხ", "ჯ", "ჰ" };
            var geoEngAlpha = new List<string> { "a", "b", "g", "d", "e", "v", "z", "T", "i", "k", "l", "m", "n", "o", "p", "J", "r", "s", "t", "u", "f", "q", "R", "y", "sh", "ch", "c", "Z", "w", "W", "x", "j", "h" };

            var rusAlpha = new List<string> { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "ъ", "ы", "ь", "э", "ю", "я" };
            var rusUpAlpha = new List<string> { "А", "Б", "В", "Г", "Д", "Е", "Ё", "Ж", "З", "И", "Й", "К", "Л", "М", "Н", "О", "П", "Р", "С", "Т", "У", "Ф", "Х", "Ц", "Ч", "Ш", "Щ", "Ъ", "Ы", "Ь", "Э", "Ю", "Я" };
            var rusEngAlpha = new List<string> { "a", "b", "v", "g", "d", "ye", "yo", "zh", "z", "ee", "y", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "f", "kh", "ts", "ch", "sh", "sh", "i", "i", "", "", "", "" };

            for (var i = 0; i < geoAlpha.Count; i++)
            {
                str = str.Replace(geoAlpha[i], geoEngAlpha[i]);
            }

            for (var i = 0; i < rusAlpha.Count; i++)
            {
                str = str.Replace(rusAlpha[i], rusEngAlpha[i]);
                str = str.Replace(rusUpAlpha[i], rusEngAlpha[i]);
            }

            str = Regex.Replace(str, @"[^a-zA-Z0-9\s-]", "-"); // invalid chars      
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space   
            str = str.Substring(0, str.Length <= 100 ? str.Length : 100).Trim(); // cut and trim it   
            str = Regex.Replace(str, @"\s", "-"); // hyphens   

            return str;
        }

        public static string ActiveLanguage => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public static string Url(string Action = "")
        {
            var scheme = HttpContext.Current.Request.Headers.Get("X-Forwarded-Proto");

            string baseUrl = (scheme ?? HttpContext.Current.Request.Url.Scheme) + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";

            return baseUrl + ActiveLanguage + "/" + Action;
        }

        public static string RemoveLanguageString(this string pathWithLang)
        {
            var result = pathWithLang.Replace("/ka", "");
            result = result.Replace("/en", "");
            result = result.Replace("/ru", "");
            if (result == "/")
            {
                result = "";
            }
            return result;
        }

    }

    public static class Files
    {
        public static string GetPath(string folderPath, string filename)
        {
            return Path.Combine(folderPath, filename);
        }

        public static string UploadFile(HttpPostedFileBase file, String parentFileName = null)
        {
            try
            {
                if (file == null) return null;
                var allowedExtensions = new[] { ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".ppt", ".pptx", ".png", ".gif", ".jpeg", ".jpg" };
                var checkextension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(checkextension))
                {
                    return null;
                }
                var filename = file.FileName;
                if (file.FileName.Length > 70)
                {
                    filename = file.FileName.Substring(0, 60) + checkextension;
                }

                var fileName = (!string.IsNullOrEmpty(parentFileName) ? parentFileName + "_" : "") + Guid.NewGuid().ToString().Split('-').First() + "_" + filename;
                fileName = fileName.Replace(" ", "");
                var pathName = HttpContext.Current.Server.MapPath(Settings.ImageUploadFolderPath) + $"\\{parentFileName}";

                Directory.CreateDirectory(pathName);
                string filePathImage = GetPath(pathName, fileName);

                file.SaveAs(filePathImage);

                return fileName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //For api services
        public static string UploadFile(HttpPostedFile file, String parentFileName = null)
        {
            try
            {
                if (file == null) return null;
                var allowedExtensions = new[] { ".doc", ".docx", ".pdf", ".xls", ".xlsx", ".png", ".gif", ".jpeg", ".jpg" };
                var checkextension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(checkextension))
                {
                    return null;
                }

                var fileName = (!string.IsNullOrEmpty(parentFileName) ? parentFileName + "_" : "") + Guid.NewGuid().ToString().Split('-').First() + "_" + file.FileName;
                fileName = fileName.Replace(" ", "");
                var pathName = HttpContext.Current.Server.MapPath(Settings.ImageUploadFolderPath) + $"\\{parentFileName}";

                Directory.CreateDirectory(pathName);
                string filePathImage = GetPath(pathName, fileName);
                file.SaveAs(filePathImage);

                return $"{Settings.ImageUploadFolderPath}/{parentFileName}/fileName";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string FileUploadResize(HttpPostedFileBase file, String parentFileName = null, bool ck = false)
        {
            if (file != null)
            {
                var allowedExtensions = new[] { ".png", ".gif", ".jpeg", ".jpg" };
                var checkextension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(checkextension))
                {
                    return null;
                }

                var fileName = (!string.IsNullOrEmpty(parentFileName) ? parentFileName + "_" : "") + Guid.NewGuid().ToString().Split('-').First() + "_" + file.FileName;
                fileName = fileName.Replace(" ", "");
                var pathName = System.Web.HttpContext.Current.Server.MapPath(Settings.ImageUploadFolderPath);

                if (ck)
                {
                    pathName = pathName + "/ckimages";
                }
                var path = Path.Combine(pathName, fileName);

                ImageJob i = new ImageJob(file, path, new ImageResizer.Instructions(
                     "format=jpg;mode=max;scale=both"));   // ----> ვინახავთ ფაილში
                i.CreateParentDirectory = true; //Auto-create the uploads directory.
                i.Build();

                return fileName;
            }

            return null;
        }
    }

    public static class UploadHelper
    {
        public static ImageFormat GetBase64Extension(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return null;
            var imgstring = base64.ToLower();
            if (imgstring.Contains("/png;"))
            {
                return ImageFormat.Png;
            }
            else
            {
                return ImageFormat.Jpeg;
            }
        }

        public static string GetImageName(string existingImageNameOrBase64, string prefix, string innerFolder="")
        {
            if (string.IsNullOrWhiteSpace(existingImageNameOrBase64)) return null;
            var extension  = GetBase64Extension(existingImageNameOrBase64);

            var image = existingImageNameOrBase64.Base64ToImage();
            if (image == null && !string.IsNullOrEmpty(existingImageNameOrBase64)) // If string is not empty and can't convert from Base64
            {
                return existingImageNameOrBase64;
            }

            var newImageName = UploadImage(image, prefix, extension, innerFolder);

            return newImageName;
        }

        public static string UploadImage(Image Image, string prefix, ImageFormat type, string innerFolder="")
        {
            try
            {
                string imageName = prefix + "_" + Guid.NewGuid().ToString().Split('-').First() + "." + type.ToString();
                var folderPath = HttpContext.Current.Server.MapPath(Settings.ImageUploadFolderPath) + $"\\{innerFolder}";
                SaveImage(Image, folderPath, imageName, type);

                return $"{Settings.ImageUploadFolderPath}/{innerFolder}/{imageName}";
            }
            catch
            {
                return null;
            }
        }

        public static Image Base64ToImage(this string base64String)
        {
            try
            {
                base64String = base64String.Substring(base64String.IndexOf(',') + 1);
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0,
                  imageBytes.Length);

                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
            catch (Exception ex)
            {
                var s = ex.Message;
                return null;
            }
        }

        public static void SaveImage(this Image image, string folderPath, string filename, ImageFormat type)
        {
            string filePathImage = getPath(folderPath, filename);
            Directory.CreateDirectory(folderPath);
            image.Save(filePathImage, type);
            image.Dispose();

        }

        public static string getPath(string folderPath, string filename)
        {
            return Path.Combine(folderPath, filename);
        }

        
    }

    public static class SessionHelper
    {
        public static void SaveMessage(bool result = false, string message = "")
        {
            RemoveMessage();
            var data = new SessionMessageVM()
            {
                Result = result,
                Message = (string.IsNullOrWhiteSpace(message) ? (result ? "ოპერაცია წარმატებით განხორციელდა" : "დაფიქსირდა შეცდომა. სცადეთ თავიდან") : message)
            };

            HttpContext.Current.Session["message"] = data;
        }

        public static void RemoveMessage()
        {
            HttpContext.Current.Session["message"] = null;
        }

        public static SessionMessageVM GetMessage()
        {
            if (HttpContext.Current.Session["message"] != null)
            {
                var resulT = HttpContext.Current.Session["message"] as SessionMessageVM;
                RemoveMessage();
                return resulT;
            }
            return null;
        }
    }
}


