using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace HeirRL.Data
{
    public class Service
    {
        public string GetApplicationName()
        {
            var result = "";
            using (var connection = (SQLiteConnection)SQLiteConnector.Factory.CreateConnection())
            {
                connection.ConnectionString = SQLiteConnector.ConnectionString;
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"SELECT * FROM service WHERE key = 'appname' LIMIT 1";
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    reader.Read();
                    result = reader["value"].ToString();
                }
                connection.Close();
            }

            return result;
        }

        public string GetApplicationVersion()
        {
            var result = "";
            using (var connection = (SQLiteConnection)SQLiteConnector.Factory.CreateConnection())
            {
                connection.ConnectionString = SQLiteConnector.ConnectionString;
                connection.Open();

                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = @"SELECT * FROM service WHERE key = 'version' LIMIT 1";
                    command.CommandType = CommandType.Text;
                    var reader = command.ExecuteReader();
                    reader.Read();
                    result = reader["value"].ToString();
                }
                connection.Close();
            }

            return result;
        }
    }
}
