using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using repository;
using repository.Models;

namespace service
{
    public class DeviceService
    {
        private readonly UserRepository _repository;

        public DeviceService(UserRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return (IEnumerable<User>)_repository.GetAllUsers();
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not get all the users due to this exception: {ex.Message}");
            }
        }
        public User GetUserById(int id)
        {
            try 
            {
                return _repository.GetUserById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find user with id: {id} {ex.Message}");
            }
        }
        public User CheckIfUsernameExists(string username)
        {
            try
            {
                return _repository.CheckIfUsernameExists(username);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not find username {username} {ex.Message}");
            }
        }
        public User ValidateUser(string username, string password)
        {
            try
            {
                return _repository.ValidateUser(username, password);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not find user with username and password {username} {password} due: {ex.Message}");
            }
        }
        public User CreateUser(string username, string email, string password)
        {
            var userToCreate = new User
            {
                Username = username,
                Email = email,
                Password = password
            };

            try
            {
                return _repository.CreateUser(userToCreate);
            }
            catch(Exception ex)
            {
                throw new Exception($"Could not create user this user due to this error: {ex.Message}");
            }
        }


    }
}