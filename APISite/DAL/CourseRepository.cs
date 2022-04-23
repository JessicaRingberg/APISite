using APISite.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace APISite.DAL
{
    public class CourseRepository : ICourseRepository
    {
        private readonly EducationContext _educationContext;

        public CourseRepository(EducationContext educationContext)
        {
            _educationContext = educationContext;
        }
        public bool CreateCourse(Course? course)
        {
            var exists = _educationContext.Courses.FirstOrDefault(c => c.Title == course.Title);
            if (exists is not null)
            {
                return false;
            }
            _educationContext.Courses.Add(course);
            return true;
        }

        public bool DeleteCourse(int id)
        {
            var existingCourse = _educationContext.Courses.FirstOrDefault(c => c.Id == id);
            if (existingCourse is null)
            {
                return false;
            }

            _educationContext.Courses.Remove(existingCourse);

            return true;
        }

        public List<Course> GetAllCourses()
        {
            return _educationContext.Courses.ToList();
        }

        public Course? GetOneCourse(int id)
        {
            return _educationContext.Courses.FirstOrDefault(c => c.Id == id);
        }

        public bool UpdateCourse(Course existing, Course? course)
        {
            existing.Title = course.Title;
            existing.Status = course.Status;
            existing.Description = course.Description;
            existing.Difficulty = course.Difficulty;
            existing.Length = course.Length;
            existing.Students = course.Students;
            _educationContext.SaveChanges();
            return true;
        }
        public bool SignUpToCourse(string email, int id)
        {
            var activeStudent = _educationContext.Students.FirstOrDefault(s => s.Email == email);
            if (activeStudent is null)
            {
                return false;
            }
            var selectedCourse = _educationContext.Courses.FirstOrDefault(c => c.Id == id);
            if (selectedCourse is null)
            {
                return false;
            }

            var student = _educationContext.Students.Include(s => s.Courses).FirstOrDefault(s => s.Email == email);
            if (student.Courses.Contains(selectedCourse))
            {
                return false;
            }
            activeStudent.Courses.Add(selectedCourse);
            _educationContext.SaveChanges();

            return true;
        }
        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}