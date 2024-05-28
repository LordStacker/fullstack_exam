using Newtonsoft.Json;

namespace repository.Models;

public class SensorData : BaseModel
{
    public int device_id { get; set; }
    public int sound_level { get; set; }
    public double temperature { get; set; }
    public double humidity { get; set; }
}