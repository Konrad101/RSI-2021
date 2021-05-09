namespace WcfServiceLibrary.APIConnection
{
    public class CurrencyTick
    {
        public bool success { get; set; }
        public string message { get; set; }
        public Result result { get; set; }

        public override string ToString()
        {
            return $"bid: {result.Bid}\n" +
                $"ask: {result.Ask}\n" +
                $"last: {result.Last}";
        }
    }

    public class Result
    {
        public float Bid { get; set; }
        public float Ask { get; set; }
        public float Last { get; set; }
    }
}
