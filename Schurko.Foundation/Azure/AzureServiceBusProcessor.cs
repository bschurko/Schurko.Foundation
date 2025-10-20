using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Schurko.Foundation.Azure
{
    public class AzureServiceBusProcessor
    {
        private string _connectionString;
        private string _queueName;

        public AzureServiceBusProcessor(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
        }



        public async Task PublishdMessage<T>(T message)
        {
            ServiceBusClient _client = null;
            ServiceBusSender _sender = null;


            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets

            };

            try
            {
                _client = new ServiceBusClient(_connectionString, clientOptions);
                _sender = _client.CreateSender(_queueName);
                ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync().ConfigureAwait(false);

                string msg = JsonConvert.SerializeObject(message);

                if (!messageBatch.TryAddMessage(new ServiceBusMessage(msg)))
                {
                    // if it is too large for the batch
                    throw new Exception($"The message is too large to fit in the batch.");
                }


                // Use the producer client to send the batch of messages to the Service Bus queue
                await _sender.SendMessagesAsync(messageBatch).ConfigureAwait(false);
                Console.WriteLine($"messages has been published to the queue.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.ToString());
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await _sender.DisposeAsync();
                await _client.DisposeAsync();
            }

        }

        public async Task<T> ConsumeMessage<T>()
        {
            ServiceBusClient client;

            ServiceBusProcessor processor;

            var clientOptions = new ServiceBusClientOptions()
            {
                TransportType = ServiceBusTransportType.AmqpWebSockets
            };
            client = new ServiceBusClient(_connectionString, clientOptions);

            // create a processor that we can use to process the messages
            processor = client.CreateProcessor(_queueName, new ServiceBusProcessorOptions());

            try
            {

                // add handler to process messages
                var tcs = new TaskCompletionSource<T>();
                T message = default(T);
                processor.ProcessMessageAsync += async (ProcessMessageEventArgs args) =>
                {
                    string body = args.Message.Body.ToString();

                    Console.WriteLine($"Received: {body}");

                    message = JsonConvert.DeserializeObject<T>(body) ?? default(T);


                    await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

                    tcs.SetResult(message!);
                };

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                T result = await tcs.Task;

                await processor.StopProcessingAsync().ConfigureAwait(false);

                return result;

            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }
        private async Task<T> MessageHandler<T>(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();

            Console.WriteLine($"Received: {body}");

            T? msg = JsonConvert.DeserializeObject<T>(body) ?? default(T);

            // complete the message. message is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);

            return msg;
        }

        // handle any errors when receiving messages
        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }
    }
}
