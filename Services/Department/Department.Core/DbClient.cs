using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Department.Core
{
    public class DbClient : IDbClient
    {
        private readonly IMongoCollection<Models.Department> _departments;
        public DbClient(IOptions<DepartmentDBConfig> options)
        {
            var client = new MongoClient(options.Value.Connection_String);
            var database = client.GetDatabase(options.Value.Database_Name);
            _departments = database.GetCollection<Models.Department>(options.Value.Departments_Collection_Name);
        }
        public IMongoCollection<Models.Department> GetDepartmentsCollection() => _departments;
    }
}
