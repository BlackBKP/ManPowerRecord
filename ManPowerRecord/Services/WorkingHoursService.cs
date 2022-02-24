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
                        job_1 = data_reader["job_1"] != DBNull.Value ? data_reader["job_1"].ToString() : "",
                        task_1 = data_reader["task_1"] != DBNull.Value ? data_reader["task_1"].ToString() : "",
                        start_time1 = data_reader["start_time1"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time1"].ToString()) : default(TimeSpan),
                        stop_time1 = data_reader["stop_time1"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time1"].ToString()) : default(TimeSpan),
                        job_2 = data_reader["job_2"] != DBNull.Value ? data_reader["job_2"].ToString() : "",
                        task_2 = data_reader["task_2"] != DBNull.Value ? data_reader["task_2"].ToString() : "",
                        start_time2 = data_reader["start_time2"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time2"].ToString()) : default(TimeSpan),
                        stop_time2 = data_reader["stop_time2"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time2"].ToString()) : default(TimeSpan),
                        job_3 = data_reader["job_3"] != DBNull.Value ? data_reader["job_3"].ToString() : "",
                        task_3 = data_reader["task_3"] != DBNull.Value ? data_reader["task_3"].ToString() : "",
                        start_time3 = data_reader["start_time3"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time3"].ToString()) : default(TimeSpan),
                        stop_time3 = data_reader["stop_time3"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time3"].ToString()) : default(TimeSpan),
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
                        job_1 = data_reader["job_1"] != DBNull.Value ? data_reader["job_1"].ToString() : "",
                        task_1 = data_reader["task_1"] != DBNull.Value ? data_reader["task_1"].ToString() : "",
                        start_time1 = data_reader["start_time1"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time1"].ToString()) : default(TimeSpan),
                        stop_time1 = data_reader["stop_time1"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time1"].ToString()) : default(TimeSpan),
                        job_2 = data_reader["job_2"] != DBNull.Value ? data_reader["job_2"].ToString() : "",
                        task_2 = data_reader["task_2"] != DBNull.Value ? data_reader["task_2"].ToString() : "",
                        start_time2 = data_reader["start_time2"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time2"].ToString()) : default(TimeSpan),
                        stop_time2 = data_reader["stop_time2"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time2"].ToString()) : default(TimeSpan),
                        job_3 = data_reader["job_3"] != DBNull.Value ? data_reader["job_3"].ToString() : "",
                        task_3 = data_reader["task_3"] != DBNull.Value ? data_reader["task_3"].ToString() : "",
                        start_time3 = data_reader["start_time3"] != DBNull.Value ? TimeSpan.Parse(data_reader["start_time3"].ToString()) : default(TimeSpan),
                        stop_time3 = data_reader["stop_time3"] != DBNull.Value ? TimeSpan.Parse(data_reader["stop_time3"].ToString()) : default(TimeSpan),
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
            using(SqlCommand command = new SqlCommand("INSERT INTO WH2022(" +
                                                        "user_id, working_date, " +
                                                        "job_1, task_1, start_time1, stop_time1, " +
                                                        "job_2, task_2, start_time2, stop_time2, " +
                                                        "job_3, task_3, start_time3, stop_time3, " +
                                                        "lunch, dinner, note) " +
                                                      "VALUES (" +
                                                        "@user_id, @working_date, " +
                                                        "@job_1, @task_1, @start_time1, @stop_time1, " +
                                                        "@job_2, @task_2, @start_time2, @stop_time2, " +
                                                        "@job_3, @task_3, @start_time3, @stop_time3, " +
                                                        "@lunch, @dinner, @note)", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@user_id", wh.user_id);
                command.Parameters.AddWithValue("@working_date", wh.working_date);
                command.Parameters.AddWithValue("@job_1", wh.job_1);
                command.Parameters.AddWithValue("@task_1", wh.task_1);
                command.Parameters.AddWithValue("@start_time1", wh.start_time1);
                command.Parameters.AddWithValue("@stop_time1", wh.stop_time1);
                command.Parameters.AddWithValue("@job_2", wh.job_2);
                command.Parameters.AddWithValue("@task_2", wh.task_2);
                command.Parameters.AddWithValue("@start_time2", wh.start_time2);
                command.Parameters.AddWithValue("@stop_time2", wh.stop_time2);
                command.Parameters.AddWithValue("@job_3", wh.job_3);
                command.Parameters.AddWithValue("@task_3", wh.task_3);
                command.Parameters.AddWithValue("@start_time3", wh.start_time3);
                command.Parameters.AddWithValue("@stop_time3", wh.stop_time3);
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
                                                       "job_1 = @job_1, " +
                                                       "task_1 = @task_1, " +
                                                       "start_time1 = @start_time1, " +
                                                       "stop_time1 = @stop_time1, " +
                                                       "job_2 = @job_2, " +
                                                       "task_2 = @task_2, " +
                                                       "start_time2 = @start_time2, " +
                                                       "stop_time2 = @stop_time2, " +
                                                       "job_3 = @job_3, " +
                                                       "task_3 = @task_3, " +
                                                       "start_time3 = @start_time3, " +
                                                       "stop_time3 = @stop_time3, " +
                                                       "lunch = @lunch, " +
                                                       "dinner = @dinner, " +
                                                       "note = @note " +
                                                       "WHERE ind = @ind ", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@user_id", wh.user_id);
                command.Parameters.AddWithValue("@working_date", wh.working_date);
                command.Parameters.AddWithValue("@job_1", wh.job_1);
                command.Parameters.AddWithValue("@task_1", wh.task_1);
                command.Parameters.AddWithValue("@start_time1", wh.start_time1);
                command.Parameters.AddWithValue("@stop_time1", wh.stop_time1);
                command.Parameters.AddWithValue("@job_2", wh.job_2);
                command.Parameters.AddWithValue("@task_2", wh.task_2);
                command.Parameters.AddWithValue("@start_time2", wh.start_time2);
                command.Parameters.AddWithValue("@stop_time2", wh.stop_time2);
                command.Parameters.AddWithValue("@job_3", wh.job_3);
                command.Parameters.AddWithValue("@task_3", wh.task_3);
                command.Parameters.AddWithValue("@start_time3", wh.start_time3);
                command.Parameters.AddWithValue("@stop_time3", wh.stop_time3);
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
