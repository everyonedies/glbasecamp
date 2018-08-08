using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;
using Storage.ConsoleClient;

namespace Storage
{
    class Program
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = Path.Combine(currentDirectory, "..\\..\\appsettings.json");

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(path);
            IConfigurationRoot config = builder.Build();

            string conStr = config["connectionString"];

            while (true)
            {
                string str = ConsoleView.Menu();
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    connection.Open();
                    switch (str)
                    {
                        case "1":
                            ConsoleView.AddNewProduct(connection);
                            break;
                        case "2":
                            ConsoleView.AddNewBatch(connection);
                            break;
                        case "3":
                            ConsoleView.ShowAllProducts(connection);
                            break;
                        default: break;
                    }
                }
            }
        }
    }
}
