using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.IO;

namespace AdventureWorks.Functions
{
    public static class FileProcessor
    {
        [FunctionName("FileProcessor")]
        public static void Run(
            [QueueTrigger("documents", Connection = "AzureWebJobsStorage")] Message message,
            [Blob("documents/{documentId}", FileAccess.Read, Connection = "AzureWebJobsStorage")] Stream blob,
            ILogger log)
        {
            byte [] bytes;

            using (MemoryStream ms = new MemoryStream())
            {
                blob.CopyTo(ms);
                bytes = ms.ToArray();
            }

            using (SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("DefaultConnection")))
            {
                connection.Open();
                var sql = $"INSERT INTO [Production].[Document] (Id, Metadata, Bytes) VALUES(@Id, @Metadata, @Bytes)";

                using(SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", message.DocumentId);
                    cmd.Parameters.AddWithValue("@Metadata", message.Metadata);
                    cmd.Parameters.AddWithValue("@Bytes", bytes);

                    cmd.ExecuteNonQuery();
                }
            }
            log.LogInformation($"C# Queue trigger function processed: {message}");
        }
    }
}
