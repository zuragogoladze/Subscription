using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.Capital.Core.Utils;
using TBC.Capital.Core.ViewModels.APIRequest;

namespace TBC.Capital.Dal.Models.Content
{
    public class Subscriber
    {
        public Subscriber()
        {
            CreateDate = DateTime.Now;
            SubscriberToTags = new List<SubscriberToTags>();
        }
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [StringLength(250)]
        [Index(IsUnique = true)]
        public string Email { get; set; }
        public bool IsSubscribedToResearches { get; set; }
        public int SubscriptionTime { get; set; }
        [Index(IsUnique = true)]
        [StringLength(250)]
        public string Token { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime MailSendDate { get; set; }
        public List<SubscriberToTags> SubscriberToTags { get; set; }

        public List<Core.ViewModels.API.TagsVM> GetTags()
        {
            return this.SubscriberToTags.Select(x => new Core.ViewModels.API.TagsVM
            {
                Id = x.Tags.Id,
                Name = x.Tags.Translations.FirstOrDefault(z => z.LanguageCode == Settings.CurrentLang)?.Name,
            }).ToList();
        }

        public int[] GetTagsId()
        {
            return SubscriberToTags.Where(q => q.SubscriberId == this.Id).Select(q => q.TagsId).ToArray();
        }

        public void Update (SubscriberVM model)
        {
            this.Email = model.Email == null ? this.Email : model.Email;
            this.IsSubscribedToResearches = model.IsSubscribedToResearches;
            this.SubscriptionTime = model.SubscriptionTime == 0 ? this.SubscriptionTime : model.SubscriptionTime;
        }
        public void UpdateTags(int[] TagsId)
        {
            if (TagsId == null || TagsId.Count() == 0) return;
            foreach (var item in TagsId)
            {
                if (this.SubscriberToTags.Any(q => q.TagsId == item)) continue;
                this.SubscriberToTags.Add(new SubscriberToTags
                {
                    TagsId = item,
                });
            }
        }
    }
}
