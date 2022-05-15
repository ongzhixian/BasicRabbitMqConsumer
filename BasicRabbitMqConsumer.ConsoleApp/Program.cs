using System.Reflection;
using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string RABBITMQ_URL_CONFIGURATION_KEY = "rabbitmq:Url";

IConfiguration configuration = new ConfigurationBuilder()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
    .Build();

var rabbitMQUrl = configuration[RABBITMQ_URL_CONFIGURATION_KEY];

var exitEvent = new ManualResetEvent(false);

Console.WriteLine("Press <CTRL-C> to terminate application.");

Console.CancelKeyPress += (sender, eventArgs) =>
{
    eventArgs.Cancel = true;
    exitEvent.Set();
};

var connectionFactory = new ConnectionFactory()
{
    Uri = new Uri(rabbitMQUrl)
};

using(var connection = connectionFactory.CreateConnection())
using(var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "hello",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

    var consumer = new EventingBasicConsumer(channel);

    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
    };

    Console.WriteLine("Consuming queue");

    channel.BasicConsume(
        queue: "hello",
        autoAck: true,
        consumer: consumer);

    exitEvent.WaitOne();
}
