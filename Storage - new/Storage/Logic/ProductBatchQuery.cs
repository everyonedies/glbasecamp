using Model.Storage;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Storage.Logic
{
    class ProductBatchQuery
    {
        private SqlConnection connection;

        public ProductBatchQuery(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<ProductBatch> SelectAllRecords()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Product";

            List<ProductBatch> product = new List<ProductBatch>();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    product.Add(new ProductBatch
                    {
                        Id = (int)dr["Id"],
                        BatchId_FK = (int)dr["BatchId_FK"],
                        ProductId_FK = (int)dr["ProductId_FK"],
                        ProductQuantity = (int)dr["ProductQuantity"],
                    });
                }
            }

            return product;
        }

        public int AddNewRecord(ProductBatch productBatch)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO ProductBatch Values(@batchId, @productId, @quantity)";

            cmd.Parameters.AddWithValue("@batchId", productBatch.BatchId_FK);
            cmd.Parameters.AddWithValue("@productId", productBatch.ProductId_FK);

            cmd.Parameters.AddWithValue("@quantity", productBatch.ProductQuantity);
            int res = cmd.ExecuteNonQuery();
            return res;
        }
    }
}
