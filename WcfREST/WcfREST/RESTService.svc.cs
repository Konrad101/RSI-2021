using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using static System.Console;

namespace WcfREST
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę klasy „Service1” w kodzie, usłudze i pliku konfiguracji.
    // UWAGA: aby uruchomić klienta testowego WCF w celu przetestowania tej usługi, wybierz plik Service1.svc lub Service1.svc.cs w eksploratorze rozwiązań i rozpocznij debugowanie.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class RESTService : IRESTService
    {
        private static List<CurrencyPair> currencyPairs = new List<CurrencyPair>();

        static RESTService()
        {
            string firstCurrency = "BTC";
            currencyPairs.Add(GetCurrencyPair(firstCurrency, "ETH"));
            currencyPairs.Add(GetCurrencyPair(firstCurrency, "DOGE"));
            currencyPairs.Add(GetCurrencyPair(firstCurrency, "LTC"));
        }

        private static CurrencyPair GetCurrencyPair(string firstCurrency, string secondCurrency)
        {
            WriteLine($"Get currency pair: {firstCurrency}, {secondCurrency}");
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

            int maxId;
            if (currencyPairs.Count == 0 || currencyPairs == null)
                maxId = 0;
            else
                maxId = currencyPairs.OrderByDescending(c => c.Id).FirstOrDefault().Id;

            return new CurrencyPair { Id=maxId+1, FirstCurrency=firstCurrency, SecondCurrency=secondCurrency, Value=tick.result.Last};
        }

        public string addJson(CurrencyPair pair)
        {
            return addXml(pair);
        }

        public string addXml(CurrencyPair pair)
        {
            if (pair == null)
            {
                throw new WebFaultException<string>("400: BadRequest", HttpStatusCode.BadRequest);
            }
            int idx = currencyPairs.FindIndex(b => b.Id == pair.Id);
            if (idx == -1)
            {
                currencyPairs.Add(pair);
                return "Added item with ID = " + pair.Id;
            }
            else
                throw new WebFaultException<string>("409: Conflict", HttpStatusCode.Conflict);
        }

        public string deleteJson(string Id)
        {
            return deleteXml(Id);
        }

        public string deleteXml(string Id)
        {
            int intId = int.Parse(Id);
            int idx = currencyPairs.FindIndex(b => b.Id == intId);
            if (idx == -1)
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            currencyPairs.RemoveAt(idx);
            return "Deleted item with ID= " + Id;
        }

        public List<CurrencyPair> getAllJson()
        {
            return currencyPairs;
        }

        public List<CurrencyPair> getAllXml()
        {
            return currencyPairs;
        }

        public CurrencyPair getByIdJson(string Id)
        {
            return getByIdXml(Id);
        }

        public CurrencyPair getByIdXml(string Id)
        {
            int intId = int.Parse(Id);
            CurrencyPair currencyPair = currencyPairs.Find(pair => pair.Id == intId);
            if (currencyPair == null)
                throw new WebFaultException<string>("404: Not Found", HttpStatusCode.NotFound);
            return currencyPair;
        }
    }
}
