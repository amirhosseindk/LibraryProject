using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Database
{
    public class DbSession : IDisposable
    {
        private readonly string _connectionString;
        public IDbConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        public DbSession(string connectionString)
        {
            _connectionString = connectionString;
            Connection = new SqlConnection(_connectionString);
            Connection.Open();
        }

        public void Dispose() => Connection?.Dispose();
    }
}