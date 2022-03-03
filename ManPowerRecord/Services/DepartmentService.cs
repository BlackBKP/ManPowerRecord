using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class DepartmentService : IDepartment
    {
        IConnectDB Database;

        public DepartmentService()
        {
            this.Database = new ConnectDB();
        }

        public List<DepartmentModel> GetDepartments()
        {
            List<DepartmentModel> departments = new List<DepartmentModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"SELECT * FROM Departments");
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    DepartmentModel department = new DepartmentModel()
                    {
                        department_id = data_reader["department_id"] != DBNull.Value ? Convert.ToInt32(data_reader["department_id"]) : default(int),
                        department_name = data_reader["department_name"] != DBNull.Value ? data_reader["department_name"].ToString() : "",
                        description = data_reader["description"] != DBNull.Value ? data_reader["description"].ToString() : ""
                    };
                    departments.Add(department);
                }
                data_reader.Close();
            }
            connection.Close();
            return departments;
        }

        public string CreateDepartment(DepartmentModel department)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                INSERT INTO 
                    [Departments](department_name, description) 
                    VALUES(@department_name, @description)");
            using (SqlCommand command = new SqlCommand(string_command, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@department_name", department.department_name);
                command.Parameters.AddWithValue("@description", department.description);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }

        public string UpdateUser(DepartmentModel department)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                UPDATE [Departments] 
                SET
                    department_name = @department_name,
                    description = @description,
                WHERE department_id = @department_id");
            using (SqlCommand command = new SqlCommand(string_command, connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@department_name", department.department_name);
                command.Parameters.AddWithValue("@description", department.description);
                command.Parameters.AddWithValue("@department_id", department.department_id);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }
    }
}
