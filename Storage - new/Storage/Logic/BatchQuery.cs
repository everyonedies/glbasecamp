using Model.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Storage.Logic
{
    class BatchQuery
    {
        private SqlConnection connection;

        public BatchQuery(SqlConnection connection)
        {
            this.connection = connection;
        }

        public List<Batch> SelectAllRecords()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Batch";

            List<Batch> batch = new List<Batch>();
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    batch.Add(new Batch
                    {
                        BatchId = (int)dr["BatchId"],
                        Date = (DateTime)dr["Date"],
                        BatchType = (string)dr["BatchType"],
                    });
                }
            }

            return batch;
        }

        public int AddNewRecord(Batch batch)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $@"INSERT INTO Batch Values(@date, @type)";

            cmd.Parameters.AddWithValue("@date", batch.Date);
            cmd.Parameters.AddWithValue("@type", batch.BatchType);

            int res = cmd.ExecuteNonQuery();
            return res;
        }

        public int GetLastId()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $@"SELECT MAX(BatchId) AS Id FROM Batch";
            int result = 0;

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    result = (int)dr["Id"];
                }
            }
            return result;
        }
    }
}
