using System.Web;
using System.Text.Json;
using Nipper.DataManager.ApiClients.WlApiClient.Models;

namespace Nipper.DataManager.ApiClients.WlApiClient;

internal class WlApiClient
{
    private static readonly HttpClient client = new()
    {
        BaseAddress = new Uri("https://wl-api.mf.gov.pl/api/search/nip/")
    };

    public WlApiClient()
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }
    public async Task<IWlResponse?> CheckNipAsync(string nip)
    {
        var builder = new UriBuilder(client.BaseAddress!);
        var query = HttpUtility.ParseQueryString(builder.Query);
        query["date"] = DateTime.Now.ToString("yyyy-MM-dd");
        builder.Query = query.ToString();
        builder.Path += "/" + nip;

        string url = builder.ToString();
        var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(httpRequest);
        var json = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<WlEntityResponse>(json);
        }
        else
        {
            return JsonSerializer.Deserialize<WlExceptionResponse>(json);
        }
    }
}
