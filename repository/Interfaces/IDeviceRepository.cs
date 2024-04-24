using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using repository.Models;

namespace repository.Interfaces
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetAllDevices();
        Device GetDeviceById(int id);
        Device CreateDevice(Device device);
        void DeleteDevice(int id);

    }

}