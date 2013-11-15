using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModel;
using EasyNetQ;

namespace ProducerEasy
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = RabbitHutch.CreateBus("host=localhost");

            bus.Publish(new Message() { MessageText = "That was easy!"});
        }
    }
}
