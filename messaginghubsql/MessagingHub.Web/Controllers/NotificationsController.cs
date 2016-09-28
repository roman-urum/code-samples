using MessagingHub.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PushSharp;
using PushSharp.Android;
using PushSharp.Apple;
using Newtonsoft.Json;
using NLog;
using System.Text;
using MessagingHub.Data.Models;
using MessagingHub.Data;

namespace MessagingHub.Web.Controllers
{
    public class NotificationsController : BaseApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<IHttpActionResult> Post(NotificationPostModel model)
        {
            logger.Debug("Post notification started");

            var notified = new List<Guid>();
            var notNotified = new List<Guid>();

            if (ModelState.IsValid)
            {
                List<Registration> registrations;

                using (var db = new MessagingHubDbContext())
                {
                    using (var tx = db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                    {
                        registrations = await QueryActiveRegistrations(db, model).ToListAsync();
                    }
                }

                logger.Debug("Found {0} registrations by types: ({1}); tags: ({2}); allTypes: {3}; notification request: {4}",
                             registrations.Count,
                             model.Types.Select(t => t.ToString()).Aggregate((t1, t2) => t1 + ", " + t2),
                             model.Tags.Select(t => t.ToString()).Aggregate((t1, t2) => t1 + ", " + t2),
                             model.AllTags,
                             JsonConvert.SerializeObject(model));

                foreach (var reg in registrations)
                {
                    var tag = Guid.NewGuid().ToString().ToLowerInvariant().Substring(0, 8);

                    try
                    {
                        QueueNotification(model, reg, tag);
                        notified.Add(reg.Id);
                    }
                    catch (Exception e)
                    {
                        logger.Log(NLog.LogLevel.Error, e, "Exception queueing notification.");
                        notNotified.Add(reg.Id);
                    }
                }

                return Ok(new { notified = notified, notNotified = notNotified });
            }

            return BadRequest();
        }

        private void QueueNotification(NotificationPostModel model, Registration reg, string tag)
        {
            switch (reg.Type)
            {
                case Data.Enums.RegistrationTypes.APN:
                    {
                        logger.Log(LogLevel.Debug, "Queueing AppleNotification to {0} with tag = {1}, application = {2}. NotificationPostModel: {3} ", reg.Token, tag, reg.Application.Name, JsonConvert.SerializeObject(model));

                        var notification = new AppleNotification(reg.Token)
                            .WithAlert(model.Message)
                            .WithBadge(0)
                            .WithContentAvailable(1)
                            .WithCustomItem("data", model.Data)
                            .WithTag(tag);
                        
                        WebApiApplication.MobilePusher.Queue(notification, reg.Application.Name);

                        break;
                    }
                case Data.Enums.RegistrationTypes.VCS:
                case Data.Enums.RegistrationTypes.FCG:
                    {
                        var request = new HttpRequestMessage(HttpMethod.Post, reg.Token);
                        request.Content = new StringContent(JsonConvert.SerializeObject(model.Data), Encoding.UTF8, "application/json");

                        logger.Log(LogLevel.Debug, "Queueing HttpRequest to {0} with tag {1}", reg.Token, tag);

                        HttpPusher.Queue(request);

                        break;
                    }
                case Data.Enums.RegistrationTypes.GCM:
                    {
                        var json = JsonConvert.SerializeObject(model.Data);

                        var notification = new GcmNotification()
                            .ForDeviceRegistrationId(reg.Token)
                            .WithJson(json)
                            .WithTag(tag);

                        logger.Log(LogLevel.Debug, "Queueing GcmNotification to {0} with tag {1}", reg.Token, tag);

                        WebApiApplication.MobilePusher.Queue(notification, reg.Application.Name);

                        break;
                    }
                case Data.Enums.RegistrationTypes.SMS:
                    break;
            }
        }

        private IQueryable<Registration> QueryActiveRegistrations(MessagingHubDbContext context, NotificationPostModel model)
        {
            var regs = context
                .Registrations
                .Include(q => q.Tags)
                .Include(q => q.Application)
                .Where(q => !q.Disabled)
                .Where(q => q.Verified);

            if (model.Types != null && model.Types.Any())
            {
                regs = regs.Where(q => model.Types.Contains(q.Type));
            }

            var distinctTags = model.Tags.Distinct().ToList();

            if (distinctTags.Any())
            {
                if (model.AllTags)
                {
                    regs = regs.Where(r => r.Tags.Select(t => t.Value).Distinct().Intersect(distinctTags).Count() == distinctTags.Count());
                }
                else
                {
                    regs = regs.Where(r => r.Tags.Any(t => distinctTags.Contains(t.Value)));
                }
            }

            return regs;
        }
    }
}