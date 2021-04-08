using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using static System.Console;

namespace Greeter
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Witamy,\nKonrad Hajduga 246995 i Rados³aw Œciga³a 246997\n" +
                "Serwer: " + Environment.MachineName + "\n" +
                request.Name
            });
        }

        public override Task<QuadraticFunctionReply> QuadraticFunction(QuadraticFunctionRequest request, ServerCallContext context)
        {
            WriteLine($"New request: a = {request.A}, b = {request.B}, c = {request.C}");

            int replyCode;
            double x1, x2;
            double extr;
            if (request.A == 0)
            {
                replyCode = -1;
                x1 = x2 = extr = -1;
            } else {
                double delta = request.B * request.B - 4 * request.A * request.C;
                x1 = (-request.B - Math.Sqrt(delta)) / (2 * request.A);
                if (delta == 0)
                {
                    replyCode = 1;
                    x2 = x1;
                    extr = x1;
                } else if (delta > 0)
                {
                    replyCode = 2;
                    x2 = (-request.B + Math.Sqrt(delta)) / (2 * request.A);
                    extr = -request.B / (2 * request.A);
                } else
                {
                    replyCode = 0;
                    x1 = x2 = 0;
                    extr = -request.B / (2 * request.A);
                }
            }


            return Task.FromResult(new QuadraticFunctionReply
            {
                ReplyCode = replyCode,
                Extr = extr,
                X1 = x1,
                X2 = x2
            });
        }
    }
}
