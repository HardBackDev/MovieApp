using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;

namespace Repository
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        => new NpgsqlConnection(_configuration.GetConnectionString("npgConnection"));
        public IDbConnection CreateMasterConnection()
        => new NpgsqlConnection(_configuration.GetConnectionString("masterConnection"));
    }
}
