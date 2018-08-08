using System.Data.SqlClient;
using Storage.ConsoleClient;

namespace Storage
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionStr = Properties.Settings.Default.myConnection;

            while (true)
            {
                string str = ConsoleView.Menu();
                using (SqlConnection connection = new SqlConnection(connectionStr))
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
