using System;
using EasyModel;
using EasyNetQ;

namespace ConsumerEasy
{
    class Program
    {
        static void Main(string[] args)
        {

            var bus = RabbitHutch.CreateBus("host=localhost");
            bus.Subscribe<Message>("my_subscription_id", msg =>
            Console.WriteLine(msg.MessageText));
        }
    }
}
