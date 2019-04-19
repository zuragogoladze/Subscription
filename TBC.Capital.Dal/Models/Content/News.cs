using TBC.Capital.Core.Utils;
using TBC.Capital.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace TBC.Capital.Dal.Models.Content
{
    public class News
    {
        public News()
        {
            Translations = new List<NewsTranslations>();
            NewsToTags = new List<NewsToTags>();
            AttachedFiles = new List<AttachedFiles>();
        }
        [Key]
        public int Id { get; set; }
        public string Image { get; set; }
        public string CoverImage { get; set; }
        public string ShareImage { get; set; }

        [Index]
        [StringLength(100)]
        public string Slug { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? DeleteDate { get; set; }
        public DateTime PublishDate { get; private set; }

        public List<NewsTranslations> Translations { get; set; }
        public List<AttachedFiles> AttachedFiles { get; set; }
        public List<NewsToTags>NewsToTags { get; set; }

        public string GetTitle()
        {
            return Translations.Where(q => q.LanguageCode == Settings.CurrentLang).Select(q => q.Title).FirstOrDefault();
        }

        public string GetDescription()
        {
            return Translations.Where(q => q.LanguageCode == Settings.CurrentLang).Select(q => q.Description).FirstOrDefault();
        }

        public int[] GetTagsId()
        {
            return NewsToTags.Where(q => q.NewsId == this.Id).Select(q => q.TagsId).ToArray();
        }
        public List<Core.ViewModels.API.TagsVM> GetTags() {
            return this.NewsToTags.Select(x => new Core.ViewModels.API.TagsVM
            {
                Id = x.Tags.Id,
                Name = x.Tags.Translations.FirstOrDefault(z => z.LanguageCode == Settings.CurrentLang)?.Name,
            }).ToList();
        }

        public string[] GetGallery()
        {
            return new string[] { };
        }


        public void Update(NewsVM model)
        {
            this.Image = model.Image;
            this.CoverImage = model.CoverImage;
            this.ShareImage = model.ShareImage;
            this.Slug = model.Slug;
            this.IsPublished = model.IsPublished;
            this.PublishDate = model.PublishDate;
        }

        public void UpdateTranslations(List<NewsVMT> translations)
        {
            foreach (var translation in translations)
            {
                if (this.Translations.Any(q => q.LanguageCode == translation.LanguageCode))
                {
                    var obj = this.Translations.FirstOrDefault(q => q.LanguageCode == translation.LanguageCode);
                    obj.Title = translation.Title;
                    obj.Description = translation.Description;
                }
                else
                {
                    this.Translations.Add(new NewsTranslations
                    {
                        LanguageCode = translation.LanguageCode,
                        Title = translation.Title,
                        Description = translation.Description,
                    });
                }
            }
        }
        public void UpdateTags(int[] TagsId)
        {
            if (TagsId == null || TagsId.Count() == 0) return;
            foreach (var item in TagsId)
            {
                if (this.NewsToTags.Any(q => q.TagsId == item)) continue;
                this.NewsToTags.Add(new NewsToTags
                {
                    TagsId = item,
                });
            }
        }
        public void UpdateFiles(List<AttachedFilesVM> files)
        {
            if (files == null || files.Count() == 0) return;
            foreach (var item in files)
            {
                if (this.AttachedFiles.Any(q => q.Id == item.Id))
                {
                    var obj = this.AttachedFiles.FirstOrDefault(q => q.Id == item.Id);
                    obj.Order = item.Order;
                    obj.UpdateTranslations(item.Translations);
                }
                else
                {
                    if (item.Translations.Any(x => !string.IsNullOrWhiteSpace(x.File)))
                    {
                        var newFile = new AttachedFiles()
                        {
                            Order = item.Order
                        };
                        newFile.UpdateTranslations(item.Translations);
                        this.AttachedFiles.Add(newFile);
                    }
                }
            }
        }
    }

    public class NewsTranslations : BaseTranslation
    {
        [ForeignKey("News")]
        public int NewsId { get; set; }
        public News News { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

}
