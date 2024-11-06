using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BankSystem.App.Dto;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BankSystem.App.Services
{
    public class CurrencyService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public CurrencyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["ApiSettings:ApiKey"];
        }

        public async Task<decimal> ConvertCurrencyAsync(decimal amount, string fromCurrency, string toCurrency,
            CancellationToken cancellationToken)
        {
            var requestUriBuilder = new StringBuilder("https://www.amdoren.com/api/currency.php?");
            requestUriBuilder.Append("api_key=").Append(_apiKey)
                .Append("&from=").Append(fromCurrency)
                .Append("&to=").Append(toCurrency)
                .Append("&amount=").Append(amount);

            var requestUri = requestUriBuilder.ToString();

            var response = await _httpClient.GetAsync(requestUri, cancellationToken);

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);

            var currencyResponse = JsonConvert.DeserializeObject<CurrencyApiResponse>(responseBody);

            if (currencyResponse.Error != 0) throw new Exception($"Ошибка API: {currencyResponse.Error}");

            return currencyResponse.Amount;
        }
    }
}