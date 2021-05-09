using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfCallbackServiceContract;
using static System.Console;

namespace WcfHost
{
    class Program
    {
        private static readonly int PORT = 10001;

        private static void PrintInfo()
        {
            WriteLine("Konrad Hajduga 246995");
            WriteLine("Radosław Ścigała 246997");
            WriteLine(DateTime.Now);
            WriteLine(Environment.MachineName);
            WriteLine(Environment.UserName + "\n");
        }

        static void Main(string[] args)
        {
            PrintInfo();

            // currencyName
            Uri currencyNameAddress= new Uri($"http://25.43.220.53:{PORT}/name");
            ServiceHost currencyNameHost = new ServiceHost(typeof(CurrencyNameProvider), currencyNameAddress);

            BasicHttpBinding currencyNameBinding = new BasicHttpBinding();
            currencyNameHost.AddServiceEndpoint(typeof(ICurrencyNameProvider), currencyNameBinding, "currencyName");
            currencyNameBinding.Security.Mode = BasicHttpSecurityMode.None;


            ServiceMetadataBehavior currencyNameSMB = new ServiceMetadataBehavior();
            currencyNameSMB.HttpGetEnabled = true;
            currencyNameHost.Description.Behaviors.Add(currencyNameSMB);

            // tick
            Uri tickAddress = new Uri($"http://25.43.220.53:{PORT}/tick");
            ServiceHost tickHost = new ServiceHost(typeof(CurrencyTickProvider), tickAddress);

            WSDualHttpBinding tickBinding = new WSDualHttpBinding();
            tickHost.AddServiceEndpoint(typeof(ICurrencyTickProvider), tickBinding, "currencyTick");
            tickBinding.Security.Mode = WSDualHttpSecurityMode.None;


            ServiceMetadataBehavior tickSMB = new ServiceMetadataBehavior();
            tickSMB.HttpGetEnabled = true;
            tickHost.Description.Behaviors.Add(tickSMB);

            // summary
            Uri summaryAddress = new Uri($"http://25.43.220.53:{PORT}/summary");
            ServiceHost summaryHost = new ServiceHost(typeof(CurrencyResultSummaries), summaryAddress);

            WSDualHttpBinding summaryBinding = new WSDualHttpBinding();
            summaryHost.AddServiceEndpoint(typeof(ICurrencyResultSummaries), summaryBinding, "currencyResultSummary");
            summaryBinding.Security.Mode = WSDualHttpSecurityMode.None;

            ServiceMetadataBehavior summarySMB = new ServiceMetadataBehavior();
            summarySMB.HttpGetEnabled = true;
            summaryHost.Description.Behaviors.Add(tickSMB);

            try
            {
                currencyNameHost.Open();
                
                WriteLine("Service - Name - is running.");
                tickHost.Open();
                WriteLine("Service - Tick - is running.");
                summaryHost.Open();
                WriteLine("Service - Summary - is running.");

                WriteLine("Press <ENTER> to stop the host.");
                WriteLine();
                ReadLine();
                currencyNameHost.Close();
                tickHost.Close();
                summaryHost.Close();
            }
            catch (CommunicationException ce)
            {
                WriteLine("Exception occurred: {0}", ce.Message);
                currencyNameHost.Abort();
                tickHost.Abort();
                summaryHost.Abort();
            }
        }
    }
}
