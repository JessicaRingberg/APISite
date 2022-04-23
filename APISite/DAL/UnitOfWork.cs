
namespace APISite.DAL
{
    public class UnitOfWork
    {
        private EducationContext _educationContext;

        private IStudentRepository _studentRepository;
        private ICourseRepository _courseRepository;

        public IStudentRepository StudentRepository
        {
            get
            {
                if (_studentRepository is null)
                {
                    _studentRepository = new StudentRepository(_educationContext);
                }

                return _studentRepository;
            }
        }
        public ICourseRepository CourseRepository
        {
            get
            {
                if (_courseRepository is null)
                {
                    _courseRepository = new CourseRepository(_educationContext);
                }

                return _courseRepository;
            }
        }

        public UnitOfWork(EducationContext educationContext)
        {
            _educationContext = educationContext;
        }

        public void Save()
        {
            _educationContext.SaveChanges();
        }

    }
}