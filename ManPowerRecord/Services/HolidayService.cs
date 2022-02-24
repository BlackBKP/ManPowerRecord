using ManPowerRecord.Interfaces;
using ManPowerRecord.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class HolidayService : IHoliday
    {
        IConnectDB Database;

        public HolidayService()
        {
            this.Database = new ConnectDB();
        }

        public List<HolidayModel> GetHolidays()
        {
            List<HolidayModel> holidays = new List<HolidayModel>();
            SqlConnection connection = Database.Connect();
            connection.Open();

            string string_command = "SELECT * FROM Holidays";
            SqlCommand command = new SqlCommand(string_command, connection);
            SqlDataReader data_reader = command.ExecuteReader();

            if (data_reader.HasRows)
            {
                while (data_reader.Read())
                {
                    HolidayModel holiday = new HolidayModel()
                    {
                        no = data_reader["no"] != DBNull.Value ? Convert.ToInt32(data_reader["no"]) : default(Int32),
                        date = data_reader["date"] != DBNull.Value ? Convert.ToDateTime(data_reader["date"]) : default(DateTime),
                        name = data_reader["name"] != DBNull.Value ? data_reader["name"].ToString() : ""
                    };
                    holidays.Add(holiday);
                }
                data_reader.Close();
            }

            connection.Close();
            return holidays.OrderBy(o => o.date).ToList();
        }
    }
}
