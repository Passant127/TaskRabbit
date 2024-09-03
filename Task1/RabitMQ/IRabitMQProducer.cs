namespace Task1.RabitMQ;

public interface IRabitMQProducer
{
    public void SendMessage<T>(T message);
}