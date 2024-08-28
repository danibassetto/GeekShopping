namespace GeekShopping.MessageBus;

public class BaseMessage
{
    public long Id { get; set; }
    public DateTime MessageCreationDate { get; set; }
}