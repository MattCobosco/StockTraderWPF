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
                string uri = "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=" + GetUriSuffix(indexType);
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("X-API-KEY", _ApiKey);
                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                string regularMarketPrice = JObject.Parse(jsonResponse)["quoteResponse"]["result"][0]["regularMarketPrice"].ToString();
                string regularMarketChange = JObject.Parse(jsonResponse)["quoteResponse"]["result"][0]["regularMarketChange"].ToString();
                string shortName = JObject.Parse(jsonResponse)["quoteResponse"]["result"][0]["shortName"].ToString();
                
                MajorIndex index = new MajorIndex();
                
                index.Name = GetIndexName(indexType);
                index.Price = Convert.ToDouble(regularMarketPrice);
                index.Change = Convert.ToDouble(regularMarketChange);
                index.Type = indexType;
                
                return index;
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return "%5EDJI";
                case MajorIndexType.NASDAQ:
                    return "%5EIXIC";
                case MajorIndexType.SP500:
                    return "%5EGSPC";
                default:
                    throw new Exception("MajorIndexType does not have this suffix defined!");
            }
        }

        private string GetIndexName(MajorIndexType indexType)
        {
            switch (indexType)
            {
                case MajorIndexType.DowJones:
                    return "Dow Jones";
                case MajorIndexType.NASDAQ:
                    return "NASDAQ";
                case MajorIndexType.SP500:
                    return "S&P 500";
                default:
                    throw new Exception("MajorIndexType does not have this suffix defined!");
            }
        }
    }
}