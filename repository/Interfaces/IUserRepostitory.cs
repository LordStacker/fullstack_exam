using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using repository.Models;

namespace repository.Interfaces
{
    public interface IUserRepostitory
    {
        Task<IEnumerable<User>> GetAllUsers();
        User GetUserById(int id);
        User CheckIfUsernameExists(string username);
        User ValidateUser(string username, string password);
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(int id);
    }
}