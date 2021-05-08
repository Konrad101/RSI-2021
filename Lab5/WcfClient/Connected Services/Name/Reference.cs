﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WcfClient.Name {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Name.ICurrencyNameProvider")]
    public interface ICurrencyNameProvider {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyNameProvider/GetCurrencies", ReplyAction="http://tempuri.org/ICurrencyNameProvider/GetCurrenciesResponse")]
        string[] GetCurrencies(int currenciesAmount);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICurrencyNameProvider/GetCurrencies", ReplyAction="http://tempuri.org/ICurrencyNameProvider/GetCurrenciesResponse")]
        System.Threading.Tasks.Task<string[]> GetCurrenciesAsync(int currenciesAmount);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICurrencyNameProviderChannel : WcfClient.Name.ICurrencyNameProvider, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CurrencyNameProviderClient : System.ServiceModel.ClientBase<WcfClient.Name.ICurrencyNameProvider>, WcfClient.Name.ICurrencyNameProvider {
        
        public CurrencyNameProviderClient() {
        }
        
        public CurrencyNameProviderClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CurrencyNameProviderClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CurrencyNameProviderClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CurrencyNameProviderClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] GetCurrencies(int currenciesAmount) {
            return base.Channel.GetCurrencies(currenciesAmount);
        }
        
        public System.Threading.Tasks.Task<string[]> GetCurrenciesAsync(int currenciesAmount) {
            return base.Channel.GetCurrenciesAsync(currenciesAmount);
        }
    }
}
