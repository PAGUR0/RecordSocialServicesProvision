using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace RecordSocialServicesProvision.mvvm.model
{
    public class MySQLModel
    {
        private string connectString = "server=localhost;user=root;database=socialservices;password=12345678;";
        private MySqlConnection connection;

        public MySQLModel()
        {
            connection = new MySqlConnection(connectString);
        }

        public DataTable GetApplicationData()
        {
            using (MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM application", connection))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public string GetPassword(string login)
        {
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT Password FROM user_worker WHERE login = @login", connection);
            cmd.Parameters.AddWithValue("@login", login);
            object resultCommand = cmd.ExecuteScalar();

            if (resultCommand == null)
            {
                return " ";
            }

            connection.Close();
            return resultCommand.ToString();
        }

        public string[] GetUserWorker(string login)
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT Name, Surname, Patronymic, Admin FROM user_worker WHERE login = @login", connection);
            resultCommand.Parameters.AddWithValue("@login", login);

            string[] result = new string[4];

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    result[0] = reader["Name"].ToString();
                    result[1] = reader["Surname"].ToString();
                    result[2] = reader["Patronymic"].ToString();
                    result[3] = reader["Admin"].ToString();
                }
            }

            connection.Close();
            return result;
        }

        public List<string>[] GetDocument()
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT name, mask, regex FROM document", connection);

            List<string>[] result = new List<string>[3] { new List<string>(), new List<string>(), new List<string>() };

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result[0].Add(reader["name"].ToString());
                    result[1].Add(reader["mask"].ToString());
                    result[2].Add(reader["regex"].ToString());
                }
            }

            connection.Close();
            return result;
        }

        public List<string> GetRegion()
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM geo_regions", connection);

            List<string> result = new List<string>();

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader["name"].ToString());
                }
            }

            connection.Close();
            return result;
        }

        public List<string> GetForm()
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT type FROM form", connection);

            List<string> result = new List<string>();

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader["type"].ToString());
                }
            }

            connection.Close();
            return result;
        }

        public List<string> GetLiving()
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT type FROM living", connection);

            List<string> result = new List<string>();

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader["type"].ToString());
                }
            }

            connection.Close();
            return result;
        }

        public List<string> GetSocialOrganizations()
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM social_organizations", connection);

            List<string> result = new List<string>();

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader["name"].ToString());
                }
            }

            connection.Close();
            return result;
        }

        public List<string> GetOrganizations()
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM organizations", connection);

            List<string> result = new List<string>();

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    result.Add(reader["name"].ToString());
                }
            }

            connection.Close();
            return result;
        }

        public string[] GetUser(string snils)
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT * FROM user WHERE snils=@snils", connection);
            resultCommand.Parameters.AddWithValue("@snils", snils);

            string[] result = new string[14];

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    result[0] = reader["Name"].ToString();
                    result[1] = reader["Surname"].ToString();
                    result[2] = reader["Patronymic"].ToString();
                    result[3] = DateTime.Parse(reader["brithdates"].ToString()).ToString("dd.MM.yyyy");
                    result[4] = reader["document_type"].ToString();
                    result[5] = reader["document_number"].ToString();
                    result[6] = reader["region"].ToString();
                    result[7] = reader["region_small"].ToString();
                    result[8] = reader["city"].ToString();
                    result[9] = reader["street"].ToString();
                    result[10] = reader["home"].ToString();
                    result[11] = reader["apartment"].ToString();
                    result[12] = reader["phone_number"].ToString();
                    result[13] = reader["email"].ToString();
                }
            }

            connection.Close();
            return result;
        }

        public bool GetUserSnils(string snils)
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM user WHERE snils=@snils", connection);
            resultCommand.Parameters.AddWithValue("@snils", snils);

            bool result = resultCommand.ExecuteScalar() != null;

            connection.Close();
            return result;
        }

        public bool GetAdditionalUser(string numberDocument)
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT home FROM user WHERE document_number=@numberDocument", connection);
            resultCommand.Parameters.AddWithValue("@numberDocument", numberDocument);

            bool result = resultCommand.ExecuteScalar() != null;

            connection.Close();
            return result;
        }

        public string[] GetUserAdd(string userId)
        {
            connection.Open();
            MySqlCommand resultCommand = new MySqlCommand("SELECT * FROM user_add WHERE document_number=@userId", connection);
            resultCommand.Parameters.AddWithValue("@userId", userId);

            string[] resultResult = new string[14];

            using (MySqlDataReader reader = resultCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    resultResult[0] = reader["name"].ToString();
                    resultResult[1] = reader["document_type"].ToString();
                    resultResult[2] = reader["document_number"].ToString();
                    resultResult[3] = reader["region"].ToString();
                    resultResult[4] = reader["region_small"].ToString();
                    resultResult[5] = reader["city"].ToString();
                    resultResult[6] = reader["street"].ToString();
                    resultResult[7] = reader["home"].ToString();
                    resultResult[8] = reader["apartment"].ToString();
                }
            }

            connection.Close();
            return resultResult;
        }

        public void SetApplication(string user_snils, int true_user_add, string user_add_id, int social_organizations_idsocial_organizations, int organization_id, int form_id, string reason, int domestic, int medical, int psychological, int pedagogical, int labour, int legal, int communication, int urgent, string family, int living_id, int income, int consent, DateTime dateCreate, string user_login)
        {
            connection.Open();
            MySqlCommand addCommand = new MySqlCommand("INSERT INTO application (user_snils, true_user_add, user_add_id, social_organizations_idsocial_organizations, organization_id, form_id, reason, domestic, medical, psychological, pedagogical, labour, legal, communication, urgent, family, living_id, income, consent, date, user_login) " +
                "VALUES (@user_snils, @true_user_add, @user_add_id, @social_organizations_idsocial_organizations, @organization_id, @form_id, @reason, @domestic, @medical, @psychological, @pedagogical, @labour, @legal, @communication, @urgent, @family, @living_id, @income, @consent, @date, @user_login)", connection);

            addCommand.Parameters.AddWithValue("@user_snils", user_snils);
            addCommand.Parameters.AddWithValue("@true_user_add", true_user_add);
            addCommand.Parameters.AddWithValue("@user_add_id", user_add_id);
            addCommand.Parameters.AddWithValue("@social_organizations_idsocial_organizations", social_organizations_idsocial_organizations);
            addCommand.Parameters.AddWithValue("@organization_id", organization_id);
            addCommand.Parameters.AddWithValue("@form_id", form_id);
            addCommand.Parameters.AddWithValue("@reason", reason);
            addCommand.Parameters.AddWithValue("@domestic", domestic);
            addCommand.Parameters.AddWithValue("@medical", medical);
            addCommand.Parameters.AddWithValue("@psychological", psychological);
            addCommand.Parameters.AddWithValue("@pedagogical", pedagogical);
            addCommand.Parameters.AddWithValue("@labour", labour);
            addCommand.Parameters.AddWithValue("@legal", legal);
            addCommand.Parameters.AddWithValue("@communication", communication);
            addCommand.Parameters.AddWithValue("@urgent", urgent);
            addCommand.Parameters.AddWithValue("@family", family);
            addCommand.Parameters.AddWithValue("@living_id", living_id);
            addCommand.Parameters.AddWithValue("@income", income);
            addCommand.Parameters.AddWithValue("@consent", consent);
            addCommand.Parameters.AddWithValue("@date", dateCreate);
            addCommand.Parameters.AddWithValue("@user_login", user_login);

            addCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void SetUser(string snils, string name, string surname, string patronymic, string brithdates, int document_type, string document_number, int region, string region_small, string city, string street, int home, int apartment, string phone_number, string email)
        {
            connection.Open();
            MySqlCommand addCommand = new MySqlCommand("INSERT INTO user (snils, name, surname, patronymic, brithdates, document_type, document_number, region, region_small, city, street, home, apartment, phone_number, email) " +
                "VALUES (@snils, @name, @surname, @patronymic, @brithdates, @document_type, @document_number, @region, @region_small, @city, @street, @home, @apartment, @phone_number, @email)", connection);

            addCommand.Parameters.AddWithValue("@snils", snils);
            addCommand.Parameters.AddWithValue("@name", name);
            addCommand.Parameters.AddWithValue("@surname", surname);
            addCommand.Parameters.AddWithValue("@patronymic", patronymic);
            addCommand.Parameters.AddWithValue("@brithdates", brithdates);
            addCommand.Parameters.AddWithValue("@document_type", document_type);
            addCommand.Parameters.AddWithValue("@document_number", document_number);
            addCommand.Parameters.AddWithValue("@region", region);
            addCommand.Parameters.AddWithValue("@region_small", region_small);
            addCommand.Parameters.AddWithValue("@city", city);
            addCommand.Parameters.AddWithValue("@street", street);
            addCommand.Parameters.AddWithValue("@home", home);
            addCommand.Parameters.AddWithValue("@apartment", apartment);
            addCommand.Parameters.AddWithValue("@phone_number", phone_number);
            addCommand.Parameters.AddWithValue("@email", email);

            addCommand.ExecuteNonQuery();
            connection.Close();
        }

        public void SetAdditionalUser(string document_number, int document_type, string name, int region, string region_small, string city, string street, int home, int apartment)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand addCommand = new MySqlCommand("INSERT INTO user_add (document_number, document_type, name, region, region_small, city, street, home, apartment) " +
                    "VALUES (@document_number, @document_type, @name, @region, @region_small, @city, @street, @home, @apartment)", connection))
                {
                    addCommand.Parameters.AddWithValue("@document_number", document_number);
                    addCommand.Parameters.AddWithValue("@document_type", document_type);
                    addCommand.Parameters.AddWithValue("@name", name);
                    addCommand.Parameters.AddWithValue("@region", region);
                    addCommand.Parameters.AddWithValue("@region_small", region_small);
                    addCommand.Parameters.AddWithValue("@city", city);
                    addCommand.Parameters.AddWithValue("@street", street);
                    addCommand.Parameters.AddWithValue("@home", home);
                    addCommand.Parameters.AddWithValue("@apartment", apartment);

                    addCommand.ExecuteNonQuery();
                }
            }
        }

        public void SetWhoOrganizations(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand addCommand = new MySqlCommand("INSERT INTO social_organizations (name) " +
                    "VALUES (@name)", connection))
                {
                    addCommand.Parameters.AddWithValue("@name", name);
                    addCommand.ExecuteNonQuery();
                }
            }
        }

        public void SetOrganizations(string name)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand addCommand = new MySqlCommand("INSERT INTO organizations (name) " +
                    "VALUES (@name)", connection))
                {
                    addCommand.Parameters.AddWithValue("@name", name);
                    addCommand.ExecuteNonQuery();
                }
            }
        }

        public void SetLiving(string type)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                using (MySqlCommand addCommand = new MySqlCommand("INSERT INTO living (type) " +
                    "VALUES (@type)", connection))
                {
                    addCommand.Parameters.AddWithValue("@type", type);
                    addCommand.ExecuteNonQuery();
                }
            }
        }

        public string ExecuteSqlQuery(string query)
        {
            using (MySqlConnection connection = new MySqlConnection(connectString))
            {
                connection.Open();
                try
                {
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                string result = "";
                                while (reader.Read())
                                {
                                    result += reader.GetString(0) + "\n";
                                }
                                return result;
                            }
                            else
                            {
                                return "Нет данных для вывода.";
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    return "Ошибка: " + ex.Message;
                }
            }
        }
    }
}