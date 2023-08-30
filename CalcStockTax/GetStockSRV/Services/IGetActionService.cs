using GetStockSRV.Models;

namespace GetStockSRV.Services
{
    public interface IGetActionService
    {
        public Task<List<Stocks>> DoGet();
    }
}
