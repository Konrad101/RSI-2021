using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfServiceLibrary.APIConnection;
using static System.Console;

namespace WcfServiceLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class CryptoCurrencyProvider : ICryptoCurrency
    {
        public List<string> GetCurrencies(int currenciesAmount)
        {
            WriteLine($"Get {currenciesAmount} first crypto currencies");
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(
                string.Format("https://api.bittrex.com/api/v1.1/public/getcurrencies"));
            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            CurrenciesHandler tick = JsonConvert.DeserializeObject<CurrenciesHandler>(jsonString);

            List<string> currencies = new List<string>();
            int index = 0;
            foreach (var currency in tick.result)
            {
                currencies.Add(currency.Currency);
                if (++index == currenciesAmount)
                {
                    break;
                }
            }

            return currencies; 
        }

        public CurrencyTick GetTick(string firstCurrency, string secondCurrency)
        {
            WriteLine($"Get tick: {firstCurrency}, {secondCurrency}");
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(
                 string.Format($"https://api.bittrex.com/api/v1.1/public/getticker?market={firstCurrency}-{secondCurrency}"));
            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            CurrencyTick tick = JsonConvert.DeserializeObject<CurrencyTick>(jsonString);
            if (!tick.success)
            {
                WriteLine("\nCurrency pair not found");
                return null;
            }

            WriteLine($"\nCurrent tick on {firstCurrency}-{secondCurrency}:");
            WriteLine(tick);
            return tick;
        }

        public List<ResultSummary> GetResultSummaries(int summariesAmount)
        {
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(
               string.Format("https://api.bittrex.com/api/v1.1/public/getmarketsummaries"));
            WebReq.Method = "GET";

            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();

            string jsonString;
            using (Stream stream = WebResp.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                jsonString = reader.ReadToEnd();
            }

            Summary summary = JsonConvert.DeserializeObject<Summary>(jsonString);
            WriteLine("summary: " + summary.success);
            WriteLine("message: " + summary.message);
            List<ResultSummary> summaries = new List<ResultSummary>();
            for (int i = 0; i < summariesAmount; i++)
            {
                summaries.Add(summary.result[i]);
                WriteLine(summary.result[i]);
            }

            return summaries;
        }
    }
}
