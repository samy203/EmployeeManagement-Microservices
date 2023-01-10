using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Department.Core.Models
{
    public class Department
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int EmployeesCount { get; set; }
        public int ManagerId { get; set; }
    }
}
