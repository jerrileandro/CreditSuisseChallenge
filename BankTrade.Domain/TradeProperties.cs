using BankTrade.Domain.Enum;
using BankTrade.Domain.Interface;

namespace BankTrade.Domain
{
    public class TradeProperties : ITrade
    {
        public double AmountValue { get; set; }
        public ClientSectorEnum ClientSector { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}