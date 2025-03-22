using RabbitMQ.Client;

const string exchange = "HelloExchange";
const string queue = "HelloQueue";
const string routingKey = "HelloRoutingKey";

ConnectionFactory connectionFactory = new()
{
    HostName = "localhost"
};

using var serverConnection = await connectionFactory.CreateConnectionAsync().ConfigureAwait(false);
using var serverChannel = await serverConnection.CreateChannelAsync().ConfigureAwait(false);
await serverChannel.ExchangeDeclareAsync(exchange, ExchangeType.Direct).ConfigureAwait(false);
await serverChannel.QueueDeclareAsync(queue).ConfigureAwait(false);
await serverChannel.QueueBindAsync(queue, exchange, routingKey).ConfigureAwait(false);
