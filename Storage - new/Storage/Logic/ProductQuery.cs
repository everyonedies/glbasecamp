using Model.Storage;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Storage.Logic
{
    class ProductQuery
    {
        private SqlConnection connection;

        public ProductQuery(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Product> SelectAllRecords()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Product";

            List<Product> product = new List<Product>();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    product.Add(new Product
                    {
                        ProductId = (int)dr["ProductId"],
                        Name = (string)dr["Name"],
                        Quantity = (int)dr["Quantity"],
                        UnitMeasure = (string)dr["UnitMeasure"],
                        UnitPrice = (double)dr["UnitPrice"],
                    });
                }
            }

            return product;
        }

        public int AddNewRecord(Product product)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Product Values(@Name, @UnitMeasure, @UnitPrice, @Quantity)";

            cmd.Parameters.AddWithValue("@Name", product.Name);
            cmd.Parameters.AddWithValue("@UnitMeasure", product.UnitMeasure);
            cmd.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

            int res = cmd.ExecuteNonQuery();
            return res;
        }
    }
}
