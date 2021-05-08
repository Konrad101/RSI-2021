using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using WcfCallbackServiceContract.APIConnection;
using static System.Console;

namespace WcfCallbackServiceContract
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CurrencyTickProvider : ICurrencyTickProvider
    {
        ICurrencyTickProviderCallback callback;

        public CurrencyTickProvider()
        {
            callback = OperationContext.Current.GetCallbackChannel<ICurrencyTickProviderCallback>();
        }

        public void GetTick(string firstCurrency, string secondCurrency)
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
            }
            else
            {
                WriteLine($"\nCurrent tick on {firstCurrency}-{secondCurrency}:");
                WriteLine(tick);
                Thread.Sleep(5000);
                callback.GetTickResult(tick);
            }
        }
    }

    public class CurrencyNameProvider : ICurrencyNameProvider
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
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CurrencyResultSummaries : ICurrencyResultSummaries
    {
        ICurrencyResultSummariesCallback callback;

        public CurrencyResultSummaries()
        {
            callback = OperationContext.Current.GetCallbackChannel<ICurrencyResultSummariesCallback>();
        }

        public void GetResultSummaries(int summariesAmount)
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

            Thread.Sleep(8000);
            callback.GetResultSummariesResult(summaries);
        }
    }
}
