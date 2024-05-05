using System.Diagnostics;
using repository;
using repository.Models;

namespace service
{
    public class UserToDeviceService
    {
        private readonly UserToDeviceRepository _repository;

        public UserToDeviceService(UserToDeviceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UserToDevice> GetAllUserToDevice()
        {
            try
            {
                return _repository.GetAllUserToDevice();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not get all user to devices due to: {ex.Message}");
            }
        }
        public UserToDevice GetUserToDevice(int id)
        {
            try
            {
                return _repository.GetUserToDeviceById(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not get the user to device with id: {id} due to: {ex.Message}");
            }
        }
        public UserToDevice CreateUserToDevice(int userId, int deviceId)
        {
            var userToDeviceToCreate = new UserToDevice
            {
                UserId = userId,
                DeviceId = deviceId
            };

            try
            {
                return _repository.CreateUserToDevice(userToDeviceToCreate);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not create user to device with user id: {userId} and device id {deviceId} due to {ex.Message}");
            }
        }
        public void DeleteUserToDevice (int id)
        {
            try 
            {
                _repository.DeleteUserToDevice(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not delete user to device with user id: {id} due to {ex.Message}");
            }
        }

    }
}