using System.ComponentModel;

namespace BankTrade.Domain.Enum
{
    public enum RiskTypeEnum
    {
        [Description("MEDIUMRISK")]
        MediumRisk = 0,
        [Description("HIGHRISK")]
        HighRisk = 1,
        [Description("EXPIRED")]
        Expired = 2,
        [Description("NOTCATEGORIZED")]
        NotCategorized = 3
    }
}