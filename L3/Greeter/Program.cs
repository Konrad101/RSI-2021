using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using static System.Console;

namespace Greeter
{
    public class Program
    {
        // Framework .NET 5.0
        
        static void PrintInfo()
        {
            WriteLine("Konrad Hajduga, 246995");
            WriteLine("Rados³aw Œciga³a, 246997");
            WriteLine(Environment.MachineName);
            WriteLine(DateTime.Now);
            WriteLine(Environment.UserName + "\n");
        }

        public static void Main(string[] args)
        {
            PrintInfo();
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
