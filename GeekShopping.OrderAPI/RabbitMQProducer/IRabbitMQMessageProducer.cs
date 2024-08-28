using GeekShopping.MessageBus;

namespace GeekShopping.OrderAPI.RabbitMQProducer;

public interface IRabbitMQMessageProducer
{
    void SendMessage(BaseMessage baseMessage, string queueName);
}