using repository;
using repository.Models;

namespace service
{
    public class SensorService
    {
        private readonly SensorRepository _sensorRepository;

        public SensorService(SensorRepository sensorRepository)
        {
            _sensorRepository = sensorRepository;
        }

        public IEnumerable<Sensor> GetAllSensors()
        {
            try
            {
                return _sensorRepository.GetAllSensors();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not return all the sensors due to this: {ex.Message}");
            }
        }
        public Sensor GetSensorById(int sensorId)
        {
            try
            {
                return _sensorRepository.GetSensorById(sensorId);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not return the sensor with id: {sensorId} due to this: {ex.Message}");
            }
        }
        public Sensor CreateSensor(int deviceId, decimal soundLevel, decimal temperature, decimal humidity, DateTime date)
        {
            var sensorToCreate = new Sensor
            {
                DeviceId = deviceId,
                SoundLevel = soundLevel,
                Tempreature = temperature,
                Humidity = humidity,
                Date = date
            };

            try
            {
                return  _sensorRepository.CreateSensor(sensorToCreate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create the sensor: {sensorToCreate} due to: {ex.Message}");
            }
        }
        public void DeleteSensor(int sensorId)
        {
            try
            {
                _sensorRepository.DeleteSensor(sensorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not delete sensor with id {sensorId} due to this: {ex.Message}");
            }
        }

    }
}
