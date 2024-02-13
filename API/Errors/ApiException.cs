namespace API.Errors;

public class ApiException
{
    public ApiException(int statusCode, string message, string details)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;
    }
    public int StatusCode { get; set; }
    public String Message { get; set; }
    public String Details { get; set; }
}
