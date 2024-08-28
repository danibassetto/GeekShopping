using GeekShopping.MessageBus;

namespace GeekShopping.PaymentAPI.RabbitMQProducer;

public interface IRabbitMQMessageProducer
{
    void SendMessage(BaseMessage baseMessage, string queueName);
}