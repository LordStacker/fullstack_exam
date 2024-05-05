using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using repository.Models;

namespace repository.Interfaces
{
    public interface IUserToDeviceRepisotory
    {
        IEnumerable<UserToDevice> GetAllUserToDevice();
        UserToDevice GetUserToDeviceById(int id);
        UserToDevice CreateUserToDevice(UserToDevice device);
        void DeleteUserToDevice(int id);
    }
}