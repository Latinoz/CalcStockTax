using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace CalcTaxSRV.Services
{
    public class RabbitMQListenSrv
    {
        public void Receive()
        {            
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    //Здесь метод расчета налога
                    Calc calc = new Calc();
                    string result = calc.GetStocksCalc(message);

                    //Здесь получить рузультат Calc и отправить в очередь taxsrv.out.queue
                    Send(result);
                    
                    Console.WriteLine(" [x] Current Tax NDFL for pay: {0}", result);

                };
                channel.BasicConsume(queue: "stocksrv.out.queue",
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                
            }
        }

        public void Send(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "taxsrv.out.queue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
