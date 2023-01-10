using MongoDB.Driver;

namespace Department.Core.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMongoCollection<Models.Department> _departments;
        public DepartmentService(IDbClient dbClient)
        {
            _departments = dbClient.GetDepartmentsCollection();
        }
        public Models.Department Add(Models.Department department)
        {
            //insertOne has major issue that it doesnt return the ID of newly created db object.
            // SEE -> https://jira.mongodb.org/browse/CSHARP-3289
            _departments.InsertOne(department);

            return department;
        }

        public Models.Department Update(Models.Department department)
        {
            _departments.ReplaceOne(x => x.Id == department.Id, department);
            return department;
        }

        public async Task<bool> Delete(string departmentId)
        {
            var result = await _departments.DeleteOneAsync(x => x.Id == departmentId);
            return result.IsAcknowledged;
        }

        public async Task<Models.Department> Get(string departmentId)
        {
            return await _departments.Find(x => x.Id == departmentId).FirstOrDefaultAsync();
        }

        public async Task<List<Models.Department>> GetAll()
        {
            return await _departments.Find(FilterDefinition<Models.Department>.Empty).ToListAsync();
        }
    }
}
