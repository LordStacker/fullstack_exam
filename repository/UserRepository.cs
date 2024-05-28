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
    
            using var transaction = connection.BeginTransaction();
    
            try
            {
               
                var insertedUser = connection.QueryFirst<User>(
                    @"INSERT INTO public.user
              (username, email, password)
              VALUES (@username, @email, @password)
              RETURNING user_id AS Id, username, email, password;",
                    user,
                    transaction);

                var insertedDevice = connection.QueryFirst<Device>(
                    @"INSERT INTO device
              (device_name)
              VALUES (@deviceName)
              RETURNING device_id AS Id, device_name;",
                    new { deviceName = user.Username },
                    transaction);
        
                connection.Execute(
                    @"INSERT INTO user_to_device
              (user_id, device_id)
              VALUES (@userId, @deviceId);",
                    new { userId = insertedUser.Id, deviceId = insertedDevice.Id },
                    transaction);

                transaction.Commit();
        
                return insertedUser;
            }
            catch (Exception)
            {
                
                transaction.Rollback();
                throw; 
            }
        }



        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        
public Token UpsertToken(Token token)
        {
            using var connection = _dataSource.OpenConnection();

            try
            {
                connection.QueryFirstOrDefault<Token>(
                    @"INSERT INTO public.user_tokens
              (user_id, fcm_token)
              VALUES (@UserId, @TokenValue) ON CONFLICT (user_id) DO
                UPDATE
                SET fcm_token = @TokenValue
                    ;", new Token {UserId = token.UserId, TokenValue = token.TokenValue});}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
               

                

          
            

            return token;
        }

    }
}