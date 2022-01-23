using MySql.Data.MySqlClient;

namespace Data
{
    public static class DataConnection
    {
        private static readonly string ConnectionString = new MySqlConnectionStringBuilder
        {
            Server = "185.182.57.122",
            Port = 3306,
            Database = "alexkxl197_exoshop",
            UserID = "alexkxl197_exoshop",
            Password = "zv4uncoQ4"
        }.ConnectionString;

        public static MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}