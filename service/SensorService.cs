using System.Text.Json;
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
        public Sensor GetSensorByUserId(int userId)
        {
            try
            {
                return _sensorRepository.GetSensorByUserId(userId);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not return the sensor with id: {userId} due to this: {ex.Message}");
            }
        }
        public Sensor CreateSensor(int deviceId, decimal soundLevel, int temperature, int humidity, DateTime date)
        {
        
            try
            {    var sensorToCreate = new Sensor
                                            {
                                                DeviceId = deviceId,
                                                SoundLevel = soundLevel,
                                                Temperature =  temperature,
                                                Humidity =  humidity,
                                                Date = date
                                            };
                                            Console.WriteLine("new sensor: "+JsonSerializer.Serialize(sensorToCreate));

                return  _sensorRepository.CreateSensor(sensorToCreate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;

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
        public Sensor UpdateSensor(int sensorId, decimal soundLevel,
                                     decimal temperature, decimal humidity)
        {
            var updatedSensor = new Sensor
            {
                SoundLevel = soundLevel,
                Temperature = temperature,
                Humidity = humidity,
                Date = DateTime.UtcNow
            };

            try
            {
                return _sensorRepository.UpdateSensor(updatedSensor, sensorId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Sensor {sensorId} was not updated due to: {ex.Message}");
            }
        }

         public void SendNotification(int userId, string message)
        {
            var monitorAlertToCreate = new MonitorAlert
            {
                UserId = userId,
                CreatedAt = DateTime.Now.ToString("HH:MM:ss"),
                Message = message
            };
            try
            {
                _sensorRepository.Notification(monitorAlertToCreate);
            }
            catch(Exception ex)
            {
                throw new Exception ($"Could not send a message back due to: {ex}!");
            }
        }

    }
}
