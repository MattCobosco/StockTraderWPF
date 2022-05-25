using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockTrader.Domain.Models;
using StockTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrader.API.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        private string _ApiKey = "TWUN8RpX1i8iki66peGve1IZbjFI2kcSaAyJ3r6i";
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using(HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("X-API-KEY", _ApiKey);
                HttpResponseMessage response = await client.GetAsync("https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=%5EDJI");
                string jsonResponse = await response.Content.ReadAsStringAsync();

                string jsonRaw = "{\n  \"quoteResponse\": {\n    \"result\": [\n      {\n        \"language\": \"en-US\",\n        \"region\": \"US\",\n        \"quoteType\": \"INDEX\",\n        \"typeDisp\": \"Index\",\n        \"quoteSourceName\": \"Delayed Quote\",\n        \"triggerable\": true,\n        \"customPriceAlertConfidence\": \"HIGH\",\n        \"currency\": \"USD\",\n        \"marketState\": \"POST\",\n        \"regularMarketTime\": 1653510000,\n        \"regularMarketPrice\": 32120.28,\n        \"regularMarketDayHigh\": 32254.44,\n        \"regularMarketDayRange\": \"31754.33 - 32254.44\",\n        \"regularMarketDayLow\": 31754.33,\n        \"regularMarketVolume\": 343464318,\n        \"regularMarketPreviousClose\": 31928.62,\n        \"bid\": 31981.21,\n        \"ask\": 32194.22,\n        \"bidSize\": 0,\n        \"askSize\": 0,\n        \"fullExchangeName\": \"DJI\",\n        \"regularMarketOpen\": 31816.31,\n        \"averageDailyVolume3Month\": 395439344,\n        \"averageDailyVolume10Day\": 437328000,\n        \"fiftyTwoWeekLowChange\": 1484.5195,\n        \"fiftyTwoWeekLowChangePercent\": 0.048457082,\n        \"fiftyTwoWeekRange\": \"30635.76 - 36952.65\",\n        \"fiftyTwoWeekHighChange\": -4832.369,\n        \"fiftyTwoWeekHighChangePercent\": -0.13077193,\n        \"fiftyTwoWeekLow\": 30635.76,\n        \"fiftyTwoWeekHigh\": 36952.65,\n        \"fiftyDayAverage\": 33711.93,\n        \"fiftyDayAverageChange\": -1591.6504,\n        \"fiftyDayAverageChangePercent\": -0.047213268,\n        \"twoHundredDayAverage\": 34761.168,\n        \"twoHundredDayAverageChange\": -2640.8887,\n        \"exchange\": \"DJI\",\n        \"shortName\": \"Dow Jones Industrial Average\",\n        \"messageBoardId\": \"finmb_INDEXDJI\",\n        \"exchangeTimezoneName\": \"America/New_York\",\n        \"exchangeTimezoneShortName\": \"EDT\",\n        \"gmtOffSetMilliseconds\": -14400000,\n        \"market\": \"us_market\",\n        \"esgPopulated\": false,\n        \"twoHundredDayAverageChangePercent\": -0.07597238,\n        \"sourceInterval\": 120,\n        \"exchangeDataDelayedBy\": 0,\n        \"tradeable\": false,\n        \"firstTradeDateMilliseconds\": 694362600000,\n        \"priceHint\": 2,\n        \"regularMarketChange\": 191.66016,\n        \"regularMarketChangePercent\": 0.600277,\n        \"symbol\": \"^DJI\"\n      }\n    ],\n    \"error\": null\n  }\n}";
                
                string regularMarketPrice = JObject.Parse(jsonRaw)["quoteResponse"]["result"][0]["regularMarketPrice"].ToString();
                string regularMarketChange = JObject.Parse(jsonRaw)["quoteResponse"]["result"][0]["regularMarketChange"].ToString();
                string fullExchangeName = JObject.Parse(jsonRaw)["quoteResponse"]["result"][0]["fullExchangeName"].ToString();
                
                MajorIndex index = new MajorIndex();
                index.Name = fullExchangeName;
                index.Price = Convert.ToDouble(regularMarketPrice);
                index.Change = Convert.ToDouble(regularMarketChange);
                index.Type = indexType;
                
                return index;
            }
        }
    }
}
