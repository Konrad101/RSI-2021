﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfServiceClient.ServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference.ICryptoCurrency")]
    public interface ICryptoCurrency {
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ICryptoCurrency/GetCurrencies", ReplyAction="http://tempuri.org/ICryptoCurrency/GetCurrenciesResponse")]
        string[] GetCurrencies(int currenciesAmount);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ICryptoCurrency/GetCurrencies", ReplyAction="http://tempuri.org/ICryptoCurrency/GetCurrenciesResponse")]
        System.Threading.Tasks.Task<string[]> GetCurrenciesAsync(int currenciesAmount);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ICryptoCurrency/GetTick", ReplyAction="http://tempuri.org/ICryptoCurrency/GetTickResponse")]
        WcfServiceLibrary.APIConnection.CurrencyTick GetTick(string firstCurrency, string secondCurrency);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ICryptoCurrency/GetTick", ReplyAction="http://tempuri.org/ICryptoCurrency/GetTickResponse")]
        System.Threading.Tasks.Task<WcfServiceLibrary.APIConnection.CurrencyTick> GetTickAsync(string firstCurrency, string secondCurrency);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ICryptoCurrency/GetResultSummaries", ReplyAction="http://tempuri.org/ICryptoCurrency/GetResultSummariesResponse")]
        WcfServiceLibrary.APIConnection.ResultSummary[] GetResultSummaries(int summariesAmount);
        
        [System.ServiceModel.OperationContractAttribute(ProtectionLevel=System.Net.Security.ProtectionLevel.None, Action="http://tempuri.org/ICryptoCurrency/GetResultSummaries", ReplyAction="http://tempuri.org/ICryptoCurrency/GetResultSummariesResponse")]
        System.Threading.Tasks.Task<WcfServiceLibrary.APIConnection.ResultSummary[]> GetResultSummariesAsync(int summariesAmount);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICryptoCurrencyChannel : WcfServiceClient.ServiceReference.ICryptoCurrency, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CryptoCurrencyClient : System.ServiceModel.ClientBase<WcfServiceClient.ServiceReference.ICryptoCurrency>, WcfServiceClient.ServiceReference.ICryptoCurrency {
        
        public CryptoCurrencyClient() {
        }
        
        public CryptoCurrencyClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CryptoCurrencyClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CryptoCurrencyClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CryptoCurrencyClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] GetCurrencies(int currenciesAmount) {
            return base.Channel.GetCurrencies(currenciesAmount);
        }
        
        public System.Threading.Tasks.Task<string[]> GetCurrenciesAsync(int currenciesAmount) {
            return base.Channel.GetCurrenciesAsync(currenciesAmount);
        }
        
        public WcfServiceLibrary.APIConnection.CurrencyTick GetTick(string firstCurrency, string secondCurrency) {
            return base.Channel.GetTick(firstCurrency, secondCurrency);
        }
        
        public System.Threading.Tasks.Task<WcfServiceLibrary.APIConnection.CurrencyTick> GetTickAsync(string firstCurrency, string secondCurrency) {
            return base.Channel.GetTickAsync(firstCurrency, secondCurrency);
        }
        
        public WcfServiceLibrary.APIConnection.ResultSummary[] GetResultSummaries(int summariesAmount) {
            return base.Channel.GetResultSummaries(summariesAmount);
        }
        
        public System.Threading.Tasks.Task<WcfServiceLibrary.APIConnection.ResultSummary[]> GetResultSummariesAsync(int summariesAmount) {
            return base.Channel.GetResultSummariesAsync(summariesAmount);
        }
    }
}