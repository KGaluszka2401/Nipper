using System.Web;
using Newtonsoft.Json;
using Nipper.DataManager.Models;

namespace Nipper.DataManager.Clients;

public class WlApiClient
{
    private static readonly HttpClient client = new()
    {
        BaseAddress = new Uri("https://wl-test.mf.gov.pl/api/search/nip/")
    };

    public WlApiClient()
    {
        client.DefaultRequestHeaders.Clear();
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }
    public async Task<IResponse?> CheckNip(string nip)
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
            return JsonConvert.DeserializeObject<EntityResponse>(json);
        }
        else
        {
            return JsonConvert.DeserializeObject<ExceptionResponse>(json);
        }
    }
}
