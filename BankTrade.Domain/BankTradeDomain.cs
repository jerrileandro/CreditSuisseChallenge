using BankTrade.Domain.Dto;
using BankTrade.Domain.Enum;
using BankTrade.Domain.Interface;
using BankTrade.Domain.Utility;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BankTrade.Domain
{
    public class BankTradeDomain : IBankTradeDomain
    {
        public IList<string> Validation(InputDto inputDto)
        {
            var verificationList = new List<string>();

            if (inputDto != null)
            {
                //Check if the date is valid
                var dateOk = VerifyDate(inputDto.ReferenceDate);

                if(!dateOk)
                    verificationList.Add("ERROR MESSAGE: Reference Date is inválid!");

                //Checks if the value is an integer
                int n;
                var intOk = Int32.TryParse(inputDto.NumberOfTrade, out n);

                if(!intOk)
                    verificationList.Add("ERROR MESSAGE: Number of Trade is inválid!");

                //Separates the string
                double d;
                var count = 1;

                foreach (var clientData in inputDto.ClientData)
                {
                    string[] elements = clientData.Split(' ');

                    //Checks if the elements were entered correctly
                    if(elements == null)
                        verificationList.Add("ERROR MESSAGE: The trade list is null! Line: " + count);
                    else if (elements.Count() != 3)
                        verificationList.Add("ERROR MESSAGE: The trade list is in the wrong format! Line: " + count);
                    else
                    {
                        //Checks if the first element is a double
                        var isDouble = Double.TryParse(elements[0], out d);

                        if (!isDouble)
                            verificationList.Add("ERROR MESSAGE: Trade Amount " + elements[0] + " is inválid! Line: " + count);

                        //Check if the category is correct
                        if (elements[1].ToUpper() != EnumHelper.GetDescription(ClientSectorEnum.Private).ToUpper() &&
                            elements[1].ToUpper() != EnumHelper.GetDescription(ClientSectorEnum.Public).ToUpper())
                        {
                            verificationList.Add("ERROR MESSAGE: Client Sector is inválid! Line: " + count);
                        }

                        //Checks if the third element is a valid date
                        var payDateOk = VerifyDate(elements[2]);

                        if (!payDateOk)
                            verificationList.Add("ERROR MESSAGE: Pending Payment Date is inválid! Line: " + count);
                    }

                    count++;
                }

                //Checks if the number of trades corresponds to the same value entered in the trade list.
                if (intOk)
                    if (int.Parse(inputDto.NumberOfTrade) != inputDto.ClientData.Count())
                        verificationList.Add("ERROR MESSAGE: The number of trades does not match the number of transactions reported.!");
            }
            else
            {
                verificationList.Add("ERROR MESSAGE: Reported object is null or invalid");
            }

            return verificationList;
        }

        public static bool VerifyDate(string date)
        {
            string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy" };
            DateTime dateTime;

            if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out dateTime))
            {
                return true;
            }
            else
                return false;
        }

        public IList<string> ProcessData(Trade trade)
        {
            var result = new List<string>();

            foreach (var tradeProp in trade.TradeProperties)
            {
                if (tradeProp.NextPaymentDate.AddDays(30) < trade.ReferenceDate)
                    result.Add(EnumHelper.GetDescription(RiskTypeEnum.Expired));
                else if (tradeProp.ClientSector == ClientSectorEnum.Private && tradeProp.AmountValue > 1000000)
                    result.Add(EnumHelper.GetDescription(RiskTypeEnum.HighRisk));
                else if (tradeProp.ClientSector == ClientSectorEnum.Public && tradeProp.AmountValue > 1000000)
                    result.Add(EnumHelper.GetDescription(RiskTypeEnum.MediumRisk));
                else
                    result.Add(EnumHelper.GetDescription(RiskTypeEnum.NotCategorized));
            }

            return result;
        }
    }
}