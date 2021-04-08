using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Console;

namespace GreeterClient
{
    class Program
    {
        // Framework .NET 5.0

        const string address = "http://25.44.47.26:5001";

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
            List<double> coefficients = ReadCoefficients();

            var reply = await client.QuadraticFunctionAsync(new QuadraticFunctionRequest { 
                A = coefficients[0],
                B = coefficients[1],
                C = coefficients[2]
            });
        
            WriteLine("From server: ");
            PrintQuadraticFunctionReply(reply.ReplyCode, reply.X1, reply.X2, reply.Extr);
            WriteLine("Press any key to exit...");
            ReadKey();
        }

        private static List<double> ReadCoefficients()
        {
            WriteLine("Wprowadź współczynniki:");
            string[] coefficients = { "A", "B", "C" };
            List<double> numbers = new List<double>();
            foreach(var coefficient in coefficients)
            {
                bool success;
                double coef;
                do
                {
                    Write($"{coefficient} = ");
                    string numberStr = ReadLine();
                    success = double.TryParse(numberStr, out coef);
                } while (!success);
                numbers.Add(coef);
            }

            return numbers;
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
