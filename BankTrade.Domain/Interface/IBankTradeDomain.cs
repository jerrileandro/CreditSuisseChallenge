using BankTrade.Domain.Dto;

namespace BankTrade.Domain.Interface
{
    public interface IBankTradeDomain
    {
        IList<string> Validation(InputDto inputDto);
        IList<string> ProcessData(Trade trade);
    }
}