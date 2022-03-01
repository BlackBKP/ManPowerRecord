using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class JobService : IJob
    {
        IConnectDB Database;

        public JobService()
        {
            this.Database = new ConnectDB();
        }

        public List<JobModel> GetAllJobs()
        {
            List<JobModel> jobs = new List<JobModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM Jobs";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    JobModel job = new JobModel()
                    {
                        job_id = data_reader["job_id"] != DBNull.Value ? data_reader["job_id"].ToString() : "",
                        job_name = data_reader["job_name"] != DBNull.Value ? data_reader["job_name"].ToString() : "",
                        sale_department = data_reader["sale_department"] != DBNull.Value ? data_reader["sale_department"].ToString() : "",
                        sale = data_reader["sale"] != DBNull.Value ? data_reader["sale"].ToString() : "",
                        cost = data_reader["cost"] != DBNull.Value ? Convert.ToInt32(data_reader["cost"]) : 0,
                        md_rate = data_reader["md_rate"] != DBNull.Value ? Convert.ToDouble(data_reader["md_rate"]) : 1,
                        pd_rate = data_reader["pd_rate"] != DBNull.Value ? Convert.ToDouble(data_reader["pd_rate"]) : 1,
                        factor = 0,
                        manpower = 0,
                        cost_per_manpower = 0,
                        ot_manpower = 0,
                        status = data_reader["status"] != DBNull.Value ? data_reader["status"].ToString() : "",
                    };
                    job.factor = job.md_rate + job.pd_rate;
                    jobs.Add(job);
                }
                data_reader.Close();
            }
            connection.Close();
            return jobs;
        }

        public string CreateJob(JobModel job)
        {
            this.Database = new ConnectDB();
            SqlConnection connection = Database.Connect();
            connection.Open();
            using(SqlCommand command = new SqlCommand("INSERT INTO Jobs(job_id, job_name, sale_department, sale, cost, md_rate, pd_rate, status) " +
                                                      "VALUES(@job_id, @job_name, @sale_department, @sale, @cost, @md_rate, @pd_rate, @status)",connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@job_id", job.job_id.Replace("-", String.Empty));
                command.Parameters.AddWithValue("@job_name", job.job_name);
                command.Parameters.AddWithValue("@sale_department", job.sale_department);
                command.Parameters.AddWithValue("@sale", job.sale);
                command.Parameters.AddWithValue("@cost", job.cost);
                command.Parameters.AddWithValue("@md_rate", job.md_rate);
                command.Parameters.AddWithValue("@pd_rate", job.pd_rate);
                command.Parameters.AddWithValue("@status", "");
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }

        public string UpdateJob(JobModel job)
        {
            this.Database = new ConnectDB();
            SqlConnection connection = Database.Connect();
            connection.Open();
            using(SqlCommand command = new SqlCommand("UPDATE Jobs SET " +
                "job_name = @job_name, " +
                "sale_department = @sale_department, " +
                "sale = @sale, " +
                "cost = @cost, " +
                "md_rate = @md_rate, " +
                "pd_rate = @pd_rate, " +
                "status = @status WHERE job_id = @job_id", connection))
            {
                command.CommandType = System.Data.CommandType.Text;
                command.Connection = connection;
                command.Parameters.AddWithValue("@job_id", job.job_id.Replace("-", String.Empty));
                command.Parameters.AddWithValue("@job_name", job.job_name);
                command.Parameters.AddWithValue("@sale_department", job.sale_department);
                command.Parameters.AddWithValue("@sale", job.sale);
                command.Parameters.AddWithValue("@cost", job.cost);
                command.Parameters.AddWithValue("@md_rate", job.md_rate);
                command.Parameters.AddWithValue("@pd_rate", job.pd_rate);
                command.Parameters.AddWithValue("@status", job.status);
                command.ExecuteNonQuery();
            }
            connection.Close();
            return "Success";
        }
    }
}
