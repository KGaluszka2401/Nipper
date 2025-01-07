namespace Nipper.DataManager.ApiClients.ViesApiClient.Models
{
    internal class ViesExceptionResponse : IViesResponse
    {
        public required string ErrorMesssage { get; set; }
    }
}
