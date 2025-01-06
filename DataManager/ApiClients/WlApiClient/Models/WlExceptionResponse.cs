namespace Nipper.DataManager.ApiClients.WlApiClient.Models;

internal class WlExceptionResponse : IWlResponse
{
    public string? code { get; set; }
    public string? message { get; set; }
}
