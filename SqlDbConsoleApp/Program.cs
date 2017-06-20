using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Azure;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.ResourceManager.Models;
using Microsoft.Azure.Management.Sql;
using Microsoft.Azure.Management.Sql.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.IO;


namespace SqlDbConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SqlConnectionStringBuilder data_conector = new SqlConnectionStringBuilder();
                data_conector.DataSource = "serverazurehw5.database.windows.net";
                data_conector.UserID = "serveradmin";
                data_conector.Password = "Admin123";
                data_conector.InitialCatalog = "AzureHW5";
                Console.WriteLine("Data is connected");

                using (SqlConnection connection = new SqlConnection(data_conector.ConnectionString))
                {
                    Console.WriteLine("Insert you login");
                    string login = Console.ReadLine();
                    Console.WriteLine("Insert you password");
                    string pass = Console.ReadLine();
                    connection.Open();
                    string query = "SELECT Users.Us_pass FROM [Users] Users WHERE Users.Us_name=" + "'" + login + "'";
                    using (SqlCommand comm = new SqlCommand(query, connection))
                    {
                        string reader = comm.ExecuteScalar().ToString();
                        {
                            if (reader == pass)
                            {
                                Console.WriteLine("You enter in Data. Wellcome!");
                                query = "SELECT Books.Autor, Books.Book FROM Books";
                                using (SqlCommand comm2 = new SqlCommand(query, connection))
                                {
                                    using (SqlDataReader reader2 = comm2.ExecuteReader())
                                    {
                                        Console.WriteLine("Books:");
                                        while (reader2.HasRows)
                                        {
                                            while (reader2.Read())
                                            {
                                                Console.WriteLine("Autor: " + reader2["Autor"] + "; Books name: " + reader2["Book"] + "\n");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("Error password or login, try again");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Oops! Conected to Data error =(");
                throw;
            }
        }
    }   
}
