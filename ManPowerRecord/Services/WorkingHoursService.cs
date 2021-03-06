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
            string string_command = string.Format($@"
                SELECT 
                    [WH2022].ind, 
                    [WH2022].user_id, 
                    [Users].user_name, 
                    [WH2022].working_date, 
                    [WH2022].job_id,
                    [Jobs].job_name,
                    [WH2022].task_id,
                    [Tasks].task_name,
                    [WH2022].start_time,
                    [WH2022].stop_time,
                    [WH2022].lunch,
                    [WH2022].dinner,
                    [WH2022].note
                FROM WH2022 
                    LEFT JOIN [Users] ON WH2022.user_id = [Users].user_id 
                    LEFT JOIN [Jobs] ON WH2022.job_id = [Jobs].job_id 
                    LEFT JOIN [Tasks] ON WH2022.task_id = [Tasks].task_id");
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
                        user_name = data_reader["user_name"] != DBNull.Value ? data_reader["user_name"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? Convert.ToDateTime(data_reader["working_date"]) : default(DateTime),
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        task_name = data_reader["task_name"] != DBNull.Value ? data_reader["task_name"].ToString() : "",
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

        public List<WorkingHoursModel> GetWorkingHours(string user_id)
        {
            List<WorkingHoursModel> whs = new List<WorkingHoursModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                SELECT 
                    [WH2022].ind,
                    [WH2022].user_id,
                    [Users].user_name,
                    [WH2022].working_date,
                    [WH2022[.job_id,
                    [Jobs].job_name,
                    [WH2022].task_id,
                    [Tasks].task_name,
                    [WH2022].start_time,
                    [WH2022].stop_time,
                    [WH2022].lunch,
                    [WH2022].dinner,
                    [WH2022].note
                FROM WH2022
                    LEFT JOIN [Users] ON [WH2022].user_id = [Users].user_id
                    LEFT JOIN [Jobs] ON [WH2022].job_id = [Jobs].job_id
                    LEFT JOIN [Tasks] ON [WH2022].task_id = [Tasks].task_id
                WHERE [WH2022].user_id = '{user_id}'");
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
                        user_name = data_reader["user_name"] != DBNull.Value ? data_reader["user_name"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? Convert.ToDateTime(data_reader["working_date"]) : default(DateTime),
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        task_name = data_reader["task_name"] != DBNull.Value ? data_reader["task_name"].ToString() : "",
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

        public List<WorkingHoursModel> GetWorkingHours(string year, string month, string user)
        {
            List<WorkingHoursModel> whs = new List<WorkingHoursModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                SELECT
                    [WH2022].ind,
                    [WH2022].user_id,
                    [Users].user_name,
                    [WH2022].working_date,
                    [WH2022].job_id,
                    [Jobs].job_name,
                    [WH2022].task_id,
                    [Tasks].task_name,
                    [WH2022].start_time,
                    [WH2022].stop_time,
                    [WH2022].lunch,
                    [WH2022].dinner,
                    [WH2022].note
                FROM WH2022
                    LEFT JOIN [Users] ON [WH2022].user_id = [Users].user_id
                    LEFT JOIN [Jobs] ON [WH2022].job_id = [Jobs].job_id
                    LEFT JOIN [Tasks] ON [WH2022].task_id = [Tasks].task_id
                WHERE [WH2022].working_date like '{year}-{month}%' 
                AND [WH2022].user_id ='{user}'");
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
                        user_name = data_reader["user_name"] != DBNull.Value ? data_reader["user_name"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? Convert.ToDateTime(data_reader["working_date"]) : default(DateTime),
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        task_name = data_reader["task_name"] != DBNull.Value ? data_reader["task_name"].ToString() : "",
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

        public List<WorkingHoursModel> GetWorkingHours(string user, DateTime working_date)
        {
            List<WorkingHoursModel> whs = new List<WorkingHoursModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();
            string string_command = string.Format($@"
                SELECT
                    ind,
                    [WH2022].user_id,
                    [Users].user_name,
                    [WH2022].working_date,
                    [WH2022].job_id,
                    [Jobs].job_name,
                    [WH2022].task_id,
                    [Tasks].task_name,
                    [WH2022].start_time,
                    [WH2022].stop_time,
                    [WH2022].lunch,
                    [WH2022].dinner,
                    [WH2022].note
                FROM WH2022
                    LEFT JOIN [Users] ON [WH2022].user_id = [Users].user_id
                    LEFT JOIN [Jobs] ON [WH2022].job_id = [Jobs].job_id
                    LEFT JOIN [Tasks] ON [WH2022].task_id = [Tasks].task_id
                WHERE [WH2022].user_id = '{user}'
                AND [WH2022].working_date LIKE '{working_date.ToString("yyyy-MM-dd")}'");
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
                        user_name = data_reader["user_name"] != DBNull.Value ? data_reader["user_name"].ToString() : "",
                        working_date = data_reader["working_date"] != DBNull.Value ? Convert.ToDateTime(data_reader["working_date"]) : default(DateTime),
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        task_id = data_reader["task_id"] != DBNull.Value ? data_reader["task_id"].ToString() : "",
                        task_name = data_reader["task_name"] != DBNull.Value ? data_reader["task_name"].ToString() : "",
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
            string string_command = string.Format($@"
                INSERT INTO WH2022(
                    user_id, working_date, job_id, task_id, start_time, stop_time, lunch, dinner, note)
                VALUES (
                    @user_id, @working_date, @job_id, @task_id, @start_time, @stop_time, @lunch, @dinner, @note)");
            using (SqlCommand command = new SqlCommand(string_command, connection))
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
            string string_command = string.Format($@"
                UPDATE WH2022 
                SET
                    user_id = @user_id,
                    working_date = @working_date,
                    job_id = @job_id,
                    task_id = @task_id,
                    start_time = @start_time,
                    stop_time = @stop_time,
                    lunch = @lunch,
                    dinner = @dinner,
                    note = @note
                WHERE ind = @ind");
            using (SqlCommand command = new SqlCommand(string_command, connection))
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
