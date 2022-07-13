using BankTrade.Domain.Enum;

namespace BankTrade.Domain.Interface
{
    public interface ITrade
    {
        public double AmountValue { get; set; }
        public ClientSectorEnum ClientSector { get; set; }
        public DateTime NextPaymentDate { get; set; }
    }
}