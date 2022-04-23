using APISite.DAL.Models;
namespace APISite.DAL
{
    public interface ICourseRepository
    {
        List<Course> GetAllCourses();
        Course? GetOneCourse(int id);
        bool CreateCourse(Course? course);
        public bool UpdateCourse(Course existing, Course? course);
        public bool DeleteCourse(int id);
        bool SignUpToCourse(string email, int id);
    }
}