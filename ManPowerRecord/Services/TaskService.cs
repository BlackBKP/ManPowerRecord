using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class TaskService : ITask
    {
        IConnectDB Database;

        public TaskService()
        {
            this.Database = new ConnectDB();
        }

        public List<TaskModel> GetTasks()
        {
            List<TaskModel> tasks = new List<TaskModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM Tasks LEFT JOIN Jobs ON Tasks.job_id = Jobs.job_id";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    TaskModel task = new TaskModel()
                    {
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        task_name = data_reader["task_name"] != DBNull.Value ? data_reader["task_name"].ToString() : "",
                        task_description = data_reader["task_description"] != DBNull.Value ? data_reader["task_description"].ToString() : "",
                        status = data_reader["status"] != DBNull.Value ? data_reader["status"].ToString() : "",
                    };
                    tasks.Add(task);
                }
                data_reader.Close();
            }

            connection.Close();
            return tasks;
        }

        public string CreateTask(TaskModel task)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using(SqlCommand command = new SqlCommand("INSERT INTO Tasks(task_name, job_id) " +
                                                      "VALUES(@task_name, @job_id)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@task_name", task.task_name);
                command.Parameters.AddWithValue("@job_id", task.job_id);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }

        public string UpdateTask(TaskModel task)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using(SqlCommand command = new SqlCommand("UPDATE TASKS SET " +
                "task_name = @task_name, " +
                "job_id = @job_id, " +
                "WHERE task_id = @task_id", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@task_id", task.task_id);
                command.Parameters.AddWithValue("@task_name", task.task_name);
                command.Parameters.AddWithValue("@job_id", task.job_id);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }
    }
}
