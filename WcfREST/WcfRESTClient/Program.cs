using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace WcfRESTClient
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                try
                {
                    WriteLine("Podaj format (xml lub json):");
                    string format = ReadLine();
                    WriteLine("Podaj metode (GET lub POST lub ...):");
                    string method = ReadLine();
                    WriteLine("Podaj URI:");
                    string uri = ReadLine();
                    HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;

                    req.KeepAlive = false;
                    req.Method = method.ToUpper();
                    if (format == "xml")
                    {
                        req.ContentType = "text/xml";
                    }
                    else if (format == "json")
                    {
                        req.ContentType = "application/json";
                    }
                    else
                    {
                        WriteLine("Podales zle dane!");
                        return;
                    }
                    switch (method.ToUpper())
                    {
                        case "GET":
                            break;
                        case "POST":
                            WriteLine("Wklej zawartość XML-a lub JSON-a (w jednej linii!)");
                            string dane = ReadLine();
                            byte[] bufor = Encoding.UTF8.GetBytes(dane);
                            req.ContentLength = bufor.Length;
                            Stream postData = req.GetRequestStream();
                            postData.Write(bufor, 0, bufor.Length);
                            postData.Close();
                            break;
                        //tu ewentualnie kolejne opcje
                        default:
                            break;
                    }
                    HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                    //przekodowanie tekstu odpowiedzi:
                    Encoding enc = System.Text.Encoding.GetEncoding(1252);
                    StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
                    string responseString = responseStream.ReadToEnd();
                    responseStream.Close();
                    resp.Close();
                    WriteLine(responseString);
                }
                catch (Exception ex)
                {
                    WriteLine(ex.Message);
                }
                WriteLine("\nDoyou want to continue?");
            } while (ReadLine().ToUpper() == "Y");
        }
    }
}
