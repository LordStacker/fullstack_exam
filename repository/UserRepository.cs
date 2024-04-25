using Dapper;
using Npgsql;
using repository.Interfaces;
using repository.Models;

namespace repository
{
    public class UserRepository : IUserRepostitory
    {
        private readonly NpgsqlDataSource _dataSource;

        public UserRepository(NpgsqlDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
           await using (var connection = _dataSource.OpenConnection())
            {
                return await  connection.QueryAsync<User>($@"select
                user_id as {nameof(User.Id)},
                username as {nameof(User.Username)},
                email as {nameof(User.Email)}
                from public.user");
            }
        }

        public User GetUserById(int id)
        {
            using var connection = _dataSource.OpenConnection();

            return connection.QueryFirst<User>($@"Select
            user_id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            email as {nameof(User.Email)}
            from public.user where user_id=@id;", new {id = id});
        }

        public User CheckIfUsernameExists(string usernameToCheck)
        {

            using var connection = _dataSource.OpenConnection();

            var user = connection.QueryFirstOrDefault<User>($@"Select
            user_id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            email as {nameof(User.Email)},
            password as {nameof(User.Password)}
            from public.user where username = @usernameToCheck;", new {usernameToCheck})!;

            return user ?? new User();
        }

        public User ValidateUser(string username, string password)
        {
            using var connection = _dataSource.OpenConnection();

            return connection.QueryFirst<User>($@"select 
            user_id as {nameof(User.Id)},
            username as {nameof(User.Username)},
            email as {nameof(User.Email)},
            password as {nameof(User.Password)}
            from public.user where username=@username and password=@password;
            ", new {username, password});
        }

        public User CreateUser(User user)
        {
             using var connection = _dataSource.OpenConnection();

            return connection.QueryFirst<User>(@$"insert into public.user
            (username, email, password) values (@username,@email , @password)
            Returning *;", 
            new {username = user.Username, email = user.Email, password = user.Password});
        }

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

    }
}