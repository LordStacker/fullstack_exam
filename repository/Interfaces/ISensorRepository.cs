using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using repository.Models;

namespace repository.Interfaces
{
    public interface ISensorRepository
    {
        IEnumerable<Sensor> GetAllSensors();
        Sensor GetSensorById(int id);
        Sensor CreateSensor(Sensor sensor);
        Sensor UpdateSensor(Sensor sensor);
        void DeleteSensor(int sensorId);
    }
}