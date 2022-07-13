namespace BankTrade.Domain
{
    public class Trade
    {
        public DateTime ReferenceDate { get; set; }
        public int NumberOfTrade { get; set; }
        public IList<TradeProperties> TradeProperties { get; set; }

        public Trade()
        {
            TradeProperties = new List<TradeProperties>();
        }
    }
}