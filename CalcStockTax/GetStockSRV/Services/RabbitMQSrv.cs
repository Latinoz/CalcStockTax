using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace GetStockSRV.Services
{
    public class RabbitMQSrv : IRabbitMQSrv
    {
        public void Send(object obj)
        {
            var message = JsonSerializer.Serialize(obj);
            Send(message);
        }
        public void Send(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {               
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "stocksrv.out.queue",
                                     basicProperties: null,
                                     body: body);                
            }
        }
    }
}
