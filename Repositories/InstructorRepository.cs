using _4VGymAPI.Data;
using _4VGymAPI.Models;

namespace _4VGymAPI.Repositories
{
    public class InstructorRepository
    {
        private readonly GymDbContext _gymDbContext;

        public InstructorRepository(GymDbContext gymDbContext)
        {
            _gymDbContext = gymDbContext;
        }

        public IEnumerable<Instructor> GetByFirstInitial(string? firstInitial = null)
        {
            // Si se recibe la inicial, aplicamos el filtro sobre la colección
            if (!string.IsNullOrWhiteSpace(firstInitial))
            {
                IEnumerable<Instructor> filteredInstructors =
                    _gymDbContext.Instructors
                        .Where(instructor =>
                                !string.IsNullOrEmpty(instructor.Name) &&
                                instructor.Name.StartsWith(firstInitial)
                        )
                        .ToList();

                return filteredInstructors;
            }

            return _gymDbContext.Instructors.ToList();
        }

        public IEnumerable<Instructor> GetAll()
        {
            return _gymDbContext.Instructors.ToList();
        }

        public Instructor? GetById(int id)
        {
            return _gymDbContext.Instructors
                .SingleOrDefault(i => i.Id == id);
            // FistOrDefault mejor que Where
        }

        public List<Instructor> GetByIds(List<int> ids)
        {
            if (ids == null || !ids.Any())
                return new List<Instructor>();

            return _gymDbContext.Instructors
                .Where(instructor => ids.Contains(instructor.Id))
                .ToList();
        }

        public Instructor? Add(Instructor instructorNew, out ErrorResponse? errorResponse)
        {
            errorResponse = null;
            // Comprobación de duplicados (Email o Nombre existente, ignorando mayúsculas/minúsculas)
            bool exists = _gymDbContext.Instructors.Any(instructor =>
                instructor.Email.ToLower() == instructorNew.Email.ToLower() ||
                instructor.Name.ToLower() == instructorNew.Name.ToLower());

            if (exists)
            {
                errorResponse = new ErrorResponse(34, "An instructor with the same email or name already exists");
                return null;
            }

            int nuevoId = (_gymDbContext.Instructors.Max(i => (int?)i.Id) ?? 0) + 1;
            Instructor instructor = new Instructor(nuevoId, instructorNew.Name, instructorNew.Email, instructorNew.PhotoUrl, instructorNew.Phone);

            return instructor;
        }
    }
}