using Grpc.Net.Client;
using System;
using System.Threading.Tasks;
using static System.Console;

namespace GreeterClient
{
    class Program
    {
        const string address = "http://25.43.220.53:5001";

        static void PrintInfo()
        {
            WriteLine("Konrad Hajduga, 246995");
            WriteLine("Radosław Ścigała, 246997");
            WriteLine(Environment.MachineName);
            WriteLine(DateTime.Now);
            WriteLine(Environment.UserName + "\n");
        }

        static async Task Main(string[] args)
        {
            PrintInfo();
            WriteLine("Starting gRPC Hello Client");
            using var channel = GrpcChannel.ForAddress(address);
            var client = new Greeter.GreeterClient(channel);

            // 2, 1, 4      Brak rozwiazan rzeczywistych
            // 1, 4, 4      1 rozwiazanie
            // 1, 1, -6     2 rozwiazania
            double a = 2;
            double b = 1;
            double c = 4;

            var reply = await client.QuadraticFunctionAsync(new QuadraticFunctionRequest { 
                A = a,
                B = b,
                C = c
            });
        
            WriteLine("From server: ");
            PrintQuadraticFunctionReply(reply.ReplyCode, reply.X1, reply.X2, reply.Extr);
            WriteLine("Press any key to exit...");
            ReadKey();
        }

        private static void PrintQuadraticFunctionReply(int replyCode, double x1, double x2, double extr)
        {
            if(replyCode == -1)
            {
                WriteLine("Funkcja nie jest kwadratowa");
            } else if (replyCode == 0)
            {
                WriteLine("Brak rozwiązań rzeczywistych");
                WriteLine($"Extremum = {extr}");
            }
            else if(replyCode == 1)
            {
                WriteLine("Znaleziono jedno rozwiązanie");
                WriteLine($"x = {x1}");
                WriteLine($"Extremum = {extr}");
            }
            else
            {
                WriteLine("Znaleziono dwa rozwiązania");
                WriteLine($"x1 = {x1}");
                WriteLine($"x2 = {x2}");
                WriteLine($"Extremum = {extr}");
            }
        }
    }
}
