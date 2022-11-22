using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using System.Data;
using MedScan.Models;

namespace MedScan.Services
{
    public class UsersDAO : IUserManagement
    {
        string connectionstring = "Server=localhost;Port=5434;Database=medscan;User Id=postgres;Password=Dabking1705";
        //basic info needed to connect to Postgres DB

        public bool ValidateUserByUsernameAndPassword(User user)
        {
            bool successfulLogin = false;
            //presets this variable for failure, will only become true if user is validated

            string search = "select * from \"user\" where username = @username and password = @password";
            //@ represents variables whose values can be assigned later
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                //makes new connection object and then opens it to connect to DB

                NpgsqlCommand command = new NpgsqlCommand(search, connection);
                //makes new command object that takes in the SQL command as well as the connection

                command.Parameters.Add("@username", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Username;
                command.Parameters.Add("@password", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Password;
                //assigns values to variables/parameters

                try
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    //creates new reader object which executes the command

                    if (reader.HasRows)
                    {
                        //reader will execute command and this will return rows(s) of values - if this is the case, the data transaction has worked
                        successfulLogin = true;
                        //changes tracking variable to true, meaning success
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                //all within a try catch loop to prevent program from crashing
            }

            return successfulLogin;
        }

        public int CreateUser(User user)
        {
            int creationSuccess = 1;
            string create = "insert into \"user\" values(@username, @password);";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(create, connection);

                command.Parameters.Add("username", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Username;
                command.Parameters.Add("password", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Password;
                try
                {
                    if (CheckIfUserIsPresentInSystem(user) == false)
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        creationSuccess = 0;
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return creationSuccess;
        }

        public int DeleteUser(User user)
        {
            int deletionSuccess = 1;
            string delete = "delete from \"user\" where username = @username and password = @password;";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(delete, connection);

                command.Parameters.Add("username", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Username;
                command.Parameters.Add("password", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Password;

                try
                {
                    if (CheckIfUserIsPresentInSystem(user) == true)
                    {
                        NpgsqlDataReader reader = command.ExecuteReader();
                        deletionSuccess = 0;
                    }        
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return deletionSuccess;
        }

        public List<User> GetUsers()
        {
            List<User> allUsers = new List<User>();
            string search = "select * from \"user\" order by Username asc";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(search, connection);
                try
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        allUsers.Add(new User { Username = (string)reader[0] });
                        //adding to a list of users new User objects. Anything inside {} denotes the properties with which these User objects
                        //are instantiated
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                connection.Close();

                return allUsers;
            }
        }

        public bool CheckIfUserIsPresentInSystem(User user)
        {
            bool userPresentInSystem = false;
            string check = "select * from \"user\" where username = @username";

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionstring))
            {
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand(check, connection);

                command.Parameters.Add("username", NpgsqlTypes.NpgsqlDbType.Varchar, 40).Value = user.Username;

                try
                {
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        userPresentInSystem = true;
                    }
                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return userPresentInSystem;
        }
    }
}