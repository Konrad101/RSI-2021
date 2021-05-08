using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfCallbackServiceContract.APIConnection;
using static System.Console;

namespace WcfClient
{
    class SummaryCallback : Summary.ICurrencyResultSummariesCallback
    {
        public void GetResultSummariesResult(ResultSummary[] summaries)
        {
            foreach(var summary in summaries)
            {
                WriteLine(summary);
            }
        }
    }

    class TickCallback : Tick.ICurrencyTickProviderCallback
    {
        public void GetTickResult(CurrencyTick tick)
        {
            if (tick != null)
            {
                WriteLine(tick);
            }
            else
            {
                WriteLine("Podana para kryptowalut nie jest dostępna w API.");
            }
        }
    }
}
