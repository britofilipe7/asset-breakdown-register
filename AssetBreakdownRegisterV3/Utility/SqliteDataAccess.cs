using AssetBreakdownRegisterV2.models;
using System.Data.SQLite;
using AssetBreakdownRegisterV3.Properties;
using System;
using System.IO;
using AssetBreakdownRegisterV2.Utility;
using Microsoft.Office.Interop.Excel;

namespace AssetBreakdownRegisterV3.Utility
{
    public class SqliteDataAccess
    {
        //Assign the database location (can be found in settings.settings)
        private string dataBaseLocation;
        private string connectionString;

        //Declare global variables
        public List<BreakdownModel> breakdown_list;
        public List<CategoryModel> categories;
        public List<SubcategoryModel> subcategories;
        public List<StatusModel> status;

        //Constructor for the class
        public SqliteDataAccess()
        {
            this.dataBaseLocation = Path.Combine(Directory.GetCurrentDirectory(), Settings.Default["DBLocation"].ToString());
            this.connectionString = "data source=" + dataBaseLocation;
            LoadData();
        }

        /*SQL helper functions*/
        //Function to create new Connection
        private SQLiteConnection CreateConnection()
        {
            SQLiteConnection conn = new SQLiteConnection(connectionString);
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return conn;
        }

        //Function to read data from database tables
        public void LoadData()
        {
            //Initialize global variables
            breakdown_list = new List<BreakdownModel>();
            categories = new List<CategoryModel>();
            subcategories = new List<SubcategoryModel>();
            status = new List<StatusModel>();

            //check if database file exists
            if (!File.Exists(dataBaseLocation))
            {
                MessageBox.Show("A base de dados não existe.");
                //return;
            }

            SQLiteConnection connection = CreateConnection();

            //Dictionary of all queries that will be executed, in order to assign values to all global variables
            Dictionary<string, string> queries = new Dictionary<string, string>()
            {
                {"breakdown", "SELECT * FROM breakdown"},
                {"categories", "SELECT * FROM category"},
                {"subcategories", "SELECT * FROM subcategory"},
                {"status", "SELECT * FROM status"}
            };

            //Loop each query
            foreach (KeyValuePair<string, string> query in queries)
            {
                using (SQLiteCommand command = new SQLiteCommand(query.Value, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        //depending on the dictionary key, the results of the query are read and passed to a temp model of the respective type and added to the list
                        switch (query.Key)
                        {
                            case "breakdown":
                                while (reader.Read())
                                {
                                    BreakdownModel model = new BreakdownModel
                                    {
                                        Id = reader.GetInt32(0),
                                        Description = reader.GetString(1),
                                        Code = reader.GetString(2),
                                        Category = reader.GetString(3),
                                        Subcategory = reader.GetString(4),
                                        Request_date = reader.GetString(5),
                                        Status = reader.GetString(6),
                                        Register_date = reader.GetString(7),
                                        Severity = reader.GetInt32(8),
                                        Observations = reader.GetString(9),
                                        State = reader.GetString(10),
                                        Color = reader.GetString(11),
                                        Cost = reader.GetDouble(12),
                                    };
                                    breakdown_list.Add(model);
                                }
                                break;

                            case "categories":
                                while (reader.Read())
                                {
                                    CategoryModel model = new CategoryModel
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                    };
                                    categories.Add(model);
                                }
                                break;

                            case "subcategories":
                                while (reader.Read())
                                {
                                    SubcategoryModel model = new SubcategoryModel
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        CategoryID = reader.GetInt32(2),
                                    };
                                    subcategories.Add(model);
                                }
                                break;

                            case "status":
                                while (reader.Read())
                                {
                                    StatusModel model = new StatusModel
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                    };
                                    status.Add(model);
                                }
                                break;
                        }
                    }
                }
            }
        }

        //Function to delete data from database, user should input the table and the value to delete
        public void DeleteData(string table, string column, string value)
        {
            SQLiteConnection connection = CreateConnection();
            //activate foreign_keys since their not activate by default (otherwise ON DELETE CASCADE only not work)
            string activateForeign = "PRAGMA foreign_keys = ON";
            using (SQLiteCommand command = new SQLiteCommand(activateForeign, connection))
            {
                command.ExecuteNonQuery();
            }
            string query = string.Format("DELETE FROM {0} WHERE {1} = @value", table, column);
            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@value", value);
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    //print the exception to a messagebox 
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Function to update data from database, user should input the table and only 1 value to update
        public void UpdateData(string table, string column, string value, string id)
        {
            SQLiteConnection connection = CreateConnection();
            string query = string.Format("UPDATE {0} SET {1} = @value WHERE id = @id", table, column);

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@value", value);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    //print the exception to a messagebox 
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Function to update data from database, user should input the table and only 2 values to update
        public void UpdateData(string table, string column1, string column2, string value1, string value2, string id)
        {
            SQLiteConnection connection = CreateConnection();
            string query = string.Format("UPDATE {0} SET {1} = @value1, {2} = @value2 WHERE id = @id", table, column1, column2);

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@value1", value1);
                    command.Parameters.AddWithValue("@value2", value2);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    //print the exception to a messagebox 
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Function to update data from database, user should input the table and only 3 values to update
        public void UpdateData(string table, string column1, string column2, string column3, string value1, string value2, string value3, string id)
        {
            SQLiteConnection connection = CreateConnection();
            string query = string.Format("UPDATE {0} SET {1} = @value1, {2} = @value2, {3} = @value3 WHERE id = @id", table, column1, column2, column3);

            using (SQLiteCommand command = new SQLiteCommand(query, connection))
            {
                try
                {
                    command.Parameters.AddWithValue("@value1", value1);
                    command.Parameters.AddWithValue("@value2", value2);
                    command.Parameters.AddWithValue("@value3", value3);
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                catch (SQLiteException ex)
                {
                    //print the exception to a messagebox 
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        //Function to insert data to the database, user should provide a model as a parameter
        public void InsertData(BreakdownModel model)
        {
            //Insert into the table breakdown the new breakdown defined by the user
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO breakdown(description, code, category, subcategory, request_date, status, register_date, severity," +
            "observations, state, color, cost) VALUES (@description, @code, @category, @subcategory, @request_date, @status, @register_date, @severity, @observations, @state, @color, @cost)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@description", model.Description);
                        command.Parameters.AddWithValue("@code", model.Code);
                        command.Parameters.AddWithValue("@category", model.Category);
                        command.Parameters.AddWithValue("@subcategory", model.Subcategory);
                        command.Parameters.AddWithValue("@request_date", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        command.Parameters.AddWithValue("@status", "");
                        command.Parameters.AddWithValue("@register_date", "");
                        command.Parameters.AddWithValue("@severity", model.Severity);
                        command.Parameters.AddWithValue("@observations", model.Observations);
                        command.Parameters.AddWithValue("@state", model.State);
                        command.Parameters.AddWithValue("@color", model.Color.ToString());
                        command.Parameters.AddWithValue("@cost", model.Cost);
                        command.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        //print the exception to a messagebox
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        public void InsertData(StatusModel model)
        {
            //Insert into the table breakdown the new breakdown defined by the user
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO status(name) VALUES (@name)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@name", model.Name);
                        command.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        //catch the exceptions if the new "name" is already in the table status and make a personalize messagebox
                        if (ex.ToString().Contains("constraint failed"))
                        {
                            MessageBox.Show("Erro: O valor introduzido já existe.");
                        }
                        //print the exception to a messagebox 
                        else
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        public void InsertData(CategoryModel model)
        {
            //Insert into the table breakdown the new breakdown defined by the user
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO category(name) VALUES (@name)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@name", model.Name);
                        command.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        //catch the exceptions if the new "name" is already in the table category and make a personalize messagebox
                        if (ex.ToString().Contains("constraint failed"))
                        {
                            MessageBox.Show("Erro: O valor introduzido já existe.");
                        }
                        //print the exception to a messagebox 
                        else
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        public void InsertData(SubcategoryModel model)
        {
            //Insert into the table breakdown the new breakdown defined by the user
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO subcategory(name, categoryID) VALUES (@name, @category)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@name", model.Name);
                        command.Parameters.AddWithValue("@category", model.CategoryID);
                        command.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        if (ex.ToString().Contains("constraint failed"))
                        {
                            //catch the exceptions if the new "name" is already in the table subcategory and make a personalize messagebox
                            MessageBox.Show("Erro: O valor introduzido já existe.");
                        }
                        else
                        {
                            //print the exception to a messagebox 
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }

        public void ClearDB()
        {
            //Create a confirmationBox object and proceed only if the setting is true and the user select Yes
            string message = "Deseja eliminar todos os dados da base de dados. Esta ação é irreversível.";
            ConfirmationBox confirmationBox = new ConfirmationBox(message);
            if (!confirmationBox.Ask())
            {
                return;
            } else
            {
                //Insert into the table breakdown the new breakdown defined by the user
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    List<string> queries = new List<string> {
                        "DELETE FROM breakdown;",
                        "DELETE FROM status;",
                        "DELETE FROM category;",
                        "DELETE FROM subcategory;",
                        "DELETE FROM sqlite_sequence;"
                    };

                    foreach (string query in queries)
                    {
                        using (SQLiteCommand command = new SQLiteCommand(query, connection))
                        {
                            try
                            {
                                
                                command.ExecuteNonQuery();
                            }
                            catch (SQLiteException ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
            }
        }
    }
}
