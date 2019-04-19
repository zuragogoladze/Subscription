using System;
using System.Collections.Generic;
using System.Linq;
using TBC.Capital.Core.Utils;
using TBC.Capital.Core.ViewModels;
using TBC.Capital.Dal.Models.Content;
using TBC.Capital.Dal.Models;
using AutoMapper;
using System.Transactions;
using System.Data.Entity;
using TBC.Capital.Bll.Services.Common;

namespace TBC.Capital.Bll.Services.AdminServices
{
    public class NewsService : ParentService
    {
        public NewsService(ApplicationDbContext db) : base(db)
        {
        }

        public List<NewsVML> Get()
        {
            var model = _db.News.Include(x => x.Translations).Where(x => !x.DeleteDate.HasValue).OrderByDescending(x => x.PublishDate).ToList();
            var mappedModel = model.Select(Mapper.Map<News, NewsVML>).ToList();
            return mappedModel;
        }

        public bool Update(NewsVM model)
        {
            var news = new News();
            try
            {
                model.Image = UploadHelper.GetImageName(model.Image, "news", "news");
                model.CoverImage = UploadHelper.GetImageName(model.CoverImage, "news-cover", "news");
                model.ShareImage = UploadHelper.GetImageName(model.ShareImage, "news-share", "news");

                model.Slug = LanguageHelper.GenerateSlug(model.Slug);

                using (var scope = new TransactionScope())
                {
                    if (model.Id == 0)
                    {
                        news.Update(model);
                        _db.News.Add(news);
                    }
                    else
                    {
                        news = _db.News.Include("Translations").Include(e=>e.NewsToTags).Include(e=>e.AttachedFiles).Where(x => x.Id == model.Id).FirstOrDefault();
                        news.Update(model);
                    }
                    news.UpdateTranslations(model.Translations);
                    news.UpdateTags(model.TagsId);

                    // attached files
                    if (model.AttachedFiles != null && model.AttachedFiles.Count() > 0)
                        news.UpdateFiles(model.AttachedFiles);

                    //remove files
                    if (model.Id != 0 && news.AttachedFiles != null)
                    {
                        var removedFiles = news.AttachedFiles.Where(x => !model.AttachedFiles.Any(q => q.Id == x.Id)).ToList();
                        _db.AttachedFiles.RemoveRange(removedFiles);
                    }
                    //remove category
                    if (news.Id != 0 && model.TagsId != null)
                    {
                        var removedTags = _db.NewsToTags.Where(x => x.NewsId == news.Id && !model.TagsId.Contains(x.TagsId)).ToList();
                        _db.NewsToTags.RemoveRange(removedTags);
                    }

                    _db.SaveChanges();
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return false;
            }
            
        }

        public NewsVM GetById(int? id)
        {
            var model = _db.News.Include(x => x.Translations).Include(e=>e.NewsToTags).Include(e=>e.AttachedFiles).Where(q => q.Id == id).FirstOrDefault();
            var mappedModel = Mapper.Map<News, NewsVM>(model);
            return mappedModel;
        }

        public void Delete(int id)
        {
            var news = _db.News.Where(q => q.Id == id).FirstOrDefault();
            if (news == null) return;
            news.DeleteDate = DateTime.Now;
            _db.SaveChanges();
        }

        public bool CheckSlug(string Slug)
        {
            var model = _db.News.Where(q => q.Slug == Slug).FirstOrDefault();

            return (model == null ? false : true);
        }
    }
}
