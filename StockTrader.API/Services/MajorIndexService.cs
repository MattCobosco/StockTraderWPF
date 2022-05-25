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
                string fullExchangeName = JObject.Parse(jsonResponse)["quoteResponse"]["result"][0]["shortName"].ToString();
                
                MajorIndex index = new MajorIndex();
                index.Name = fullExchangeName;
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
                    break;
                case MajorIndexType.NASDAQ:
                    return "%5EIXIC";
                    break;
                case MajorIndexType.SP500:
                    return "%5EGSPC";
                    break;
                default:
                    return "%5EDJI";
                    break;
            }
        }
    }
}