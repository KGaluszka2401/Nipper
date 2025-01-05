namespace Nipper.DataManager.ApClients.WlApiClient.Models;

public class WlExceptionResponse : IWlResponse
{
    public string Message { get; set; }
    public string Code { get; set; }
}
