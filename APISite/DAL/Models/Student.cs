
namespace APISite.DAL.Models
{
    public class Student
    {
        public Student()
        {
            Courses = new List<Course>();
        }

        public int StudentId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}