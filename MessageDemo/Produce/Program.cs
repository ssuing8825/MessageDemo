using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Produce
{
    class Program
    {

        private static IModel model;
        private static IConnection connection;
        private const string exchangeName = "TRINUGExchange";
        private const string host = "LocalHost";


        static void Main(string[] args)
        {
            Connect();

            SendMessage();

            TearDown();

        }

        private static void SendMessage()
        {
            var stop = false;
            int i = 1;

            while (!stop)
            {
                IBasicProperties basicProperties = model.CreateBasicProperties();
                model.BasicPublish(exchangeName, string.Empty, basicProperties, Encoding.UTF8.GetBytes("Hello Trinug " + i.ToString()));
                Console.WriteLine("Wrote {0} messages...", i);
                i++;

               // System.Threading.Thread.Sleep(50);

                //var readLine = Console.ReadLine();
                //if (!string.IsNullOrEmpty(readLine))
                //{
                //    var answer = readLine[0];
                //    stop = (answer == 'q' || answer == 'Q');
                //}
            }
        }

        private static void Connect()
        {

            Console.WriteLine("Initializing connection with {0}...", host);
            var connectionFactory = new ConnectionFactory { HostName = host };
            connection = connectionFactory.CreateConnection();
            Console.WriteLine("Creating Channel...");
            model = connection.CreateModel();

        }





        private static void TearDown()
        {
            model.Close();
            connection.Close();
        }
    }
}
