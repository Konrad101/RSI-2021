using System;
using System.Collections.Generic;

namespace WcfServiceLibrary.APIConnection
{
    public class Summary
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<ResultSummary> result { get; set; }


        public override string ToString()
        {
            return $"succes: {success}\n" +
                $"message: {message}\n" +
                $"results: {result}";
        }
    }

    public class ResultSummary
    {
        public string MarketName { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Volume { get; set; }
        public double? Last { get; set; }
        public double? BaseVolume { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public double? Bid { get; set; }
        public double? Ask { get; set; }
        public int? OpenBuyOrders { get; set; }
        public int? OpenSellOrders { get; set; }
        public double? PrevDay { get; set; }
        public DateTimeOffset Created { get; set; }
        public string DisplayMarketName { get; set; }

        public override string ToString()
        {
            return "Result:\n" +
                $"\t MarketName: {MarketName}\n" +
                $"\t High: {High}\n" +
                $"\t Low: {Low}\n" +
                $"\t Volume: {Volume}\n" +
                $"\t Last: {Last}\n" +
                $"\t BaseVolume: {BaseVolume}\n" +
                $"\t TimeStamp: {TimeStamp}\n" +
                $"\t Bid: {Bid}\n" +
                $"\t Ask: {Ask}\n" +
                $"\t OpenBuyOrders: {OpenBuyOrders}\n" +
                $"\t OpenSellOrders: {OpenSellOrders}\n" +
                $"\t PrevDay: {PrevDay}\n" +
                $"\t Created: {Created}\n" +
                $"\t DisplayMarketName: {DisplayMarketName}\n";
        }
    }
}
