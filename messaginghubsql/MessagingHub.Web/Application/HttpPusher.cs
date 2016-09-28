using NLog;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MessagingHub.Web
{
    public static class HttpPusher
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private static ConcurrentQueue<HttpRequestMessage> queue = new ConcurrentQueue<HttpRequestMessage>();

        private static Task deliverTask = null;

        private static void deliver()
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;

            logger.Log(LogLevel.Debug, "[{0}]: start delivery of {1} queued items.", threadId, queue.Count);

            var client = new HttpClient();
            var request = null as HttpRequestMessage;

            while (queue.TryDequeue(out request))
            {
                var tag = Guid.NewGuid().ToString().ToLowerInvariant().Substring(0, 8);

                try
                {
                    var response = Task.Run(() => client.SendAsync(request)).Result;
                    logger.Log(LogLevel.Info, "[{0}] {1}: {2} {3} -- {4}", threadId, tag, request.Method, request.RequestUri, response.StatusCode);
                }
                catch (Exception e)
                {
                    logger.Log(LogLevel.Error, e, "[{0}] {1}: {2} {3} ", threadId, tag, request.Method, request.RequestUri);
                }
            }

            logger.Log(LogLevel.Debug, "[{0}]: HTTP delivery queue is empty.", threadId);
        }

        public static void Queue(HttpRequestMessage r)
        {
            queue.Enqueue(r);

            if (deliverTask != null
                && (deliverTask.Status == TaskStatus.Canceled
                    || deliverTask.Status == TaskStatus.Faulted
                    || deliverTask.Status == TaskStatus.RanToCompletion))
            {
                deliverTask = null;
            }

            if (deliverTask == null)
            {
                logger.Log(LogLevel.Debug, "[{0}]: Creating / starting delivery task.", Thread.CurrentThread.ManagedThreadId);

                deliverTask = new Task(deliver);
                deliverTask.Start();
            }

            logger.Log(LogLevel.Debug, "[{0}]: Delivery task is {1}.", Thread.CurrentThread.ManagedThreadId, deliverTask.Status);
        }
    }
}