using BankTrade.Domain;
using BankTrade.Domain.Dto;

namespace BankTrade.Service.Interface
{
    public interface IBankTradeService
    {
        IList<string> Validation(InputDto inputDto);

        IList<string> ProcessData(Trade trade);
    }
}