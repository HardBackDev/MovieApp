using Dapper;
using Repository;

namespace MovieApp.Migrations
{
    public class Database
    {
        private readonly DapperContext _context;

        public Database(DapperContext context) => _context = context;
        
        public void CreateDatabase(string dbName)
        {
            var query = "SELECT datname FROM pg_database WHERE datname = @name;";

            var parameters = new DynamicParameters();
            parameters.Add("name", dbName);

            using (var connection = _context.CreateMasterConnection())
            {
                var records = connection.Query(query, parameters);

                if (!records.Any())
                {
                    connection.Execute($"CREATE DATABASE {dbName}");
                }
            }
        }
    }
}
