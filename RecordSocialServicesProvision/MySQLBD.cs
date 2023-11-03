using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordSocialServicesProvision
{
    class MySQLBD
    {
        private static string connectString = "server=localhost;user=root;database=socialservices;password=12345678;";
        private static MySqlConnection connect { get; set; } = new MySqlConnection(connectString);
        private static MySQLBD? instance;

        public static MySQLBD getInstanse()
        {
            if (instance == null)
            {
                instance = new MySQLBD();
                connect.Open();
            }
            return instance;
        }

        public string getPassword(string login)
        {
            object passwordResult = new MySqlCommand("SELECT Password FROM user_worker WHERE login = '" + login + "'", connect).ExecuteScalar();
            if (passwordResult == null)
            {
                return " ";
            }
            return passwordResult.ToString();
        }

        public string[] getUserWorker(string login)
        {
            MySqlCommand userWorkerResultCommand = new MySqlCommand("SELECT Name, Surname, Patronymic, Admin FROM user_worker WHERE login = '" + login + "'", connect);

            if (userWorkerResultCommand == null)
            {
                return new string[4];
            }
            MySqlDataReader reader = userWorkerResultCommand.ExecuteReader();

            string[] userWorkerResult = new string[4];
            while (reader.Read())
            {
                userWorkerResult[0] = reader["Name"].ToString();
                userWorkerResult[1] = reader["Surname"].ToString();
                userWorkerResult[2] = reader["Patronymic"].ToString();
                userWorkerResult[3] = reader["Admin"].ToString();
            }
            return userWorkerResult;
        }
    }
}
