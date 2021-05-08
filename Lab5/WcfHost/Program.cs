using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfCallbackServiceContract;

namespace WcfHost
{
    class Program
    {
        private static readonly int PORT = 10001;

        private static void PrintInfo()
        {
            Console.WriteLine("Konrad Hajduga 246995");
            Console.WriteLine("Radosław Ścigała 246997");
            Console.WriteLine(DateTime.Now);
            Console.WriteLine(Environment.MachineName + "\n");
        }

        static void Main(string[] args)
        {
            PrintInfo();

            // currencyName
            Uri currencyNameAddress= new Uri($"http://25.43.220.53:{PORT}/name");
            ServiceHost currencyNameHost = new ServiceHost(typeof(CurrencyNameProvider), currencyNameAddress);

            BasicHttpBinding currencyNameBinding = new BasicHttpBinding();
            currencyNameHost.AddServiceEndpoint(typeof(ICurrencyNameProvider), currencyNameBinding, "currencyName");

            ServiceMetadataBehavior currencyNameSMB = new ServiceMetadataBehavior();
            currencyNameSMB.HttpGetEnabled = true;
            currencyNameHost.Description.Behaviors.Add(currencyNameSMB);

            // tick
            Uri tickAddress = new Uri($"http://25.43.220.53:{PORT}/tick");
            ServiceHost tickHost = new ServiceHost(typeof(CurrencyTickProvider), tickAddress);

            WSDualHttpBinding tickBinding = new WSDualHttpBinding();
            tickHost.AddServiceEndpoint(typeof(ICurrencyTickProvider), tickBinding, "currencyTick");

            ServiceMetadataBehavior tickSMB = new ServiceMetadataBehavior();
            tickSMB.HttpGetEnabled = true;
            tickHost.Description.Behaviors.Add(tickSMB);

            // summary
            Uri summaryAddress = new Uri($"http://25.43.220.53:{PORT}/summary");
            ServiceHost summaryHost = new ServiceHost(typeof(CurrencyResultSummaries), summaryAddress);

            WSDualHttpBinding summaryBinding = new WSDualHttpBinding();
            summaryHost.AddServiceEndpoint(typeof(ICurrencyResultSummaries), summaryBinding, "currencyResultSummary");

            ServiceMetadataBehavior summarySMB = new ServiceMetadataBehavior();
            summarySMB.HttpGetEnabled = true;
            summaryHost.Description.Behaviors.Add(tickSMB);

            try
            {
                currencyNameHost.Open();
                
                Console.WriteLine("Service - Name - is running.");
                tickHost.Open();
                Console.WriteLine("Service - Tick - is running.");
                summaryHost.Open();
                Console.WriteLine("Service - Summary - is running.");

                Console.WriteLine("Press <ENTER> to stop the host.");
                Console.WriteLine();
                Console.ReadLine();
                currencyNameHost.Close();
                tickHost.Close();
                summaryHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Exception occurred: {0}", ce.Message);
                currencyNameHost.Abort();
                tickHost.Abort();
                summaryHost.Abort();
            }
        }
    }
}
