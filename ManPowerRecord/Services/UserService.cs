using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class UserService : IUser
    {
        IConnectDB Database;

        public UserService()
        {
            this.Database = new ConnectDB();
        }

        public List<UserModel> GetUsers()
        {
            List<UserModel> users = new List<UserModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM [User]";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    UserModel user = new UserModel()
                    {
                        user_id = data_reader["user_id"] != DBNull.Value ? data_reader["user_id"].ToString() : "",
                        user_name = data_reader["user_name"] != DBNull.Value ? data_reader["user_name"].ToString() : "",
                        department = data_reader["department"] != DBNull.Value ? data_reader["department"].ToString() : "",
                    };
                    users.Add(user);
                }
                data_reader.Close();
            }

            connection.Close();
            return users;
        }
    }
}
