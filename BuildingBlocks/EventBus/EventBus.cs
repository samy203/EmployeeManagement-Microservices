using EasyNetQ;
using EventBus.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class EventBus : IEventBus
    {
        public void Publish<T>(T message)
        {
            var bus = RabbitHutch.CreateBus("host=rabbitmq;timeout=120;username=guest;password=guest");
            bus.PubSub.Publish(message);
        }

        public void Subscribe<T>(string sub_id, Action<T> action)
        {
            var bus = RabbitHutch.CreateBus("host=rabbitmq;timeout=120;username=guest;password=guest");
            bus.PubSub.Subscribe<T>(sub_id, action);
        }
    }
}
