using Dapper;
using Npgsql;
using repository.Interfaces;
using repository.Models;

namespace repository
{
    public class SensorRepository : ISensorRepository
    {
        private readonly NpgsqlDataSource _dataSource;

        public SensorRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Sensor> GetAllSensors()
        {
            using var connection = _dataSource.OpenConnection();

            return connection.Query<Sensor>($@"select
            sensor_id as {nameof(Sensor.Id)},
            device_id as {nameof(Sensor.DeviceId)},
            sound_level as {nameof(Sensor.SoundLevel)},
            temperature as {nameof(Sensor.Temperature)},
            humidity as {nameof(Sensor.Humidity)},
            date as {nameof(Sensor.Date)}
            from public.sensor;");
        }

        public Sensor GetSensorByUserId(int userId)
        {
            using var connection = _dataSource.OpenConnection();

            var sensor = connection.QueryFirstOrDefault<Sensor>($@"
        SELECT 
            s.sensor_id AS {nameof(Sensor.Id)},
            s.device_id AS {nameof(Sensor.DeviceId)},
            s.sound_level AS {nameof(Sensor.SoundLevel)},
            s.temperature AS {nameof(Sensor.Temperature)},
            s.humidity AS {nameof(Sensor.Humidity)},
            s.date AS {nameof(Sensor.Date)}
        FROM 
            public.sensor AS s
        INNER JOIN 
            public.device AS d ON s.device_id = d.device_id
        INNER JOIN 
            public.user_to_device AS utd ON d.device_id = utd.device_id
        WHERE 
            utd.user_id = @userId
            AND
            s.date = (
                SELECT MAX(date) 
                FROM public.sensor 
                WHERE device_id = s.device_id
            );",
                new { userId });

            if (sensor == null)
            {
                throw new Exception($"No sensor data found for user with ID: {userId}");
            }

            return sensor;
        }



        public Sensor CreateSensor(Sensor sensor)
        {
            using (var connection = _dataSource.OpenConnection())
            {
                Console.WriteLine("connection opened");
                   return connection.QueryFirst<Sensor>($@"insert into
                            public.sensor (device_id, sound_level, temperature, humidity, date)
                            values (@deviceId, @soundLevel, @temperature, @humidity, @date)
                            returning *;",
                             new {deviceId = sensor.DeviceId, soundLevel = sensor.SoundLevel, temperature = sensor.Temperature,
                                    humidity = sensor.Humidity, date = sensor.Date});
            }

         
        }

        public Sensor UpdateSensor(Sensor sensor, int sensorId)
        {
            using var connection = _dataSource.OpenConnection();

            return connection.QueryFirst<Sensor>($@"update  public.sensor
            SET
            sound_level = @soundLevel,
            temperature = @temperature,
            humidity = @humidity,
            date = @date
            WHERE sensor_id = @sensorId
            RETURNING *;", new {soundLevel = sensor.SoundLevel, temperature = sensor.Temperature,
                                humidity = sensor.Humidity, date = sensor.Date, sensorId});
        }

        public void DeleteSensor(int sensorId)
        {
            using var connection = _dataSource.OpenConnection();

            connection.Execute($@"delete from public.sensor where sensor_id=@id;", new {id = sensorId});
        }

        public void Notification(MonitorAlert monitorAlert)
        {
            using var connection = _dataSource.OpenConnection();
            

            connection.Execute($@"Insert into alerts (user_id, created_at, alert_body)
                                 values (@userId, @createdAt, @alertBody);",
                                  new {userId = monitorAlert.UserId, 
                                        createdAt = monitorAlert.CreatedAt,
                                        alertBody = monitorAlert.Message});
        }
        
        public int GetUserIdByDeviceId(int deviceId)
        {
            using var connection = _dataSource.OpenConnection();
            var userId = connection.QueryFirstOrDefault<int>(
                @"SELECT user_id
          FROM user_to_device
          WHERE device_id = @deviceId;",
                new { deviceId });

            return userId;
        }
    }
}