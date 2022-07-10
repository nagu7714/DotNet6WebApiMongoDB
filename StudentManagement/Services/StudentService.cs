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
        public Student Create(Student student)
        {
            _students.InsertOne(student);
            return student;
        }

        public void Delete(string id)
        {
            _students.DeleteOne(student => student.Id == id);
        }

        public List<Student> Get()
        {
            return _students.Find(student => true).ToList();
        }

        public Student GetById(string id)
        {
            return _students.Find(student => student.Id == id).FirstOrDefault();
        }

        public void Update(string id, Student student)
        {
            _students.ReplaceOne(student => student.Id == id, student);
        }
    }
}
