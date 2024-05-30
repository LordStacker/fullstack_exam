namespace repository.Models;

public class Token : BaseModel
{
    public int? UserId { get; set; }
    public string? TokenValue { get; set; }
}