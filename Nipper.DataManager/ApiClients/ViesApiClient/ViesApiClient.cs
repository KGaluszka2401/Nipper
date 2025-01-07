using System.Text.Json;
using Nipper.DataManager.ApiClients.ViesApiClient.Models;

namespace Nipper.DataManager.ApClients.ViesApiClient;

internal class ViesApiClient
{
    private static readonly HttpClient client = new();
    private const string baseUrl = "https://ec.europa.eu/taxation_customs/vies/rest-api/ms/";

    /// <summary>
    /// Send a GET request to EU VIES service to validate a vat eu number.
    /// </summary>
    /// <param name="countryCode"></param>
    /// <param name="nip"></param>
    /// <returns>
    /// <strong>ViesEntityResponse</strong> object when vies service returns information about nip number or a 
    /// <strong>ViesExceptionResponse</strong> on a failed request.
    /// </returns>
    public async Task<IViesResponse> CheckNipAsync(string countryCode, string nip)
    {
        var uri = new Uri(baseUrl + countryCode + $"/vat/" + nip);
        var response = await client.GetAsync(uri);

        if (!response.IsSuccessStatusCode)
        {
            return new ViesExceptionResponse()
            {
                ErrorMesssage = $"Zapytanie nie powiodło się, serwer zwrócił kod {response.StatusCode}"
            };
        }

        string responseBody = await response.Content.ReadAsStringAsync();
        var viesResponse = JsonSerializer.Deserialize<ViesEntityResponse>(responseBody);

        if (viesResponse == null)
        {
            return new ViesExceptionResponse()
            {
                ErrorMesssage = $"Nie powiodła się deserializacja odpowiedzi serwera"
            };
        }

        return viesResponse;
    }
}
