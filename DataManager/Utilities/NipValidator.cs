using Nipper.DataManager.ApClients.ViesApiClient;
using Nipper.DataManager.ApClients.WlApiClient.Models;
using Nipper.DataManager.ApiClients.ViesApiClient.Models;
using Nipper.DataManager.ApiClients.WlApiClient;
using Nipper.DataManager.Models;

namespace Nipper.DataManager.Utilities;

public class NipValidator
{
    private static readonly WlApiClient wlApiClient = new();
    private static readonly ViesApiClient viesApiClient = new();
    public async IAsyncEnumerable<CompanyInfo> CheckNipsAsync(string[] nips)
    {
        int requestsSentBeforeBreak = 0;
        foreach (string nip in nips)
        {
            if (string.IsNullOrEmpty(nip) || nip.Length < 2)
            {
                yield return new()
                {
                    Nip = nip,
                    ErrorMesage = "Nip jest za krótki",
                    CompanyName = null
                };
                continue;
            }

            requestsSentBeforeBreak += 1;
            if (requestsSentBeforeBreak >= 20)
            {
                requestsSentBeforeBreak = 0;
                await Task.Delay(3000);
            }

            CompanyInfo companyInfo = new()
            {
                Nip = nip,
                ErrorMesage = null,
                CompanyName = null
            };

            if (char.IsLetter(nip[0]) && char.IsLetter(nip[1]))
            {
                yield return await ValidateViesNipAsync(nip, companyInfo);
                continue;
            }

            yield return await ValidateEuNipAsync(nip, companyInfo);
        }
    }

    private static async Task<CompanyInfo> ValidateEuNipAsync(string nip, CompanyInfo companyInfo)
    {
        var wlResponse = await wlApiClient.CheckNipAsync(nip);
        switch (wlResponse)
        {
            case WlEntityResponse entityResponse:
                if (entityResponse == null || entityResponse.result == null || entityResponse.result.subject == null)
                {
                    companyInfo.ErrorMesage = "Nie istnieje firma o podanym nipie";
                    break;
                }
                companyInfo.CompanyName = entityResponse.result.subject.name;
                break;
            case WlExceptionResponse exceptionResponse:
                companyInfo.ErrorMesage =
                    $"Wystąpił błąd {exceptionResponse.code}: {exceptionResponse.message}";
                break;
            default:
                companyInfo.ErrorMesage = "Wystąpił niezidentyfikowany błąd";
                break;
        }
        return companyInfo;
    }

    private static async Task<CompanyInfo> ValidateViesNipAsync(string nip, CompanyInfo companyInfo)
    {
        var viesResponse = await viesApiClient.CheckNipAsync(nip[..2], nip);
        switch (viesResponse)
        {
            case ViesEntityResponse entityResponse:
                if (!entityResponse.isVald)
                {
                    companyInfo.ErrorMesage = "Nie istnieje firma o podanym nipie";
                    break;
                }
                companyInfo.CompanyName = entityResponse.name;
                break;
            case ViesExceptionResponse entityResponse:
                companyInfo.ErrorMesage = entityResponse.ErrorMesssage;
                break;
            default:
                companyInfo.ErrorMesage = "Wystąpił niezidentyfikowany błąd";
                break;
        }
        return companyInfo;
    }
}
