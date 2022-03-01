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

            string string_command = "SELECT * FROM [Users]";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    UserModel user = new UserModel()
                    {
                        user_no = data_reader["user_no"] != DBNull.Value ? Convert.ToInt32(data_reader["user_no"]) : default(Int32),
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

        public string CreateUser(UserModel user)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using (SqlCommand command = new SqlCommand("INSERT INTO [Users](user_id, user_name, department) VALUES(@user_id, @user_name, @department)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@user_id", user.user_id);
                command.Parameters.AddWithValue("@user_name", user.user_name);
                command.Parameters.AddWithValue("@department", user.department);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }

        public string UpdateUser(UserModel user)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using (SqlCommand command = new SqlCommand("UPDATE [Users] SET " +
                "user_id = @user_id, " +
                "user_name = @user_name, " +
                "department = @department, " +
                "WHERE user_no = @user_no", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@user_id", user.user_id);
                command.Parameters.AddWithValue("@user_name", user.user_name);
                command.Parameters.AddWithValue("@department", user.department);
                command.Parameters.AddWithValue("@user_no", user.user_no);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }
    }
}
