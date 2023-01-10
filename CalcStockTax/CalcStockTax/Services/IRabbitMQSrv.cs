namespace GetStockSRV.Services
{
    public interface IRabbitMQSrv
    {
        void Send(object obj);
        void Send(string message);
    }
}
