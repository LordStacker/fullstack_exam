using Dapper;
using Npgsql;
using repository.Interfaces;
using repository.Models;

namespace repository
{
    public class DeviceRepository : IDeviceRepository
    {   
        private readonly NpgsqlDataSource _dataSource;

        public DeviceRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<Device> GetAllDevices()
        {
            using var connection = _dataSource.OpenConnection();

            return  connection.Query<Device>($@"select
            device_id as {nameof(Device.Id)},
            device_name as {nameof(Device.DeviceName)},
            user_id as {nameof(Device.UserId)}
            from public.device;
            ");
        }   

        public Device GetDeviceById(int id)
        {
            using var connection = _dataSource.OpenConnection();

            return  connection.QueryFirst<Device>($@"select
            device_id as {nameof(Device.Id)},
            device_name as {nameof(Device.DeviceName)},
            user_id as {nameof(Device.UserId)}
            from public.device where device_id= @id;
            ", new {id});
        }
        public Device CreateDevice(Device device)
        {
            using (var connection = _dataSource.OpenConnection())

            return connection.QueryFirst<Device>(@$"insert into public.device
            (device_name, user_id) values (@deviceName, @userId)
            returning *;", new {deviceName = device.DeviceName, userId = device.UserId});
        }

        public void DeleteDevice(int id)
        {
            using var connection = _dataSource.OpenConnection();

            connection.Execute($@"Delete from public.device where device_id=@id", new {id});
        }

        
    }
}