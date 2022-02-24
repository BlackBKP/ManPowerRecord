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
                        index = data_reader["ind"] != DBNull.Value ? Convert.ToInt32(data_reader["ind"]) : default(Int32),
                        user_id = data_reader["user_id"] != DBNull.Value ? data_reader["user_id"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? Convert.ToDateTime(data_reader["working_date"]) : default(DateTime),
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        start_time = data_reader["start_time"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time"].ToString()) : default(TimeSpan),
                        stop_time = data_reader["stop_time"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time"].ToString()) : default(TimeSpan),
                        lunch = data_reader["lunch"] != DBNull.Value ? Convert.ToBoolean(data_reader["lunch"]) : true,
                        dinner = data_reader["dinner"] != DBNull.Value ? Convert.ToBoolean(data_reader["dinner"]) : true,
                        note = data_reader["note"] != DBNull.Value ? data_reader["note"].ToString() : "",
                    };
                    whs.Add(wh);
                }
                data_reader.Close();
            }

            connection.Close();
            return whs;
        }

        public List<WorkingHoursModel> GetWorkingHours(string year, string month)
        {
            List<WorkingHoursModel> whs = new List<WorkingHoursModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM WH2022 WHERE working_date like '" + year + "-" + month + "%'";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    WorkingHoursModel wh = new WorkingHoursModel()
                    {
                        index = data_reader["ind"] != DBNull.Value ? Convert.ToInt32(data_reader["ind"]) : default(Int32),
                        user_id = data_reader["user_id"] != DBNull.Value ? data_reader["user_id"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? Convert.ToDateTime(data_reader["working_date"]) : default(DateTime),
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        start_time = data_reader["start_time"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time"].ToString()) : default(TimeSpan),
                        stop_time = data_reader["stop_time"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time"].ToString()) : default(TimeSpan),
                        lunch = data_reader["lunch"] != DBNull.Value ? Convert.ToBoolean(data_reader["lunch"]) : true,
                        dinner = data_reader["dinner"] != DBNull.Value ? Convert.ToBoolean(data_reader["dinner"]) : true,
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
            using(SqlCommand command = new SqlCommand("INSERT INTO WH2022(user_id, working_date, job_id, task_id, start_time, stop_time, lunch, dinner, note) " +
                                                      "VALUES (@user_id, @working_date, @job_id, @task_id, @start_time, @stop_time, @lunch, @dinner, @note)", connection))
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
                command.Parameters.AddWithValue("@dinner", wh.dinner);
                command.Parameters.AddWithValue("@note", wh.note);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }

        public string UpdateWorkingHours(WorkingHoursModel wh)
        {
            SqlConnection connection = Database.Connect();
            connection.Open();

            using (SqlCommand command = new SqlCommand("UPDATE WH2022 SET " +
                                                       "user_id = @user_id, " +
                                                       "working_date = @working_date, " +
                                                       "job_id = @job_id, " +
                                                       "task_id = @task_id, " +
                                                       "start_time = @start_time, " +
                                                       "stop_time = @stop_time, " +
                                                       "lunch = @lunch, " +
                                                       "dinner = @dinner, " +
                                                       "note = @note " +
                                                       "WHERE ind = @ind ", connection))
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
                command.Parameters.AddWithValue("@dinner", wh.dinner);
                command.Parameters.AddWithValue("@note", wh.note);
                command.Parameters.AddWithValue("@ind", wh.index);
                command.ExecuteNonQuery();
            }

            connection.Close();
            return "Success";
        }
    }
}
