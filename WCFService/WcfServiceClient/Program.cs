using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WcfServiceClient.ServiceReference;
using WcfServiceLibrary.APIConnection;
using static System.Console;

namespace WcfServiceClient
{
    // binding: WSHttpBinding, BasicHttpBinding
    // konfiguracja dwumaszynowa
    // usluga oferuje: 
    // 1. pobranie n pierwszych dostępnych kryptowalut w API
    // 2. pobranie aktualnych wartości kupna/sprzedaży z danej pary kryptowalutowej
    // 3. pobranie n pierwszych dziennych podsumowań dla par kryptowalut

    class Program
    {
        static void PrintInfo()
        {
            WriteLine("Konrad Hajduga, 246995");
            WriteLine("Radosław Ścigała, 246997");
            WriteLine(Environment.MachineName);
            WriteLine(DateTime.Now);
            WriteLine(Environment.UserName);
            var hostName = Dns.GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            var ipAdresses = hostEntry.AddressList;
            WriteLine($"IP address: http://{ ipAdresses[ipAdresses.Length - 1]}\n");
        }

        static void Main(string[] args)
        {
            PrintInfo();

            //Krok 1: Utworzenie instancji WCF proxy.
            CryptoCurrencyClient klient1 = new CryptoCurrencyClient("WSHttpBinding_ICryptoCurrency");
            CryptoCurrencyClient klient2 = new CryptoCurrencyClient("BasicHttpBinding_ICryptoCurrency");
            CryptoCurrencyClient klient3 = new CryptoCurrencyClient("mojEndpoint3");

            TestClient(klient1, "WSHttpBinding");
            TestClient(klient2, "BasicHttpBinding");
            TestClient(klient3, "mojEndpoint3");
        }

        static void TestClient(CryptoCurrencyClient client, string clientName="")
        {
            WriteLine($"Klient {clientName}: ");
            WriteLine($"Metoda pobierania n pierwszych dostępnych walut: ");
            int n = getN();
            string[] currencies = client.GetCurrencies(n);
            foreach(var currency in currencies)
            {
                WriteLine(currency);
            }

            WriteLine($"Metoda pobierania aktualnej wartości: ");
            WriteLine("Wprowadź pierwszą kryptowalutę: ");
            string firstCurrency = ReadLine();
            WriteLine("Wprowadź drugą kryptowalutę: ");
            string secondCurrency = ReadLine();
            CurrencyTick tick = client.GetTick(firstCurrency, secondCurrency);
            if (tick != null)
            {
                WriteLine(tick);
            } else
            {
                WriteLine("Podana para kryptowalut nie jest dostępna w API.");
            }

            Write($"Metoda do pobierania podsumowań z ostatnich 24 godzin dla wszystkich rynków kryptowalut: ");
            n = getN();
            var summaries = client.GetResultSummaries(n);
            foreach (var summary in summaries)
            {
                WriteLine(summary);
            }
            client.Close();
        }

        static int getN()
        {
            int n;
            bool success;
            do
            {
                Write("Podaj n: ");
                string numberStr = ReadLine();
                success = int.TryParse(numberStr, out n);
            } while (!success);

            return n;
        }
    }
}
