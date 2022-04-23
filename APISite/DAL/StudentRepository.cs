using APISite.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace APISite.DAL
{
    public class StudentRepository : IStudentRepository
    {
        private readonly EducationContext _educationContext;

        public StudentRepository(EducationContext educationContext)
        {
            _educationContext = educationContext;
        }
        public bool CreateStudent(Student? student)
        {
            var exists = _educationContext.Students.FirstOrDefault(s => s.Email == student.Email);
            if (exists is not null)
            {
                return false;
            }
            _educationContext.Students.Add(student);
            return true;
        }

        public bool DeleteStudent(string email)
        {
            var existingStudent = _educationContext.Students.FirstOrDefault(s => s.Email == email);
            if (existingStudent is null)
            {
                return false;
            }

            _educationContext.Students.Remove(existingStudent);
            return true;
        }

        public ICollection<Student> GetAllStudents()
        {
            return _educationContext.Students.ToList();
        }

        public Student? GetOneStudent(string email)
        {
            return _educationContext.Students.FirstOrDefault(s => s.Email == email);
        }

        public bool UpdateStudent(Student existing, Student student)
        {
            existing.Email = student.Email;
            existing.FirstName = student.FirstName;
            existing.LastName = student.LastName;
            existing.Address = student.Address;
            existing.Phone = student.Phone;
            _educationContext.Students.Update(existing);
            _educationContext.SaveChanges();
            return true;
        }
        public ICollection<Course>? GetStudentCourses(string email)
        {
            var student = _educationContext.Students.Include(s => s.Courses).FirstOrDefault(s => s.Email == email);
            if (student is null)
                return null;

            return student.Courses;
        }

    }
}