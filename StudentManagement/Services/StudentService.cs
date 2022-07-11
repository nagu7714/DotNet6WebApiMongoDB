using StudentManagement.Models;
using StudentManagement.Database;
using MongoDB.Driver;

namespace StudentManagement.Services
{
    public class StudentService : IStudentService
    { 

       private readonly IMongoCollection<Student> _students;
             

        public StudentService(IStudentStoreDatabaseSettings studentStoreDatabaseSettings,IMongoClient mongoClient)
        {
            var dataBase = mongoClient.GetDatabase(studentStoreDatabaseSettings.DatabaseName);
            _students = dataBase.GetCollection<Student>(studentStoreDatabaseSettings.StudentCoursesCollectionName);

        }
        public async Task<Student> CreateAsync(Student student)
        {
            await _students.InsertOneAsync(student);
            return student;
        }

        public async void DeleteAsync(string id)
        {
           await _students.DeleteOneAsync(student => student.Id == id);
        }

        public async Task<List<Student>> GetAsync()
        {
            return await _students.Find(student => true).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await _students.Find(student => student.Id == id).SingleOrDefaultAsync();
        }

        public async void UpdateAsync(string id, Student student)
        {
           await _students.ReplaceOneAsync(student => student.Id == id, student);
        }
    }
}
