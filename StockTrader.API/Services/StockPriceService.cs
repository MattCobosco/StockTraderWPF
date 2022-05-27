
using Newtonsoft.Json.Linq;
using StockTrader.Domain.Exceptions;
using StockTrader.Domain.Services;

namespace StockTrader.YahooFinanceAPI.Services
{
    public class StockPriceService : IStockPriceService
    {
        private string _ApiKey = "TWUN8RpX1i8iki66peGve1IZbjFI2kcSaAyJ3r6i";

        public async Task<double> GetStockPrice(string symbol)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=" + symbol;
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("X-API-KEY", _ApiKey);
                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                string regularMarketPrice = JObject.Parse(jsonResponse)["quoteResponse"]["result"][0]["regularMarketPrice"].ToString();
                double price = Convert.ToDouble(regularMarketPrice);

                if (price == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }
                return price;
            }
        }
    }
}
