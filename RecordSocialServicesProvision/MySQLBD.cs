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
            string[] userWorkerResult = new string[4];

            if (userWorkerResultCommand == null)
            {
                return userWorkerResult;
            }
            MySqlDataReader reader = userWorkerResultCommand.ExecuteReader();

            while (reader.Read())
            {
                userWorkerResult[0] = reader["Name"].ToString();
                userWorkerResult[1] = reader["Surname"].ToString();
                userWorkerResult[2] = reader["Patronymic"].ToString();
                userWorkerResult[3] = reader["Admin"].ToString();
            }
            reader.Close();
            return userWorkerResult;
        }

        public List<string>[] getDocument()
        {
            MySqlCommand documentResultCommand = new MySqlCommand("SELECT name, mask FROM document", connect);
            List<string>[] document = new List<string>[2] { new List<string>(), new List<string>() };
            if (documentResultCommand == null)
            {
                return document;
            }
            MySqlDataReader reader = documentResultCommand.ExecuteReader();
            
            while (reader.Read())
            {
                document[0].Add(reader["name"].ToString());
                document[1].Add(reader["mask"].ToString());
            }
            reader.Close();
            return document;
        }

        public List<string> getRegion()
        {
            MySqlCommand regionResultCommand = new MySqlCommand("SELECT name FROM geo_regions", connect);
            List<string> region = new List<string>();
            if (regionResultCommand == null)
            {
                return region;
            }
            MySqlDataReader reader = regionResultCommand.ExecuteReader();

            while (reader.Read())
            {
                region.Add(reader["name"].ToString());
            }
            reader.Close();
            return region;
        }
    }
}
