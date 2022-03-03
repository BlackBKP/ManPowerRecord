using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class AnalysisService : IAnalysis
    {
        IConnectDB Database;

        public AnalysisService()
        {
            this.Database = new ConnectDB();
        }

        public List<TaskTotalHoursModel> GetTasksWorkingHours(string job_id)
        {
            List<TaskTotalHoursModel> tasks = new List<TaskTotalHoursModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                SELECT
                    [WH2022].job_id,
                    [Jobs].job_name,
                    [WH2022].task_id,
                    [Tasks].task_name,
                    SUM(DATEDIFF(HOUR, [WH2022].start_time, [WH2022].stop_time)) as total_hours
                FROM [WH2022]
                    LEFT JOIN [Jobs] ON [WH2022].job_id = [Jobs].job_id
                    LEFT JOIN [Tasks] ON [WH2022].task_id = [Tasks].task_id
                WHERE [WH2022].job_id = '{job_id}'
                Group by [WH2022].job_id, [Jobs].job_name, [WH2022].task_id, [Tasks].task_name");
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    TaskTotalHoursModel task = new TaskTotalHoursModel()
                    {
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        task_name = data_reader["task_name"] != DBNull.Value ? data_reader["task_name"].ToString() : "",
                        hours = data_reader["total_hours"] != DBNull.Value ? Convert.ToInt32(data_reader["total_hours"].ToString()) : 0,
                    };
                    tasks.Add(task);
                }
                data_reader.Close();
            }
            connection.Close();
            return tasks;
        }

        public List<JobInvolveModel> GetPercentsInvolve(string job_id)
        {
            List<JobInvolveModel> invs = new List<JobInvolveModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                SELECT
                    [WH2022].job_id,
                    [Jobs].job_name,
                    [WH2022].user_id,
                    [Users].user_name,
                    SUM(DATEDIFF(HOUR, [WH2022].start_time, [WH2022].stop_time)) as total_hours
                FROM [WH2022]
                    LEFT JOIN [Jobs] ON [WH2022].job_id = [Jobs].job_id
                    LEFT JOIN [Users] ON [WH2022].user_id = [Users].user_id
                WHERE [WH2022].job_id = '{job_id}'
                Group by [WH2022].job_id, [Jobs].job_name, [WH2022].user_id, [Users].user_name");
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();
            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    JobInvolveModel inv = new JobInvolveModel()
                    {
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        user_id = data_reader["user_id"] != DBNull.Value ? data_reader["user_id"].ToString() : "",
                        user_name = data_reader["user_name"] != DBNull.Value ? data_reader["user_name"].ToString() : "",
                        hours = data_reader["total_hours"] != DBNull.Value ? Convert.ToInt32(data_reader["total_hours"].ToString()) : 0,
                    };
                    invs.Add(inv);
                }
                data_reader.Close();
            }
            connection.Close();
            return invs;
        }
    }
}
