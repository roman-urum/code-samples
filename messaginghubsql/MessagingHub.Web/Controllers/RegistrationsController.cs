using AutoMapper;
using MessagingHub.Data.Models;
using MessagingHub.Web.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

using MessagingHub.Data.Enums;

using Newtonsoft.Json;

namespace MessagingHub.Web.Controllers
{
    public class RegistrationsController : BaseApiController
    {
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<IHttpActionResult> Post([FromBody]RegistrationPostRequestModel model)
        {
            VerifyTokenForType(model.Type, model.Token);

            if (ModelState.IsValid)
            {
                var reg = await Context.Registrations.Include(r => r.Tags).SingleOrDefaultAsync(r => r.Type == model.Type && r.Token == model.Token);

                if (reg != null)
                {
                    logger.Log(LogLevel.Debug, "Update existing registration, request model: {0}", JsonConvert.SerializeObject(model));

                    UpdateRegistration(reg, model.Application, model.Device, model.Name, model.Token, model.Tags);
                }
                else
                {
                    logger.Log(LogLevel.Debug, "Create new registration, request model: {0}", JsonConvert.SerializeObject(model));

                    reg = Mapper.Map<Registration>(model);
                    reg.Application = Context.Applications.FirstOrDefault(a => a.Name == model.Application);
                    if (reg.Application == null)
                    {
                        if (reg.Type == RegistrationTypes.APN)
                        {
                            reg.Application = Context.Applications.FirstOrDefault(a => a.Name == "HH-Mobile-1-iOs");
                        }
                        else if (reg.Type == RegistrationTypes.GCM)
                        {
                            reg.Application = Context.Applications.FirstOrDefault(a => a.Name == "HH-Mobile-1-Android");
                        }
                        reg.Client = model.Application;
                    }
                    reg.Secret = Utility.CreateRngCspString();

                    // If APN, GCM, FCG, or VCS: it's verified!
                    reg.Verified = (reg.Type == RegistrationTypes.APN || reg.Type == RegistrationTypes.GCM || reg.Type == RegistrationTypes.FCG || reg.Type == RegistrationTypes.VCS);

                    Context.Registrations.Add(reg);
                }

                await Context.SaveChangesAsync();

                var response = Mapper.Map<RegistrationPostResponseModel>(reg);

                return Ok(response);
            }

            return BadRequest();
        }

        private void VerifyTokenForType(RegistrationTypes type, string token)
        {
            if (type == RegistrationTypes.FCG || type == RegistrationTypes.VCS)
            {
                // Token should be a URL
                Uri temp;
                if (!Uri.TryCreate(token, UriKind.Absolute, out temp))
                {
                    ModelState.AddModelError("Token", "Token for this type must be a valid URI.");
                }
            }
        }

        public async Task<IHttpActionResult> Put(Guid id, [FromBody]RegistrationPutRequestModel model)
        {
            logger.Debug("Start updating registration, update request {0}", JsonConvert.SerializeObject(model));

            if (ModelState.IsValid)
            {
                using (var tx = Context.Database.BeginTransaction())
                {
                    var reg = await Context.Registrations.Include(r => r.Tags).SingleOrDefaultAsync(r => r.Id == id && r.Secret == model.Secret && r.Disabled == false);

                    if (reg == null)
                        return NotFound();

                    VerifyTokenForType(reg.Type, model.Token);

                    if (ModelState.IsValid)
                    {
                        var newTags = model.Tags.ToList();
                        UpdateRegistration(reg, model.Application, model.Device, model.Name, model.Token, newTags);

                        await Context.SaveChangesAsync();

                        tx.Commit();

                        return Ok();
                    }
                }
            }

            return BadRequest();
        }

        private void UpdateRegistration(Registration reg, string application, string device, string name, string token, IEnumerable<string> tags)
        {
            reg.Application = Context.Applications.FirstOrDefault(a => a.Name == application);
            if (reg.Application == null)
            {
                if (reg.Type == RegistrationTypes.APN)
                {
                    reg.Application = Context.Applications.FirstOrDefault(a => a.Name == "HH-Mobile-1-iOs");
                }
                else if (reg.Type == RegistrationTypes.GCM)
                {
                    reg.Application = Context.Applications.FirstOrDefault(a => a.Name == "HH-Mobile-1-Android");
                }
                reg.Client = application;
            }
            reg.Device = device;
            reg.Name = name;
            reg.Token = token;

            var doomedTags = reg.Tags.ToList();

            foreach (var tag in doomedTags)
            {
                Context.Tags.Remove(tag);
                reg.Tags.Remove(tag);
            }

            foreach (var tag in tags)
            {
                reg.Tags.Add(new RegistrationTag { Value = tag });
            }
        }

        [Route(@"api/Registrations/{id:guid}")]
        public async Task<IHttpActionResult> Delete(Guid id, string secret)
        {
            logger.Debug("Start deleting registration, registrationid: {0}, secret: {1}", id, secret);

            var targetRegistrations = Context.Registrations.Where(reg => reg.Id == id && reg.Secret == secret).ToList();

            if (!targetRegistrations.Any())
            {
                return NotFound();
            }

            foreach (var registration in targetRegistrations)
            {
                Context.Registrations.Remove(registration);
            }

            await Context.SaveChangesAsync();

            return Ok();
        }         
    }
}
