using MessagingHub.Data;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Import
{
    class Program
    {
        private static MessagingHubDbContext context = new MessagingHubDbContext();

        static void Main(string[] args)
        {
            var customer = Convert.ToInt32(ConfigurationManager.AppSettings["Customer"]);

            if (customer < 1)
                throw new ApplicationException("Customer must be >= 1");

            var tag = "vcs-customer-" + customer;

            var endpoint = new Uri(ConfigurationManager.AppSettings["Endpoint"]);
            var key = ConfigurationManager.AppSettings["Key"];
            var database = ConfigurationManager.AppSettings["Database"];

            var documents = new List<RegistrationDto>();

            using (var client = new DocumentClient(endpoint, key))
            {
                Console.WriteLine("Querying DocumentDB for all registrations...");
                var docDb = client.CreateDatabaseQuery().Where(d => d.Id == database).AsEnumerable().FirstOrDefault();
                var docColl = client.CreateDocumentCollectionQuery(docDb.CollectionsLink).Where(x => x.Id == "registrations").AsEnumerable().FirstOrDefault();
                documents = client.CreateDocumentQuery<RegistrationDto>(docColl.DocumentsLink).AsEnumerable().ToList();
                Console.WriteLine("Found " + documents.Count + " registrations");
            }

            Console.WriteLine("Filtering registrations to those tagged [" + tag + "]");
            documents = documents.Where(d =>
                d.Type == RegistrationType.GCM &&
                d.Tags.Contains(tag, StringComparer.InvariantCultureIgnoreCase)
            ).ToList();
            Console.WriteLine(documents.Count + " registrations tagged [" + tag + "]");

            var inserted = 0;
            var duplicate = 0;
            var error = 0;
            var total = 0;

            var alreadyImported = new List<string>();

            using (var db = new MessagingHubDbContext())
            using (var tx = db.Database.BeginTransaction())
            {
                var importedRegistrations = db.Registrations.Include(q => q.Tags).ToList();
                alreadyImported = importedRegistrations.Where(q => q.Token != null).Select(q => string.Concat(q.Type.ToString(), '-', q.Token).ToLowerInvariant()).ToList();

                foreach (var doc in documents)
                {
                    var addedKey = string.Concat(doc.Type.ToString(), '-', doc.Token).ToLowerInvariant();

                    if (!alreadyImported.Contains(addedKey))
                    {
                        var converted = ConvertRegistration(doc);

                        try
                        {
                            db.Registrations.Add(converted);
                            db.SaveChanges();
                            alreadyImported.Add(addedKey);

                            inserted++;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine();
                            Console.WriteLine("EXCEPTION: " + JsonConvert.SerializeObject(e));
                            Console.WriteLine();

                            error++;
                        }


                        total = inserted + duplicate + error;
                    }
                    else
                    {
                        duplicate++;
                    }

                    Console.Write(total + " processed (" + inserted + " inserted, " + duplicate + " skipped, " + error + " errors)\r");
                }

                Console.WriteLine();
                Console.WriteLine();

                if (inserted > 0)
                {
                    Console.WriteLine("Type \"COMMIT\" to commit " + inserted + " records or anything else to quit.");

                    var commit = Console.ReadLine();

                    if (commit == "COMMIT")
                    {
                        Console.WriteLine("Committing...");
                        tx.Commit();
                    }
                    else
                    {
                        Console.WriteLine("Rolling back...");
                        tx.Rollback();
                    }
                }
            }
        }

        private static MessagingHub.Data.Models.Registration ConvertRegistration(RegistrationDto doc)
        {
            var reg = new MessagingHub.Data.Models.Registration();

            reg.Id = doc.Id;
            reg.Name = doc.Name;

            var applicationEntity = context.Applications.FirstOrDefault(a => a.Name == doc.Application)
                        ?? context.Applications.First(a => a.Name == "HH-Mobile-1-iOS");

            reg.Application = applicationEntity;
            reg.Device = doc.Device;
            reg.Secret = doc.Secret;
            reg.Token = doc.Token;
            reg.Type = (MessagingHub.Data.Enums.RegistrationTypes)(int)doc.Type;
            reg.VerificationCode = doc.VerificationCode;
            reg.Verified = doc.Verified;
            reg.Disabled = doc.Disabled;
            reg.Tags = doc.Tags.Select(t => new MessagingHub.Data.Models.RegistrationTag { Value = t }).ToList();

            return reg;
        }
    }
}
