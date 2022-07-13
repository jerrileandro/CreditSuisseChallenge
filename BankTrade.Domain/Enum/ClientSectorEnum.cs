using System.ComponentModel;

namespace BankTrade.Domain.Enum
{
    public enum ClientSectorEnum
    {
        [Description("Private")]
        Private = 0,
        [Description("Public")]
        Public = 1
    }
}