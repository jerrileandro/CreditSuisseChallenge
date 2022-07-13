using BankTrade.Domain;
using BankTrade.Domain.Dto;
using BankTrade.Domain.Enum;
using BankTrade.Domain.Interface;
using BankTrade.Service;
using BankTrade.Service.Interface;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    static void Main(string[] args)
    {
        var serviceColletion = new ServiceCollection();
        ConfigureServices(serviceColletion);
        var serviceProvider = serviceColletion.BuildServiceProvider();

        var bankTradeService = serviceProvider.GetService<IBankTradeService>();

        Console.WriteLine("Starting the application");

        var inputData = new InputDto();
        var inputClientData = string.Empty;

        //Fetching screen information
        Console.WriteLine("\n Enter a reference date in format dd/mm/yyyy.");
        inputData.ReferenceDate = Console.ReadLine();
        Console.WriteLine("Enter a number of trade.");
        inputData.NumberOfTrade = Console.ReadLine();

        do
        {
            Console.WriteLine("Enter a string containing the Trade Amount, Customer Sector and Next Payment Date. Values must be separated by a space. After each enter, a new line with information can be typed., or type Q to process.");
            inputClientData = Console.ReadLine();

            if (inputClientData != null)
                if (!inputClientData.ToUpper().Equals("Q"))
                {
                    inputData.ClientData.Add(inputClientData);
                }
        }
        while (!inputClientData.ToUpper().Equals("Q"));

        //Validation
        var validationList = bankTradeService.Validation(inputData);

        if (validationList.Any())
        {
            foreach (var error in validationList)
            {
                Console.WriteLine(error);
            }
        }
        else
        {
            //Mount object
            var trade = new Trade();

            trade.ReferenceDate = DateTime.ParseExact(inputData.ReferenceDate, "MM/dd/yyyy", null);
            trade.NumberOfTrade = int.Parse(inputData.NumberOfTrade);

            foreach (var tradeElement in inputData.ClientData)
            {
                string[] elements = tradeElement.Split(' ');

                var tradeProperties = new TradeProperties();
                var clientSectorTemp = char.ToUpper(elements[1].ToLower()[0]) + elements[1].ToLower().Substring(1);

                tradeProperties.AmountValue = Double.Parse(elements[0]);
                tradeProperties.ClientSector = (ClientSectorEnum)Enum.Parse(typeof(ClientSectorEnum), clientSectorTemp);
                tradeProperties.NextPaymentDate = DateTime.ParseExact(elements[2], "MM/dd/yyyy", null);

                trade.TradeProperties.Add(tradeProperties);
            }
            
            var riskList = bankTradeService.ProcessData(trade);

            if(riskList.Any())
                foreach (var risk in riskList)
                {
                    Console.WriteLine(risk);
                }
        }
    }

    //QUESTION 2:
    //It would create a new element in the ClientSectorEnum enumerator and do the handling where the enumerator is used.It would create a boolean property on the
    //interface called IsPoliticallyExposed.It would adjust to inform this new value in the data input.When the client comes as PEP I would check if the boolean was
    //informed as true and return PEP.

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IBankTradeService, BankTradeService>()
                .AddScoped<IBankTradeDomain, BankTradeDomain>();
    }
}