using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlphaStockAPI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Stock Data Variables
            double stock100DayTotal = 0;
            double stock50DayTotal = 0;
            double stock20DayTotal = 0;
            int dayCounter = 0;
            string printOutSpacer = "  ";

            // API Key
            string apiKey = "demo";

            // Stock Symbole
            string symbol = "IBM";

            // API URL that grabs last 100 days of a stocks data
            string queryURL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={symbol}&apikey={apiKey}&datatype=csv";

            // Create a new WEb Client
            WebClient webClient = new WebClient();
            
            // create date time string with todays date and format with the symbol
            string dateTime = symbol + "-" + DateTime.Now.ToString("MM-dd-yyyy");

            // Checkk if CSV file with todays date already exist
            // we are doing this to prevent multiple api calls in one day
            // incase we want to run different caluclations over and over on the same
            // set of data
            if(!(File.Exists(@"C:\Users\" + Environment.UserName + @"\Desktop\" + dateTime + ".csv")))
            {
                // Create a string called data that downloads API url to a web client
                string data = webClient.DownloadString(queryURL);
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\Desktop\" + dateTime + ".csv", data);
            }

            // Read in CSV File to StreamReader
            using (StreamReader reader = new StreamReader(@"C:\Users\" + Environment.UserName + @"\Desktop\" + dateTime + ".csv"))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // Create list of StockData
                List<StockData> stockDataList = new List<StockData>();

                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    // Get data off of CSV into stockDAta
                    StockData stockData = new StockData
                    {
                        AdjustedClose = csv.GetField<double>("adjusted_close"),
                        Date = DateTime.Parse(csv.GetField<string>("timestamp")).ToString("MM-dd-yyyy"),
                        Symbol = symbol,
                        Open = csv.GetField<double>("open"),
                        High = csv.GetField<double>("high"),
                        Low = csv.GetField<double>("low"),
                        Close = csv.GetField<double>("close"),
                        Volume = csv.GetField<int>("volume"),
                        DividendAmount = csv.GetField<double>("dividend_amount"),
                    };

                    // Increase counter and stockday ajudsted close totals
                    // for moving averages calculations
                    stock100DayTotal += stockData.AdjustedClose;
                    dayCounter++;

                    // Get 20 Day Moving Total
                    if (dayCounter == 20)
                    {
                        stock20DayTotal = stock100DayTotal;
                    }

                    // Get 50 Day moving total
                    if (dayCounter == 50)
                    {
                        stock50DayTotal = stock100DayTotal;
                    }

                    // Add the stockdata object to the list of stock data
                    stockDataList.Add(stockData);
                }

                // Print out all the closing price
                foreach (var item in stockDataList)
                {
                    Console.WriteLine($"{item.Symbol}{printOutSpacer}" +
                                        $"Open: ${item.Open.ToString("0.00")}{printOutSpacer}" +
                                        $"High: $ {item.High.ToString("0.00")}{printOutSpacer}" +
                                        $"Low: ${item.Low.ToString("0.00")}{printOutSpacer}" +
                                        $"Close: ${item.Close.ToString("0.00")}{printOutSpacer}" +
                                        $"Adjusted Close: ${item.AdjustedClose.ToString("0.00")}{printOutSpacer}" +
                                        $"Volume: {item.Volume}{printOutSpacer}" +
                                        $"DividendAmount: ${item.DividendAmount.ToString("0.00")}{printOutSpacer}" +
                                        $"{item.Date}");

                    Thread.Sleep(100);
                }

                // Print out moving averages
                Console.WriteLine($"{symbol} 100 Day Moving Average: ${(stock100DayTotal / 100).ToString("0.00")}");
                Console.WriteLine($"{symbol} 50 Day Moving Average: ${(stock50DayTotal / 50).ToString("0.00")}");
                Console.WriteLine($"{symbol} 20 Day Moving Average: ${(stock20DayTotal / 20).ToString("0.00")}");
            }

            // Keep Console from Closing automatically
            Console.Read();

        }
    }

    public class StockData
    {
        public double AdjustedClose { get; set; }
        public string Date { get; set; }
        public string Symbol { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; } 
        public double Close { get;set; }
        public double DividendAmount { get; set; }
        public int Volume { get; set; }
    }
}
