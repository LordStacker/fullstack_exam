using Dapper;
using Npgsql;
using repository.Interfaces;
using repository.Models;

namespace repository
{
    public class UserToDeviceRepository : IUserToDeviceRepisotory
    {
        private readonly NpgsqlDataSource _dataSource;

        public UserToDeviceRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public IEnumerable<UserToDevice> GetAllUserToDevice()
        {
            using var conn = _dataSource.OpenConnection();

            return conn.Query<UserToDevice>($@"Select
            utd.user_id as {nameof(UserToDevice.UserId)},
            utd.device_id as {nameof(UserToDevice.DeviceId)}
            from public.user_to_device as utd
            inner join public.user as u on utd.user_id = u.id
            inner join public.device as d on utd.device_id = d.id;
            ");
        }

        public UserToDevice GetUserToDeviceById(int id)
        {
            using var conn = _dataSource.OpenConnection();

            return conn.QueryFirst<UserToDevice>($@"Select
            utd.user_id as {nameof(UserToDevice.UserId)},
            utd.device_id as {nameof(UserToDevice.DeviceId)}
            from public.user_to_device as utd
            inner join public.user as u on utd.user_id = u.id
            inner join public.device as d on utd.device_id = d.id where utd.user_id=@id;
            ", id);
        }
        public UserToDevice CreateUserToDevice(UserToDevice userToDevice)
        {
            using var conn = _dataSource.OpenConnection();

            return conn.QueryFirst<UserToDevice>($@"insert into 
            public.user_to_device (user_id, device_id)
            values (userId, deviceId)
            returning *;", new {userId = userToDevice.UserId, deviceId = userToDevice.DeviceId});
        }

        public void DeleteUserToDevice(int id)
        {
            using var conn = _dataSource.OpenConnection();

            conn.Execute($@"Delete from public.user_to_device where user_id=@id", id);
        }

       
    }
}