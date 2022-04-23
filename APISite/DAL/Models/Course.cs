using System.Text.Json.Serialization;

namespace APISite.DAL.Models
{
    public class Course
    {
        public Course()
        {
            Students = new List<Student>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public string Difficulty { get; set; }
        public string Status { get; set; }
        [JsonIgnore]
        public virtual ICollection<Student> Students { get; set; }
    }
}