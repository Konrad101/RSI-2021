using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfCallbackServiceContract.APIConnection;

namespace WcfCallbackServiceContract
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract =typeof(ICurrencyTickProviderCallback))]
    public interface ICurrencyTickProvider
    {
        [OperationContract(IsOneWay = true)]
        void GetTick(string firstCurrency, string secondCurrency);
    }

    public interface ICurrencyTickProviderCallback
    {

        [OperationContract(IsOneWay = true)]
        void GetTickResult(CurrencyTick tick);
    }

    [ServiceContract]
    public interface ICurrencyNameProvider
    {
        [OperationContract]
        List<string> GetCurrencies(int currenciesAmount);
    }

    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract =typeof(ICurrencyResultSummariesCallback))]
    public interface ICurrencyResultSummaries 
    {
        [OperationContract(IsOneWay = true)]
        void GetResultSummaries(int summariesAmount);
    }

    public interface ICurrencyResultSummariesCallback
    {
        [OperationContract(IsOneWay = true)]
        void GetResultSummariesResult(List<ResultSummary> summaries);
    }
}
