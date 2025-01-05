using Nipper.DataManager.Clients;
using Nipper.DataManager.Models;

namespace Nipper.DataManager.Utilities;

public class NipValidator
{
    private static readonly WlApiClient wlApiClient = new();
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

            if (char.IsLetter(nip[0]) && char.IsLetter(nip[1]))
            {
                yield return new()
                {
                    Nip = nip,
                    ErrorMesage = null,
                    CompanyName = "Chuj wie czy istnieje, eu, potem dodam api calla"
                };
                continue;
            }

            requestsSentBeforeBreak += 1;
            if (requestsSentBeforeBreak >= 20)
            {
                requestsSentBeforeBreak = 0;
                await Task.Delay(3000);
            }

            var response = await wlApiClient.CheckNip(nip);
            CompanyInfo companyInfo = new()
            {
                Nip = nip,
                ErrorMesage = null,
                CompanyName = null
            };
            switch (response)
            {
                case EntityResponse entityResponse:
                    if (entityResponse.Result.Subject == null)
                    {
                        companyInfo.ErrorMesage = "Nie istnieje firma o podanym nipie";
                        break;
                    }
                    companyInfo.CompanyName = entityResponse.Result.Subject.Name;
                    break;
                case ExceptionResponse exceptionResponse:
                    companyInfo.ErrorMesage =
                        $"Wystąpił błąd {exceptionResponse.Code}: {exceptionResponse.Message}";
                    break;
                default:
                    companyInfo.ErrorMesage = "Niezidentyfikowana akcja";
                    break;
            }
            yield return companyInfo;
        }
    }
}
