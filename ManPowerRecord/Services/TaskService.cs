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

        public string CreateTask(TaskModel task)
        {
            this.Database = new ConnectDB();
            SqlConnection connection = Database.Connect();
            connection.Open();
            using(SqlCommand command = new SqlCommand("INSERT INTO Tasks(task_id, task_name, task_description, job_id, status) " +
                                                      "VALUE(@task_id, @task_name, @task_description, @job_id, @status)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@task_id", task.task_id);
                command.Parameters.AddWithValue("@task_name", task.task_name);
                command.Parameters.AddWithValue("@task_description", task.task_description);
                command.Parameters.AddWithValue("@job_id", task.job_id);
                command.Parameters.AddWithValue("@status", task.status);
            }
            connection.Close();
            return "Success";
        }
    }
}
