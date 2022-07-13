namespace BankTrade.Domain.Dto
{
    public class InputDto
    {
        public string ReferenceDate { get; set; }
        public string NumberOfTrade { get; set; }
        public IList<string> ClientData { get; set; }

        public InputDto()
        {
            ClientData = new List<string>();
        }
    }
}