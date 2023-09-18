using Microsoft.Data.SqlClient;
using System.Configuration;

namespace Codecool.CodecoolShop.Daos
{
    public class DatabaseManager
    {
        public string ConnectionString => ConfigurationManager.AppSettings["connectionString"];

        public void ConnectToDB()
        {
            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
        }
    }
}
