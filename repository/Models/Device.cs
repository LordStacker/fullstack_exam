namespace repository.Models
{
    public class Device : BaseModel
    {
        public string? DeviceName { get; set; }
        public int UserId { get; set; }
    }
}