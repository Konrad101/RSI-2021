using System.Collections.Generic;
using System.ServiceModel;
using WcfServiceLibrary.APIConnection;

namespace WcfServiceLibrary
{
    [ServiceContract(ProtectionLevel = System.Net.Security.ProtectionLevel.None)]
    public interface ICryptoCurrency
    {
        [OperationContract]
        List<string> GetCurrencies(int currenciesAmount);
        [OperationContract]
        CurrencyTick GetTick(string firstCurrency, string secondCurrency);
        [OperationContract]
        List<ResultSummary> GetResultSummaries(int summariesAmount);
    }
}
