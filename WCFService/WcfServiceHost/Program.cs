using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfServiceLibrary;
using static System.Console;

namespace WcfServiceHost
{
    class Program
    {
        private static readonly int PORT = 5001;
        private static readonly string IP_ADDRESS = "25.43.220.53";

        static void PrintInfo()
        {
            WriteLine("Konrad Hajduga, 246995");
            WriteLine("Radosław Ścigała, 246997");
            WriteLine(Environment.MachineName);
            WriteLine(DateTime.Now);
            WriteLine(Environment.UserName);
            var hostName = Dns.GetHostName();
            var hostEntry = Dns.GetHostEntry(hostName);
            var ipAdresses = hostEntry.AddressList;
            WriteLine($"IP address: http://{ ipAdresses[ipAdresses.Length - 1]}\n");
        }

        static void Main(string[] args)
        {
            PrintInfo();
            // Krok 1 Utworz URI dla bazowego adresu serwisu.
            Uri baseAddress = new Uri($"http://{IP_ADDRESS}:{PORT}/mojkalkulator");
            // Krok 2 Utworz instancje serwisu.
            ServiceHost mojHost = new ServiceHost(typeof(CryptoCurrencyProvider), baseAddress);
            // Krok 3 Dodaj endpoint.
            WSHttpBinding mojBanding = new WSHttpBinding();
            mojBanding.Security.Mode = SecurityMode.None;
            mojHost.AddServiceEndpoint(typeof(ICryptoCurrency),
            mojBanding, "endpoint1");
            // Krok 4 Ustaw właczenie metadanych.
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            mojHost.Description.Behaviors.Add(smb);
            try
            {
                BasicHttpBinding binding2 = new BasicHttpBinding();
                ServiceEndpoint endpoint2 = mojHost.AddServiceEndpoint(
                 typeof(ICryptoCurrency), binding2, "endpoint2");
                ServiceEndpoint endpoint3 = mojHost.Description.Endpoints.Find(
                    new Uri($"http://{IP_ADDRESS}:{PORT}/mojkalkulator/endpoint3")
                    );

                WriteLine("\n---> Endpointy:");
                WriteLine("\nService endpoint {0}:", endpoint2.Name);
                WriteLine("Binding: {0}", endpoint2.Binding.ToString());
                WriteLine("ListenUri: {0}", endpoint2.ListenUri.ToString());

                WriteLine("\nService endpoint {0}:", endpoint3.Name);
                WriteLine("Binding: {0}", endpoint3.Binding.ToString());
                WriteLine("ListenUri: {0}", endpoint3.ListenUri.ToString());

                // Krok 5 Uruchom serwis.
                mojHost.Open();
                WriteLine("Serwis jest uruchomiony.");
                WriteLine("Nacisnij <ENTER> aby zakonczyc.");
                WriteLine();
                ReadLine(); // aby nie kończyć natychmiast:
                mojHost.Close();
            }
            catch (CommunicationException ce)
            {
                WriteLine("Wystapil wyjatek: {0}", ce.Message);
                mojHost.Abort();
            }
        }
    }
}
