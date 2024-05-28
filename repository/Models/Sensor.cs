namespace repository.Models
{
    public class Sensor : BaseModel
    {
        public int DeviceId { get; set; }
        public decimal SoundLevel { get; set; }
        public decimal Temperature { get; set; }
        public decimal Humidity { get; set; }
        public DateTime Date { get; set; }
    }
}