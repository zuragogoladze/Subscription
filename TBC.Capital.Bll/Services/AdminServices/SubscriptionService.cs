using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.Capital.Bll.Services.Common;
using TBC.Capital.Core.ViewModels;
using TBC.Capital.Dal.Models;
using TBC.Capital.Dal.Models.Content;
using System.Data.Entity;

namespace TBC.Capital.Bll.Services.AdminServices
{
    public class SubscriptionService : ParentService
    {
        public SubscriptionService(ApplicationDbContext _db) : base(_db)
        {
        }
        public List<SubscriberVML> GetSubscribers()
        {
            var model = _db.Subscribers.Include("SubscriberToTags.Tags").ToList();
            var mappedModel = model.Select(Mapper.Map<Subscriber, SubscriberVML>).ToList();
            return mappedModel;
        }

        public void DeleteSubscriber(int id)
        {
            var subscriber = _db.Subscribers.Where(q => q.Id == id).FirstOrDefault();
            if (subscriber == null) return;
            _db.Subscribers.Remove(subscriber);
            _db.SaveChanges();
        }
    }
}
