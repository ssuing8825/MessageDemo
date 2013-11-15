using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace Consumer
{
    class Program
    {
        private static IModel model;
        private static IConnection connection;
        private const string exchangeName = "TRINUGExchange";
        private const string host = "LocalHost";
        private static string queueName;
        private static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Connect();

           // ConfigureQueue();

             ConfigureQueueSubscribe();

            Consume();
        }

        private static void Connect()
        {
            Console.WriteLine("Initializing connection with {0}...", host);
            var connectionFactory = new ConnectionFactory { HostName = host };
            connection = connectionFactory.CreateConnection();
            Console.WriteLine("Creating Channel...");
            model = connection.CreateModel();
        }

        private static void ConfigureQueueSubscribe()
        {
            queueName = model.QueueDeclare().QueueName;
            model.QueueBind(queueName, exchangeName, "");
        }

        private static void ConfigureQueue()
        {
            queueName = "TRINUG";
        }

        private static void Consume()
        {
            QueueingBasicConsumer consumer = new QueueingBasicConsumer(model);

            model.BasicConsume(queueName, false, consumer);

            while (true)
            {
                try
                {
                    var e = (RabbitMQ.Client.Events.BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    // ... process the message
                    Console.WriteLine(System.Text.Encoding.UTF8.GetString(e.Body));

                   // System.Threading.Thread.Sleep(200);
                    model.BasicAck(e.DeliveryTag, false);
                }
                catch (OperationInterruptedException ex)
                {
                    // The consumer was removed, either through
                    // channel or connection closure, or through the
                    // action of IModel.BasicCancel().
                    break;
                }
            }
        }


    }
}
