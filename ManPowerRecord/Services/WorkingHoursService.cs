using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class WorkingHoursService : IWorkingHours
    {
        IConnectDB Database;

        public WorkingHoursService()
        {
            this.Database = new ConnectDB();
        }

        public List<WorkingHoursModel> GetWorkingHours()
        {
            List<WorkingHoursModel> whs = new List<WorkingHoursModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM WH2022";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    WorkingHoursModel wh = new WorkingHoursModel()
                    {
                        user_id = data_reader["user_id"] != DBNull.Value ? data_reader["user_id"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? data_reader["working_date"].ToString() : "",
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        start_time = data_reader["start_time"] != DBNull.Value ? data_reader["start_time"].ToString() : "",
                        stop_time = data_reader["stop_time"] != DBNull.Value ? data_reader["stop_time"].ToString() : "",
                        lunch = data_reader["lunch"] != DBNull.Value ? Convert.ToBoolean(data_reader["lunch"]) : true,
                        lunch_start = data_reader["lunch_start"] != DBNull.Value ? data_reader["lunch_start"].ToString() : "12:00",
                        lunch_stop = data_reader["lunch_stop"] != DBNull.Value ? data_reader["lunch_stop"].ToString() : "13:00",
                        break_evening = data_reader["break_evening"] != DBNull.Value ? Convert.ToBoolean(data_reader["break_evening"]) : true,
                        break_start = data_reader["break_start"] != DBNull.Value ? data_reader["break_start"].ToString() : "17:30",
                        break_stop = data_reader["break_stop"] != DBNull.Value ? data_reader["break_stop"].ToString() : "18:00",
                        note = data_reader["note"] != DBNull.Value ? data_reader["note"].ToString() : "",
                    };
                    whs.Add(wh);
                }
                data_reader.Close();
            }

            connection.Close();
            return whs;
        }

        public string AddWorkingHours(WorkingHoursModel wh)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();
            using(SqlCommand command = new SqlCommand("INSERT INTO WH2022(user_id, working_date, job_id, task_id, start_time, stop_time, lunch, lunch_start, lunch_stop, break_evening, break_start, break_stop, note) " +
                                                      "VALUES(@user_id, @working_date, @job_id, @task_id, @start_time, @stop_time, @lunch, @lunch_start, @lunch_stop, @break_evening, @break_start, @break_stop, @note)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@user_id", wh.user_id);
                command.Parameters.AddWithValue("@working_date", wh.working_date);
                command.Parameters.AddWithValue("@job_id", wh.job_id);
                command.Parameters.AddWithValue("@task_id", wh.task_id);
                command.Parameters.AddWithValue("@start_time", wh.start_time);
                command.Parameters.AddWithValue("@stop_time", wh.stop_time);
                command.Parameters.AddWithValue("@lunch", wh.lunch);
                command.Parameters.AddWithValue("@lunch_start", wh.lunch_start);
                command.Parameters.AddWithValue("@lunch_stop", wh.lunch_stop);
                command.Parameters.AddWithValue("@break_evening", wh.break_evening);
                command.Parameters.AddWithValue("@break_start", wh.break_start);
                command.Parameters.AddWithValue("@break_stop", wh.break_stop);
                command.Parameters.AddWithValue("@note", wh.note);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        } 
    }
}
