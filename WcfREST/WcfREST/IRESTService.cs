using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfREST
{
    // UWAGA: możesz użyć polecenia „Zmień nazwę” w menu „Refaktoryzuj”, aby zmienić nazwę interfejsu „IService1” w kodzie i pliku konfiguracji.
    [ServiceContract]
    public interface IRESTService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/currencyPair")]
        List<CurrencyPair> getAllXml();

        [OperationContract]
        [WebGet(UriTemplate = "/currencyPair/{id}",
            ResponseFormat = WebMessageFormat.Xml)]
        CurrencyPair getByIdXml(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/currencyPair",
            Method = "POST",
            RequestFormat = WebMessageFormat.Xml)]
        string addXml(CurrencyPair pair);

        [OperationContract]
        [WebInvoke(UriTemplate = "/currencyPair/{id}/delete",
            Method = "DELETE")]
        string deleteXml(string Id);


        [OperationContract]
        [WebGet(UriTemplate = "/json/currencyPair",
            ResponseFormat = WebMessageFormat.Json)]
        List<CurrencyPair> getAllJson();

        [OperationContract]
        [WebGet(UriTemplate = "/json/currencyPair/{id}",
            ResponseFormat = WebMessageFormat.Json)]
        CurrencyPair getByIdJson(string Id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/currencyPair",
            Method = "POST",
            RequestFormat = WebMessageFormat.Json)]
        string addJson(CurrencyPair pair);

        [OperationContract]
        [WebInvoke(UriTemplate = "/json/currencyPair/{id}/delete",
            ResponseFormat = WebMessageFormat.Json,
            Method = "DELETE")]
        string deleteJson(string Id);

    }


    // Użyj kontraktu danych, jak pokazano w poniższym przykładzie, aby dodać typy złożone do operacji usługi.
    [DataContract]
    public class CurrencyPair
    {
        [DataMember(Order = 0)]
        public int Id { get; set; }

        [DataMember(Order = 1)]
        public string FirstCurrency { get; set; }

        [DataMember(Order = 2)]
        public string SecondCurrency { get; set; }

        [DataMember(Order = 3)]
        public double Value { get; set; }
    }
}
