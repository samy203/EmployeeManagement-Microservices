namespace Department.Core.Services
{
    public interface IDepartmentService
    {
        Models.Department Add(Models.Department department);
        Models.Department Update(Models.Department department);
        Task<Models.Department> Get(string departmentId);
        Task<List<Models.Department>> GetAll();
        Task<bool> Delete(string departmentId);
    }
}
