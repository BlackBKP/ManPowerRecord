using ManPowerRecord.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ManPowerRecord.Services
{
    public class ConnectDB : IConnectDB
    {
        public SqlConnection Connect()
        {
            string connection_string = @"Data Source=localhost\SQLExpress;Initial Catalog=MPR;User ID=sa;Password=p@ssw0rd";
            SqlConnection connection = new SqlConnection(connection_string);
            return connection;
        }
    }
}
