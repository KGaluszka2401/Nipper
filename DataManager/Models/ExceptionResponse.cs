namespace Nipper.DataManager.Models;

public class ExceptionResponse : IResponse
{
    public string Message { get; set; }
    public string Code { get; set; }
}
