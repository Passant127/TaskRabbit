namespace Employee.RabitMQ;
using RabbitMQ.Client;
using System.Text;

public class PublishSalaryUpdateMessage
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public PublishSalaryUpdateMessage()
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: "salary_update_exchange", type: ExchangeType.Fanout);
    }

    public void PublishSalaryUpdate(int employeeId, decimal newSalary)
    {
        var message = $"{employeeId}:{newSalary}";
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "salary_update_exchange",
                              routingKey: "",
                              basicProperties: null,
                              body: body);
        Console.WriteLine(" [x] Sent {0}", message);
    }
}
