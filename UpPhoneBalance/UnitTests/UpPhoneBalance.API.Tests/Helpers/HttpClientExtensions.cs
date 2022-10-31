using System.Text;
using Newtonsoft.Json;
using UpPhoneBalance.Infrastructure.Enums;
using UpPhoneBalance.Infrastructure.Models;
using UpPhoneBalance.Infrastructure.Models.Responses;

namespace UpPhoneBalance.API.Tests.Helpers;

public static class HttpClientExtensions
{
    public async static Task<DefaultResponseObject<TResponse>> PostAndReturnResponseAsync<TRequest, TResponse>(this HttpClient client, TRequest request, string uri)
    {
        var message = new HttpRequestMessage()
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(client.BaseAddress!, uri),
            Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
        };

        return await client.ExchangeAsync<TResponse>(message);
    }
    
    private async static Task<DefaultResponseObject<TResponse>> ExchangeAsync<TResponse>(this HttpClient client, HttpRequestMessage message)
    {
        try
        {
            var response = await client.SendAsync(message);
            response.EnsureSuccessStatusCode();
            string dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = JsonConvert.DeserializeObject<DefaultResponseObject<TResponse>>(dataAsString);
            if (result == null) throw new InvalidCastException($"Cast to {typeof(DefaultResponseObject<TResponse>)} is dropped");
            return result;
        }
        catch (Exception ex)
        {
            return new DefaultResponseObject<TResponse>() { ErrorModel =  new ErrorModel{StatusCode  = StatusCodes.ServiceIsUnavailable, Message = $"{ex.Message}"}};
        }
    }
}