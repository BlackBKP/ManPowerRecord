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

            string string_command = "SELECT * FROM Department";
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
    }
}
