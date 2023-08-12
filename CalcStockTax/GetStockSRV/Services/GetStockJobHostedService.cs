using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Channels;
using GetStockSRV.Controllers;

namespace GetStockSRV.Services
{
    public class GetStockJobHostedService : BackgroundService
    {   
        public GetStockJobHostedService() 
        { 

        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            

            return base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            
            
            await base.StopAsync(cancellationToken);            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //stoppingToken.ThrowIfCancellationRequested();            

            await Task.CompletedTask;
        }
    }
}
