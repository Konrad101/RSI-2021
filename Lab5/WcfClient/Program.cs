using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WcfCallbackServiceContract;
using WcfClient.Name;
using WcfClient.Summary;
using WcfClient.Tick;
using static System.Console;

namespace WcfClient
{
    class Program
    {
        private static void PrintInfo()
        {
            WriteLine("Konrad Hajduga 246995");
            WriteLine("Radosław Ścigała 246997");
            WriteLine(DateTime.Now);
            WriteLine(Environment.MachineName + "\n");
        }

        private static int GetNumberFromUser()
        {
            int number;
            bool correctInput;
            do
            {
                correctInput = int.TryParse(ReadLine(), out number);
                if (!correctInput)
                {
                    WriteLine("Not a number!");
                }
            } while (!correctInput);
            return number;
        }

        static void Main(string[] args)
        {
            PrintInfo();

            TickCallback tickCallback = new TickCallback();
            InstanceContext instanceContextTick = new InstanceContext(tickCallback);
            CurrencyTickProviderClient tickClient = new CurrencyTickProviderClient(instanceContextTick);

            CurrencyNameProviderClient nameClient = new CurrencyNameProviderClient("BasicHttpBinding_ICurrencyNameProvider");

            SummaryCallback summariesCallback = new SummaryCallback();
            InstanceContext instanceContextSummaries = new InstanceContext(summariesCallback);
            Summary.CurrencyResultSummariesClient summariesClient = new Summary.CurrencyResultSummariesClient(instanceContextSummaries);

            nameClient.Open();
            WriteLine("Name Client has started.");

            tickClient.Open();
            WriteLine("Tick Client has started.");

            summariesClient.Open();
            WriteLine("Summaries Client has started.");

            while (true)
            {
                WriteLine("\n1: Get currency tick");
                WriteLine("2: Get available currencies");
                WriteLine("3: Get summaries");

                Write("My choice: ");
                int choiceNumber = Convert.ToInt32(ReadLine());
                switch (choiceNumber)
                {
                    case 1:
                        {
                            Write("First currency: ");
                            string firstCurrency = ReadLine();
                            Write("Second currency: ");
                            string secondCurrency = ReadLine();

                            tickClient.GetTickAsync(firstCurrency, secondCurrency);
                            WriteLine($"# Called - GetTick for {firstCurrency} - {secondCurrency}\n");
                        }
                        break;
                    case 2:
                        {
                            Write("Currencies amount: ");
                            int currenciesAmount = GetNumberFromUser();

                            string[] currencies = nameClient.GetCurrencies(currenciesAmount);
                            WriteLine($"# Called - GetCurrenciesAsync for {currenciesAmount}\n");
                            foreach (var currency in currencies)
                            {
                                Write(currency);
                                if (currency != currencies.Last())
                                {
                                    Write(", ");
                                }
                            }
                            WriteLine("\n");
                        }
                        break;
                    case 3:
                        {
                            Write("Summaries amount: ");
                            int summariesAmount = GetNumberFromUser();

                            summariesClient.GetResultSummariesAsync(summariesAmount);
                            WriteLine($"# Called - GetResultSummaries for {summariesAmount}\n");
                        }
                        break;
                    default:
                        WriteLine("Wrong choice");
                        break;
                }
            }
        }
    }
}
