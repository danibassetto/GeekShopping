using GeekShopping.MessageBus;

namespace GeekShopping.CartAPI.RabbitMQProducer;

public interface IRabbitMQMessageProducer
{
    void SendMessage(BaseMessage baseMessage, string queueName);
}