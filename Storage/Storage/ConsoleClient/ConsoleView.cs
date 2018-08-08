using Model.Storage;
using Storage.Logic;
using System;
using System.Data.SqlClient;

namespace Storage.ConsoleClient
{
    class ConsoleView
    {
        public static string Menu()
        {
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 - Add new product;");
            Console.WriteLine("2 - Add new batch with several products;");
            Console.WriteLine("3 - Show all products.");
            Console.Write("Please, make your choice: ");
            string str = Console.ReadLine();
            return str;
        }

        public static void ShowAllProducts(SqlConnection connection)
        {
            Console.WriteLine("\nAll products:");
            ProductQuery productQuery = new ProductQuery(connection);
            var list = productQuery.SelectAllRecords();
            foreach (var i in list)
            {
                Console.WriteLine(i.ToString());
            }
        }

        public static void AddNewProduct(SqlConnection connection)
        {
            Product product = new Product();
            Console.WriteLine("Please, enter the product details: ");

            Console.Write("Name: ");
            product.Name = Console.ReadLine();

            Console.Write("UnitMeasure: ");
            product.UnitMeasure = Console.ReadLine();

            Console.Write("UnitPrice: ");
            product.UnitPrice = ConsoleInput.InputDouble();

            Console.Write("Quantity: ");
            product.Quantity = ConsoleInput.InputInt();

            ProductQuery productQuery = new ProductQuery(connection);
            int result = productQuery.AddNewRecord(product);
            Console.WriteLine($"Affected rows: {result}");
        }

        public static void AddNewBatch(SqlConnection connection)
        {
            Batch batch = new Batch();
            Console.WriteLine("Please, enter the batch details: ");

            Console.Write("Date: ");
            batch.Date = ConsoleInput.InputDate();

            Console.Write("Select batch type: ");
            batch.BatchType = SelectBatchType();

            BatchQuery batchQuery = new BatchQuery(connection);
            batchQuery.AddNewRecord(batch);
            int lastId = batchQuery.GetLastId();

            Console.WriteLine("Please, add products to the batch (input product's Id and quantity): ");
            ProductBatchQuery productBatchQuery = new ProductBatchQuery(connection);
            AddRecordsIntoProductBatch(lastId, productBatchQuery);
        }

        private static void AddRecordsIntoProductBatch(int lastId, ProductBatchQuery productBatchQuery)
        {
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Please, input corrrect existing product id: ");
                    int id = ConsoleInput.InputInt();
                    Console.Write("Please, input corrrect product product quantity: ");
                    int quantity = ConsoleInput.InputInt();

                    ProductBatch productBatch = new ProductBatch
                    {
                        BatchId_FK = lastId,
                        ProductId_FK = id,
                        ProductQuantity = quantity
                    };
                    productBatchQuery.AddNewRecord(productBatch);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.Write("Add another one product to the batch? (y/n): ");
                string str = Console.ReadLine();
                if (str == "n")
                {
                    flag = false;
                }
            }
        }

        private static string SelectBatchType()
        {
            bool flag = false;
            string result = "";
            while (!flag)
            {
                Console.WriteLine("\n1 - 'Unloading'");
                Console.WriteLine("2 - 'Loading'");
                Console.Write("Your choice: ");
                string type = Console.ReadLine();
                switch (type)
                {
                    case "1":
                        result = "Unloading";
                        flag = true;
                        break;
                    case "2":
                        result = "Loading";
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Wrong type of batch. Please, select a correct option from the list: ");
                        break;
                }
            }
            return result;
        }
    }
}
