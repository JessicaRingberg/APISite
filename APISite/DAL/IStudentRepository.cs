
using APISite.DAL.Models;

namespace APISite.DAL
{
    public interface IStudentRepository
    {
        public ICollection<Student> GetAllStudents();
        Student? GetOneStudent(string email);
        bool CreateStudent(Student? student);
        public bool UpdateStudent(Student existing, Student student);
        bool DeleteStudent(string email);
        ICollection<Course>? GetStudentCourses(string email);

    }
}