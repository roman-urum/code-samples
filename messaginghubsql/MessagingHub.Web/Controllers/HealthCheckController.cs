using MessagingHub.Data;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Http;

namespace MessagingHub.Web.Controllers
{
    public class HealthCheckController : BaseApiController
    {
        private static DateTime started = DateTime.UtcNow;

        [HttpGet, Route("healthcheck/prtg")]
        public async Task<IHttpActionResult> PRTG()
        {
            var okay = true;
            var age = DateTime.UtcNow - WebApiApplication.ApplicationStartedUtc;

            var timer = new Stopwatch();

            timer.Start();

            Exception ex = null;

            try
            {
                using (var db = new MessagingHubDbContext())
                using (var tx = db.Database.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted))
                {
                    var reg = await db.Registrations.FirstOrDefaultAsync();
                    tx.Commit();

                    timer.Stop();
                }
            }
            catch (Exception e)
            {
                timer.Stop();

                okay = false;
                ex = e;
            }

            return Ok(new { okay = okay, ms = timer.ElapsedMilliseconds, age = age, machine = Environment.MachineName, e = ex });
        }
    }
}