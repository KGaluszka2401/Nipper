namespace Nipper.DataManager.ApClients.WlApiClient.Models;

public class WlExceptionResponse : IWlResponse
{
    public string code { get; set; }
    public string message { get; set; }
}
