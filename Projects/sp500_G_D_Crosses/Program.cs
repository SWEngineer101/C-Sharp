using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;

namespace sp500_G_D_Crosses
{
    internal class Program
    {
        static List<StockData> stockDataList = new List<StockData>();
        static int stockCounter = 0;

        static void Main(string[] args)
        {
            // Import sp500 stock tickers into a ticker list
            GetStockTickers();

            // Set up Alpha Vantage API Connections
            foreach (StockData stock in stockDataList)
            {
                GetStockAlphaVantageAPIData(stock.Ticker);
                stockCounter++;
                Console.WriteLine($"Stocks Data imported by API: {stock.Ticker} Counter: {stockCounter}");
            }

            stockCounter = 0;

            // Get SMA CSV Data from CSV Collections
            foreach (StockData stock in stockDataList)
            {
                GetSMAData(stock.Ticker);
                stockCounter++;
                Console.WriteLine($"Stocks SMA Calculated: {stock.Ticker} Counter: {stockCounter}");
            }

            // Print Golden/Death Cross Stocks
            PrintCrossStocks();

            // Keep Console from closing after program execution
            Console.WriteLine("Program finished, press any key to continue..");
            Console.Read();
        }

        public static void PrintCrossStocks()
        {
            Console.WriteLine("\n==============================\nPossible Golden Cross Stocks\n==============================");            
            foreach(StockData stock in stockDataList)
            {
                if ((stock.GoldenCross != 0))
                {                    
                    // Print Golden Crosses
                    Console.WriteLine($"{stock.Ticker}  sma50: {stock.sma50}  sma200: {stock.sma200}  Golden Cross Ratio: {stock.GoldenCross}");
                }
            }

            Console.WriteLine("\n==============================\nPossible Death Cross Stocks\n==============================");            
            foreach (StockData stock in stockDataList)
            {
                if ((stock.DeathCross != 0))
                {                    
                    // Print Death Crosses
                    Console.WriteLine($"{stock.Ticker}  sma50: {stock.sma50}  sma200: {stock.sma200}  Death Cross Ratio: {stock.DeathCross}");
                }
            }
        }

        public static void GetSMAData(string ticker)
        {
            // create date time string with todays date and format with the symbol
            string csvFileName = ticker + "-" + DateTime.Now.ToString("MM-dd-yyyy");
            bool isStockUpdated = false;
            float runningSum = 0;
            int sumCount = 0;
            float sma45 = 0;

            while (!isStockUpdated)
            {
                foreach (StockData item in stockDataList)
                {
                    if (isStockUpdated)
                        break;

                    if ((item.Ticker).Equals(ticker))
                    {
                        try
                        {
                            // Read in CSV File to StreamReader
                            using (StreamReader reader = new StreamReader("..\\..\\stock_csv_data\\" + csvFileName + ".csv"))
                            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                                csv.Read();
                                csv.ReadHeader();
                                while (csv.Read())
                                {
                                    runningSum += csv.GetField<float>("adjusted_close");
                                    sumCount++;
                                    if (sumCount == 45)
                                        sma45 = (runningSum / 45);
                                    if (sumCount == 50)
                                        item.sma50 = (runningSum / 50);
                                    if (sumCount == 100)
                                        item.sma100 = (runningSum / 100);
                                    if (sumCount == 200)
                                    {
                                        item.sma200 = (runningSum / 200);

                                        // Calculate Possible Golden Cross
                                        if (((item.sma50 >= item.sma200) && sma45 < item.sma200) 
                                            || (((item.sma50/item.sma200) >= 0.98) && (item.sma50 / item.sma200) <= 1.02 ) 
                                            && sma45 < item.sma200)
                                        {
                                            item.GoldenCross = (item.sma50 / item.sma200);
                                        }
                                        // Calculate Possible Death Cross
                                        else if (((item.sma50 <= item.sma200) && sma45 > item.sma200) 
                                            || (((item.sma50 / item.sma200) >= 0.98) && ((item.sma50 / item.sma200) <= 1.02) 
                                            && sma45 > item.sma200))
                                        {
                                            item.DeathCross = (item.sma50 / item.sma200);
                                        }
                                        else
                                        {
                                            item.GoldenCross = 0;
                                            item.DeathCross = 0;
                                        }

                                        isStockUpdated = true;
                                        break;
                                    }
                                }
                            }
                        }
                        catch 
                        {
                            isStockUpdated = true;
                            break;
                        }
                    }
                }
            }
        }

        public static void GetStockAlphaVantageAPIData(string ticker)
        {
            string apiKey = "";
            string apiURL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY_ADJUSTED&symbol={ticker}&outputsize=full&apikey={apiKey}&datatype=csv";

            // Create a new WEb Client
            WebClient webClient = new WebClient();

            // create date time string with todays date and format with the symbol
            string fileName = ticker + "-" + DateTime.Now.ToString("MM-dd-yyyy");

            // Checkk if CSV file with todays date already exist
            // we are doing this to prevent multiple api calls in one day
            // incase we want to run different caluclations over and over on the same
            // set of data
            if (!(File.Exists("..\\..\\stock_csv_data\\" + fileName + ".csv")))
            {
                // Create a string called data that downloads API url to a web client
                string data = webClient.DownloadString(apiURL);
                File.WriteAllText("..\\..\\stock_csv_data\\" + fileName + ".csv", data);

                // Sleep 15 Seconds
                Thread.Sleep(15000);
            }           
        }

        public static void GetStockTickers()
        {
            // Read in CSV File to StreamReader
            using (StreamReader reader = new StreamReader("..\\..\\sp500.csv"))
            using (CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    // Get data off of CSV into stockDAta
                    StockData stock = new StockData
                    {
                        Ticker = csv.GetField<string>("Symbol"),
                        Name = csv.GetField<string>("Name"),
                        Sector = csv.GetField<string>("Sector")
                    };
                    stockDataList.Add(stock);
                }
            }

            Console.WriteLine("sp500 Stock Tickers read into memory.");
        }
    }

    public class StockData
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
        public float sma50 { get; set; }
        public float sma100 { get; set; }
        public float sma200 { get; set; }
        public float GoldenCross { get; set; }
        public float DeathCross { get; set; }
        public string Sector { get; set; }
        public DateTime DateExamined { get; set; }
    }
}
