using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace RecordSocialServicesProvision
{
    class MySQLBD
    {
        private static MySqlConnection? connect;
        private static MySQLBD? instance;

        public static MySQLBD getInstanse()
        {
            if (instance == null)
            {
                connCreate();
                instance = new MySQLBD();
            }
            return instance;
        }

        public static void connCreate() 
        {
            try
            {
                string filePath = "db.txt";
                connect = new MySqlConnection(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public DataTable getDataAdapter() 
        {
            DataTable dataTable = new DataTable();
            try
            {
                using (connect)
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(new MySqlCommand(@"
                    SELECT 
                        a.idapplication AS 'id',
                        a.user_snils AS 'СНИЛС',
                        u.name AS 'имя',
                        u.surname AS 'фамилия',
                        u.patronymic AS 'отчество',
                        DATE_FORMAT(u.brithdates, '%Y-%m-%d') AS 'дата рождения',
                        d.name  AS 'документ',
                        u.document_number AS 'номер документа',
                        r.name AS 'регион',
                        u.region_small AS 'район',
                        u.city AS 'город',
                        u.street AS 'улица',
                        u.home AS 'дом',
                        u.apartment AS 'квартира',
                        u.phone_number AS 'телефон',
                        u.email AS 'почта',
                        CASE WHEN a.true_user_add = 1 THEN 'да' ELSE 'нет' END AS 'второй заявитель',
                        ua.name AS 'ФИО и название',
                        ua.document_type  AS 'документ', 
                        ua.document_number AS 'номер документа', 
                        ua.region AS 'регион2',
                        ua.region_small AS 'район2',
                        ua.city AS 'город2',
                        ua.street AS 'улица2', 
                        ua.home AS 'дом2', 
                        ua.apartment AS 'квартира2', 
                        so.name AS 'кому', 
                        o.name AS 'окажет услугу', 
                        f.type AS 'форма', a.reason AS 'причина', 
                        CASE WHEN a.domestic = 1 THEN 'да' ELSE 'нет' END AS 'социальные-бытовое', 
                        CASE WHEN a.medical = 1 THEN 'да' ELSE 'нет' END AS 'социальные-медецинские', 
                        CASE WHEN a.psychological = 1 THEN 'да' ELSE 'нет' END AS 'социальные-психологические', 
                        CASE WHEN a.pedagogical = 1 THEN 'да' ELSE 'нет' END AS 'социальные-педагогические', 
                        CASE WHEN a.labour = 1 THEN 'да' ELSE 'нет' END AS 'социальные-трудовые', 
                        CASE WHEN a.legal = 1 THEN 'да' ELSE 'нет' END AS 'социальные-правовые', 
                        CASE WHEN a.urgent = 1 THEN 'да' ELSE 'нет' END AS 'срочные социальные услуги', 
                        CASE WHEN a.communication = 1 THEN 'да' ELSE 'нет' END AS 'повышение коммуникации', 
                        a.family AS 'состав семьи', l.type AS 'условия проживания', 
                        a.income AS 'доход', CASE WHEN a.consent = 1 THEN 'да' ELSE 'нет' END AS 'согласие', 
                        DATE_FORMAT(a.date, '%Y-%m-%d') AS 'дата подачи', 
                        uw.Name AS 'работник', 
                        CASE WHEN a.proof_poverty = 1 THEN 'да' ELSE 'нет' END  AS 'одобрено' 
                    FROM 
                        application a 
                    LEFT JOIN 
                        user u ON a.user_snils = u.snils 
                    LEFT JOIN 
                        document d ON u.document_type = d.id 
                    LEFT JOIN 
                        geo_regions r ON u.region = r.id 
                    LEFT JOIN 
                        user_add ua ON a.user_add_id = ua.document_number 
                    LEFT JOIN 
                        social_organizations so ON a.social_organizations_idsocial_organizations = so.idsocial_organizations 
                    LEFT JOIN 
                        organizations o ON a.organization_id = o.id 
                    LEFT JOIN 
                        form f ON a.form_id = f.id 
                    LEFT JOIN 
                        living l ON a.living_id = l.id 
                    LEFT JOIN 
                        user_worker uw ON a.user_login = uw.Login;", connect));
                    connect.Open();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (MySqlException ex)
            {
                return dataTable;
                Console.WriteLine(ex.Message); ;
            }
        }

        public string[] getDataApplication()
        {
            try{
                using (connect)
                {
                    MySqlCommand resultCommand = new MySqlCommand(@"
                    SELECT 
                        a.idapplication AS 'id', 
                        u.name AS 'имя', 
                        u.surname AS 'фамилия', 
                        u.patronymic AS 'отчество', 
                        DATE_FORMAT(u.brithdates, '%Y-%m-%d') AS 'дата рождения', 
                        d.name  AS 'документ', 
                        u.document_number AS 'номер документа', 
                        r.name AS 'регион', 
                        u.region_small AS 'район', 
                        u.city AS 'город', 
                        u.street AS 'улица', 
                        u.home AS 'дом', 
                        u.apartment AS 'квартира', 
                        u.phone_number AS 'телефон', 
                        u.email AS 'почта', 
                        CASE WHEN a.true_user_add = 1 THEN 'true' ELSE 'false' END AS 'второй заявитель', 
                        ua.name AS 'ФИО и название',
                        ua.document_type  AS 'документ', 
                        ua.document_number AS 'номер документа', 
                        ua.region AS 'регион2', 
                        ua.region_small AS 'район2', 
                        ua.city AS 'город2', 
                        ua.street AS 'улица2', 
                        ua.home AS 'дом2', 
                        ua.apartment AS 'квартира2', 
                        so.name AS 'кому', 
                        o.name AS 'окажет услугу', 
                        f.type AS 'форма', 
                        a.reason AS 'причина', 
                        CASE WHEN a.domestic = 1 THEN 'true' ELSE 'false' END AS 'социальные-бытовое', 
                        CASE WHEN a.medical = 1 THEN 'true' ELSE 'false' END AS 'социальные-медецинские', 
                        CASE WHEN a.psychological = 1 THEN 'true' ELSE 'false' END AS 'социальные-психологические', 
                        CASE WHEN a.pedagogical = 1 THEN 'true' ELSE 'false' END AS 'социальные-педагогические', 
                        CASE WHEN a.labour = 1 THEN 'true' ELSE 'false' END AS 'социальные-трудовые', 
                        CASE WHEN a.legal = 1 THEN 'true' ELSE 'false' END AS 'социальные-правовые', 
                        CASE WHEN a.urgent = 1 THEN 'true' ELSE 'false' END AS 'срочные социальные услуги', 
                        CASE WHEN a.communication = 1 THEN 'true' ELSE 'false' END AS 'повышение коммуникации', 
                        a.family AS 'состав семьи', 
                        l.type AS 'условия проживания', 
                        a.income AS 'доход', 
                        CASE WHEN a.consent = 1 THEN 'true' ELSE 'false' END AS 'согласие на обработку данных', 
                        DATE_FORMAT(a.date, '%Y-%m-%d') AS 'дата подачи', 
                        uw.login AS 'работник',
                        a.user_snils 
                    FROM 
                        application a
                    LEFT JOIN 
                        user u ON a.user_snils = u.snils
                    LEFT JOIN 
                        document d ON u.document_type = d.id
                    LEFT JOIN 
                        geo_regions r ON u.region = r.id
                    LEFT JOIN 
                        user_add ua ON a.user_add_id = ua.document_number
                    LEFT JOIN 
                        social_organizations so ON a.social_organizations_idsocial_organizations = so.idsocial_organizations
                    LEFT JOIN 
                        organizations o ON a.organization_id = o.id
                    LEFT JOIN 
                        form f ON a.form_id = f.id
                    LEFT JOIN 
                        living l ON a.living_id = l.id
                    LEFT JOIN 
                        user_worker uw ON a.user_login = uw.Login
                    WHERE 
                        a.proof_poverty IS NULL
                    ORDER BY 
                        a.proof_poverty ASC 
                    LIMIT 1;
                ", connect);
                    connect.Open();
                    string[] result = new string[44];

                    if (resultCommand == null)
                    {
                        return result;
                    }
                    using (MySqlDataReader reader = resultCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < 44; i++)
                            {
                                result[i] = reader[i].ToString();
                            }
                        }
                        return result;
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new string[44];
            }
        }


        public string getPassword(string login)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    object resultCommand = new MySqlCommand("SELECT Password FROM user_worker WHERE login = '" + login + "'", connect).ExecuteScalar();
                    if (resultCommand == null)
                    {
                        return " ";
                    }
                    return resultCommand.ToString();
                }
            } catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return " ";
            }
        }

        public string[] getUserWorker(string login)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT Name, Surname, Patronymic, Admin FROM user_worker WHERE login = '" + login + "'", connect);
                    string[] result = new string[4];

                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            result[i] = reader[i].ToString();
                        }
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new string[0];
            }
        }

        public List<string>[] getDocument()
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT name, mask, regex FROM document", connect);
                    List<string>[] result = new List<string>[3] { new List<string>(), new List<string>(), new List<string>() };
                    if (result == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result[0].Add(reader["name"].ToString());
                        result[1].Add(reader["mask"].ToString());
                        result[2].Add(reader["regex"].ToString());
                    }
                    reader.Close();
                    return result;
                }
            }
            catch(MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<string>[0];
            }
        }

        public List<string> getRegion()
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM geo_regions", connect);
                    List<string> result = new List<string>();
                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(reader["name"].ToString());
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<string>();
            }
        }

        public List<string> getForm()
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT type FROM form", connect);
                    List<string> result = new List<string>();
                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(reader["type"].ToString());
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<string>();
            }
        }

        public List<string> getLiving()
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT type FROM living", connect);
                    List<string> result = new List<string>();
                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(reader["type"].ToString());
                    }
                    reader.Close();                  
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<string>();
            }
        }

        public List<string> getSocialOrganizations()
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM social_organizations", connect);
                    List<string> result = new List<string>();
                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(reader["name"].ToString());
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<string>();
            }
        }

        public List<string> getOrganizations()
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT name FROM organizations", connect);
                    List<string> result = new List<string>();
                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        result.Add(reader["name"].ToString());
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<string>();
            }
        }

        public string[] getUser(string snils)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT * FROM user WHERE snils='" + snils + "'", connect);
                    string[] result = new string[15];

                    if (resultCommand == null)
                    {
                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < 15; i++)
                        {
                            result[i] = reader[i].ToString();
                        }
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new string[0];
            }
        }

        public bool getAdditionalUser(string numberDocument)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT home FROM user WHERE document_number='" + numberDocument + "'", connect);
                    if (resultCommand == null)
                    { 
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return true;
            }
        }

        public string[] getUserAdd(string userId)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand resultCommand = new MySqlCommand("SELECT * FROM user_add WHERE document_number='" + userId + "'", connect);
                    string[] result = new string[14];

                    if (resultCommand == null)
                    {

                        return result;
                    }
                    MySqlDataReader reader = resultCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            result[i] = reader[i].ToString();
                        }
                    }
                    reader.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new string[0];
            }
        }

        public void setApplication(string user_snils, int true_user_add, string user_add_id, int social_organizations_idsocial_organizations, int organization_id, int form_id, string reason, int domestic, int medical, int psychological, int pedagogical, int labour, int legal, int communication, int urgent, string family, int living_id, int income, int consent, DateTime dateCreate, string user_login)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand addCommand = new MySqlCommand("INSERT INTO application (user_snils, true_user_add, user_add_id, social_organizations_idsocial_organizations, organization_id, form_id, reason, domestic, medical, psychological, pedagogical, labour, legal, communication, urgent, family, living_id, income, consent, date, user_login) " +
                           "VALUES (@user_snils, @true_user_add, @user_add_id, @social_organizations_idsocial_organizations, @organization_id, @form_id, @reason, @domestic, @medical, @psychological, @pedagogical, @labour, @legal, @communication, @urgent, @family, @living_id, @income, @consent, @date, @user_login)", connect);

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
                }
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.ToString());
            }
        }

        public void setUser(string snils,
            string name,
            string surname,
            string patronymic,
            string brithdates,
            int document_type,
            string document_number,
            int region,
            string region_small,
            string city,
            string street,
            int home,
            int apartment,
            string phone_number,
            string email)
        {
            try
            {
                using (connect){
                    connect.Open();
                    MySqlCommand addCommand = new MySqlCommand(@"
                    INSERT INTO 
                        user 
                        (snils, 
                        name, 
                        surname, 
                        patronymic, 
                        brithdates, 
                        document_type, 
                        document_number, 
                        region, 
                        region_small, 
                        city,
                        street, 
                        home, 
                        apartment, 
                        phone_number, 
                        email)
                    VALUES 
                        (@snils, 
                        @name, 
                        @surname, 
                        @patronymic, 
                        @brithdates, 
                        @document_type, 
                        @document_number, 
                        @region, 
                        @region_small, 
                        @city, 
                        @street, 
                        @home, 
                        @apartment, 
                        @phone_number, 
                        @email)", connect);
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
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void setAdditionalUser(string document_number,int document_type, string name,   int region, string region_small, string city, string street, int home, int apartment)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand addCommand = new MySqlCommand("INSERT INTO user_add (document_number, document_type, name, region, region_small, city, street, home, apartment) " +
                   "VALUES (@document_number, @document_type, @name, @region, @region_small, @city, @street, @home, @apartment)", connect);

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
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }

        public void setWhoOrganizations(string name)
        {
            try
            {
                using (connect)
                {
                    MySqlCommand addCommand = new MySqlCommand("INSERT INTO social_organizations (name) " +
                           "VALUES (@name)", connect);

                    addCommand.Parameters.AddWithValue("@name", name);

                    addCommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }

        public void setOrganizations(string name)
        {
            try
            {
                using (connect)
                {
                    MySqlCommand addCommand = new MySqlCommand("INSERT INTO organizations (name) " +
                           "VALUES (@name)", connect);
                    addCommand.Parameters.AddWithValue("@name", name);
                    addCommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }

        public void setLiving(string type)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand addCommand = new MySqlCommand(
                        "INSERT INTO living (type) " + "VALUES (@type)", connect);
                    addCommand.Parameters.AddWithValue("@type", type);
                    addCommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message); ;
            }
        }

        public string ExecuteSqlQuery(string query)
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand command = new MySqlCommand(query, connect);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            string result = "";
                            while (reader.Read())
                            {
                                string res = "";
                                if (result == "")
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        res += reader.GetName(i).ToString() + " | ";
                                    }
                                    res += "\n\n";
                                }
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    res += reader.GetValue(i).ToString() + " | ";

                                }
                                result += $"{res}\n\n";
                            }
                            return result;
                        }
                        else
                        {
                            return "Ввод успешен.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return "Ошибка: " + ex.Message;
            }
        }

        public void setApproved(string id, bool approved) 
        {
            try
            {
                using (connect)
                {
                    connect.Open();
                    MySqlCommand command = new MySqlCommand(@"UPDATE application SET proof_poverty = @bool 
                        WHERE (idapplication = @id);", connect);
                    command.Parameters.AddWithValue("@bool", approved == false ? 1 : 0);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            } catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
