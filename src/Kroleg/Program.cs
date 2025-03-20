using RabbitMQ.Client;

const string Exchange = "HelloExchange";
const string Queue = "HelloQueue";
const string RoutingKey = "HelloRoutingKey";

ConnectionFactory connectionFactory = new()
{
    HostName = "localhost"
};

using var serverConnection = await connectionFactory.CreateConnectionAsync();
using var serverChannel = await serverConnection.CreateChannelAsync();
await serverChannel.ExchangeDeclareAsync(Exchange, ExchangeType.Direct);
await serverChannel.QueueDeclareAsync(Queue);
await serverChannel.QueueBindAsync(Queue, Exchange, RoutingKey);
