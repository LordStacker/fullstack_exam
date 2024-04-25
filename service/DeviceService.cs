using repository;
using repository.Models;

namespace service
{
    public class DeviceService
    {
        private readonly DeviceRepository _deviceRepository;

        public DeviceService(DeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public IEnumerable<Device> GetAllDevices()
        {
            try
            {
                return _deviceRepository.GetAllDevices();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not get all devices due to this error: {ex.Message}");
            }
        }
        public Device GetDevice(int id)
        {
            try 
            {
                return _deviceRepository.GetDeviceById(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not get device with id: {id} due to: {ex.Message}");
            }
        }
        public Device CreateDevice(string deviceName, int userId)
        {
            var deviceToCreate = new Device
            {
                DeviceName = deviceName,
                UserId = userId
            };

            try
            {
                return _deviceRepository.CreateDevice(deviceToCreate);  
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not create this device due to this error: {ex.Message}");
            }
        }
        public void DeleteDevice(int id)
        {
            try
            {
                _deviceRepository.DeleteDevice(id);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not delete device with id: {id} due to this error: {ex.Message}");
            }
        }
    }
}