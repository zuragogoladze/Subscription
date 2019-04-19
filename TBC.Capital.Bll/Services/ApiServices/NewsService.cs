using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.Capital.Bll.Services.Common;
using TBC.Capital.Dal.Models;
using System.Data.Entity;
using TBC.Capital.Core.ViewModels.API;
using TBC.Capital.Dal.Models.Content;
using AutoMapper;
using TBC.Capital.Core.Utils;

namespace TBC.Capital.Bll.Services.ApiServices
{
    public class NewsService : ParentService
    {
        public NewsService(ApplicationDbContext db) : base(db)
        {
        }
        public NewsVM GetNewsBySlug(string Slug)
        {
            try
            {
                var model = _db.News.Include(e => e.Translations).Include("NewsToTags.Tags").Include(e=>e.AttachedFiles).FirstOrDefault(e => e.Slug == Slug);
                var mappedModel = Mapper.Map<News, NewsVM>(model);
                mappedModel.AttachedFiles = mappedModel.AttachedFiles.OrderBy(e => e.Order).ToList();
                return mappedModel;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public PaginationNewsVM FilterNews(List<int> years, List<int> monthes, List<int> tags, int page, int limit, bool orderByDescending, string excludeSlug)
        {
            try
            {
                var model = _db.News.Include(e => e.Translations).Include(e => e.AttachedFiles).Include("NewsToTags.Tags").Where(e => e.IsPublished && !e.DeleteDate.HasValue && e.Slug != excludeSlug);

                //start filtering content
                if (years != null && years.Count > 0)
                {
                    model = model.Where(e => years.Contains(e.PublishDate.Year));
                }
                if (monthes != null && monthes.Count > 0)
                {
                    model = model.Where(e => monthes.Contains(e.PublishDate.Month));
                }
                if (tags != null && tags.Count > 0)
                {
                    model = model.Where(x => x.NewsToTags.Any(xx => tags.Contains(xx.TagsId)));
                }
                if (orderByDescending)
                {
                    model = model.OrderByDescending(e => e.PublishDate);
                }
                else
                {
                    model = model.OrderBy(e => e.PublishDate);
                }

                //pagination
                model = model
                    .Skip(limit * page)
                    .Take(limit);
                var pagination = Helpers.GetPaginationData(model, page, limit);
                var modelList = model.ToList();
                //response
                var mappedModel = modelList.Select(Mapper.Map<News, NewsVM>).ToList();
                var response = new PaginationNewsVM()
                {
                    Pagination = pagination,
                    News = mappedModel
                };

                return response;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public List<TagsVM> GetNewsTags()
        {
            try
            {
                var model = _db.News.Include("NewsToTags.Tags").Where(e => e.IsPublished && !e.DeleteDate.HasValue).SelectMany(e => e.NewsToTags.Select(x => x.Tags)).Distinct().ToList();
                var mappedModel = model.Select(Mapper.Map<Tags, TagsVM>).ToList();
                return mappedModel;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
            
        }

        public List<int> GetNewsYears()
        {
            var model = _db.News.Where(e => e.IsPublished && !e.DeleteDate.HasValue).Select(e => e.PublishDate.Year).Distinct();
            return model.ToList();
        }
        public List<int> GetNewsMonthes()
        {
            var model = _db.News.Where(e => e.IsPublished && !e.DeleteDate.HasValue).Select(e => e.PublishDate.Month).Distinct();
            return model.ToList();
        }

        public List<SearchVM> Search(string key)
        {
            try
            {
                var model = _db.News.Include("Translations")
                .Where(x =>
                !x.DeleteDate.HasValue && x.IsPublished &&
                x.Translations.Any(xx => xx.Title.Contains(key) || xx.Description.Contains(key))).ToList();
                var mappedModel = model.Select(Mapper.Map<News, SearchVM>).ToList();
                return mappedModel;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw (ex);
            }
        }
    }
}
