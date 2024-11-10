namespace StockControl.Application.Requests;

public class ErrorResponse
{
    public ErrorResponse(int statusCode, string description)
    {
        StatusCode = statusCode;
        Description = description;
        Timestamp = DateTime.UtcNow;
    }

    public int StatusCode { get; set; }
    public string Description { get; set; }
    public DateTime Timestamp { get; set; }
}