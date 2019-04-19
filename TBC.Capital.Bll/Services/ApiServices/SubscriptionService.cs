using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBC.Capital.Bll.Services.Common;
using APIRequest = TBC.Capital.Core.ViewModels.APIRequest;
using APIResponse = TBC.Capital.Core.ViewModels.API;
using TBC.Capital.Dal.Models;
using TBC.Capital.Dal.Models.Content;
using System.Data.Entity;
using AutoMapper;
using TBC.Capital.Core.Utils;

namespace TBC.Capital.Bll.Services.ApiServices
{
    public class SubscriptionService : ParentService
    {
        public SubscriptionService(ApplicationDbContext _db) : base(_db)
        {
        }
        public void UpdateSubscriber(APIRequest.SubscriberVM model, string token = "")
        {
            try
            {
                var subscriber = new Subscriber();
                if (string.IsNullOrWhiteSpace(token))
                {
                    subscriber.Update(model);
                    subscriber.MailSendDate = DateTime.Now;
                    subscriber.Token = Helpers.Tokenizer();
                    while (_db.Subscribers.Any(x => x.Token == subscriber.Token))
                    {
                        subscriber.Token = Helpers.Tokenizer();
                    }
                    _db.Subscribers.Add(subscriber);
                }
                else
                {
                    subscriber = _db.Subscribers.Include("SubscriberToTags.Tags").FirstOrDefault(x => x.Token == token);
                    subscriber.Update(model);
                }
                subscriber.UpdateTags(model.TagsId);
                //remove tags
                if (subscriber.Id != 0)
                {
                    if (model.TagsId == null)
                    {
                        var removedTags = _db.SubscriberToTags.Where(x => x.SubscriberId == subscriber.Id).ToList();
                        _db.SubscriberToTags.RemoveRange(removedTags);
                    }
                    else
                    {
                        var removedTags = _db.SubscriberToTags.Where(x => x.SubscriberId == subscriber.Id && !model.TagsId.Contains(x.TagsId)).ToList();
                        _db.SubscriberToTags.RemoveRange(removedTags);
                    }
                }

                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public APIResponse.SubscriberVM GetSubscriber(string token)
        {
            try
            {
                var model = _db.Subscribers.Include("SubscriberToTags.Tags").FirstOrDefault(xx => xx.Token == token);
                var mappedModel = Mapper.Map<Subscriber, APIResponse.SubscriberVM>(model);
                return mappedModel;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }

        }
    }
}
