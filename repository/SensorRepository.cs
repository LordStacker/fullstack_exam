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
            temperature as {nameof(Sensor.Tempreature)},
            humidity as {nameof(Sensor.Humidity)},
            date as {nameof(Sensor.Date)}
            from public.sensor;");
        }

        public Sensor GetSensorById(int id)
        {
            using var connection = _dataSource.OpenConnection();

            return connection.QueryFirst<Sensor>($@"select
            sensor_id as {nameof(Sensor.Id)},
            device_id as {nameof(Sensor.DeviceId)},
            sound_level as {nameof(Sensor.SoundLevel)},
            temperature as {nameof(Sensor.Tempreature)},
            humidity as {nameof(Sensor.Humidity)},
            date as {nameof(Sensor.Date)}
            from public.sensor where sensor_id=@id;", new {id});

        }

        public Sensor CreateSensor(Sensor sensor)
        {
            using var connection = _dataSource.OpenConnection();

            return connection.QueryFirst<Sensor>($@"insert into
            public.sensor (device_id, sound_level, temperature, humidity, date)
            values (@deviceId, @soundLevel, @temperature, @humidity, @date)
            returning *;
            ", new {deviceId = sensor.DeviceId, soundLevel = sensor.SoundLevel, temperature = sensor.Tempreature,
                    humidity = sensor.Humidity, date = sensor.Date});
        }

        public Sensor UpdateSensor(Sensor sensor)
        {
            throw new NotImplementedException();
        }

        public void DeleteSensor(int sensorId)
        {
            using var connection = _dataSource.OpenConnection();

            connection.Execute($@"delete from public.sensor where sensor_id=@id;", new {id = sensorId});
        }

    }
}