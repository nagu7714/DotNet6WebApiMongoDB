using StudentManagement.Models;

namespace StudentManagement.Services
{
    public interface IStudentService
    {
        Task<List<Student>> GetAsync();
        Task<Student> GetByIdAsync(string id); 
        Task<Student> CreateAsync(Student student);
        void UpdateAsync(string id,Student student);
        void DeleteAsync(string id);
        
    }
}
