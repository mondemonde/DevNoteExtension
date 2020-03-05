using DevNote.Interface;
using IntegrationEvents.Events.DevNote;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using QRP.JobFinder.API.EventHandler.IntegrationEvents.Handler.Manning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EFCoreTransactionsReceiver
{
    public class DevNoteServiceBusMessageHandler
    {
        private readonly ISubscriptionClient client;
        private readonly string connectionString;

        public IBot ApiBOT { get; set; }


        public DevNoteServiceBusMessageHandler(ISubscriptionClient client, IBot bot)
        {
            this.connectionString = connectionString;
            this.client = client;
            IBot = bot;
        }
        public async Task MessageReceivedHandler(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            var jsonMessage = Encoding.UTF8.GetString(message.Body);


            switch (message.Label)
            {
                case "DevNoteIntegrationEvent":
                    var @event = JsonConvert.DeserializeObject<DevNoteIntegrationEvent>(jsonMessage);

                    var handler = new DevNoteIntegrationEventHandler();
                    await handler.Handle(@event);
                    break;
                default:
                    Console.WriteLine(jsonMessage);
                    break;

            }

            await client.CompleteAsync(message.SystemProperties.LockToken);
        }



    }
}

