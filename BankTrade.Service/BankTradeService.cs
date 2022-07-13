using BankTrade.Domain;
using BankTrade.Domain.Dto;
using BankTrade.Domain.Interface;
using BankTrade.Service.Interface;

namespace BankTrade.Service
{
    public class BankTradeService : IBankTradeService
    {
        private readonly IBankTradeDomain _ibankTradeDomain;

        public BankTradeService(IBankTradeDomain ibankTradeDomain)
        {
            _ibankTradeDomain = ibankTradeDomain;
        }

        public IList<string> Validation(InputDto inputDto)
        {
            return _ibankTradeDomain.Validation(inputDto);
        }

        public IList<string> ProcessData(Trade trade)
        {
            return _ibankTradeDomain.ProcessData(trade);
        }
    }
}