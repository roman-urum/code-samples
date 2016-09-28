using System;
using System.Threading.Tasks;
using VitalsService.Domain.EsbEntities;
using VitalsService.DomainLogic.Services.Interfaces;
using System.Configuration;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace VitalsService.DomainLogic.Services.Implementations
{
    /// <summary>
    /// Esb.
    /// </summary>
    public class Esb : IEsb
    {
        const string SubscriptionName = "All";

        //private Logger log = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the <see cref="Esb"/> class.
        /// </summary>
        public Esb()
        {
            //var subscribeClient = SubscriptionClient.CreateFromConnectionString(ConnectionString, TopicName, SubscriptionName);

            //log.Log(LogLevel.Info, "Subscriber started");

            //subscribeClient.OnMessageAsync(OnReceiveMessageCallback);
        }

        /// <summary>
        /// Called when [receive message callback].
        /// </summary>
        /// <param name="brokeredMessage">The brokered message.</param>
        /// <returns></returns>
        private async Task OnReceiveMessageCallback(BrokeredMessage brokeredMessage)
        {
            var body = brokeredMessage.GetBody<MeasurementMessage>();

            var msgString = JsonConvert.SerializeObject(body);

            var logLine = string.Format("Subscriber message received: {0}", msgString);

            //this.log.Log(LogLevel.Info, logLine);

            await brokeredMessage.CompleteAsync();
        }

        /// <summary>
        /// Publishes the measurement.
        /// </summary>
        /// <param name="measurementMessage">The measurement message.</param>
        /// <returns></returns>
        public async Task PublishMeasurement(MeasurementMessage measurementMessage)
        {
            try
            {
                var connectionString =
                    ConfigurationManager.ConnectionStrings["MeasurementESBConnectionString"].ConnectionString;

                var publishClient = TopicClient.CreateFromConnectionString(
                    connectionString,
                    Settings.MeasurementESBTopicName);

                var msg = new BrokeredMessage(measurementMessage);
                var measurementMsgJson = JsonConvert.SerializeObject(measurementMessage);

                msg.Properties["MeasurementJSON"] = measurementMsgJson;

                await publishClient.SendAsync(msg);

                string logMessage = string.Format("Message sent to ESB: {0}", measurementMsgJson);

                //this.log.Log(LogLevel.Info, logMessage);
            }
            catch (Exception ex)
            {
                //todo put exception error to log
                throw ex;
            }

        }

        /// <summary>
        /// Publishes the health session.
        /// </summary>
        /// <param name="healthSession">The health session.</param>
        /// <returns></returns>
        public async Task PublishHealthSession(object healthSession)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["HealthSessionESBConnectionString"].ConnectionString;

                var publishClient = TopicClient.CreateFromConnectionString(connectionString, Settings.HealthSessionESBTopicName);

                var msg = new BrokeredMessage(healthSession);

                var measurementMsgJson = JsonConvert.SerializeObject(healthSession);

                msg.Properties["HealthSessionJSON"] = measurementMsgJson;

                await publishClient.SendAsync(msg);
            }
            catch (Exception ex)
            {
                //todo log exception message
                throw ex;
            }
        }
    }
}