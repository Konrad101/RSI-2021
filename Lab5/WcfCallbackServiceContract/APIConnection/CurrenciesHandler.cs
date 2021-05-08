using System.Collections.Generic;

namespace WcfCallbackServiceContract.APIConnection
{
    public class CurrenciesHandler
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<CurrenciesResult> result { get; set; }

    }

    public class CurrenciesResult
    {
        public string Currency { get; set; }
        public string CurrencyLong { get; set; }
        public string CoinType { get; set; }
        public int MinConfirmation { get; set; }
        public double TxFee { get; set; }
        public bool IsActive { get; set; }
        public bool IsRestricted { get; set; }
        public string BaseAddress { get; set; }
    }
}
