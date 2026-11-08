using _4VGymAPI.Data;
using _4VGymAPI.Models;

namespace _4VGymAPI.Repositories
{
    public class ActivityRepository
    {
        private readonly GymDbContext _gymDbContext;

        public ActivityRepository(GymDbContext gymDbContext)
        {
            _gymDbContext = gymDbContext;
        }

        public Activity? Add(Activity activity, out List<ErrorResponse> errorResponse)
        {
            errorResponse = new List<ErrorResponse>();

            // Validamos todas los referente a las entidades cruzadas
            // Validación 1: Que exista el tipo de actividad
            if (activity.ActivityTypeId == null || !_gymDbContext.ActivityTypes.Any(at => at.Id == activity.ActivityTypeId))
            {
                errorResponse.Add(new ErrorResponse(34, "EL tipo de actividad debe de funcionar"));
            }

            // Validacion 2: Que existan todos los monitores
            errorResponse =
                activity.InstructorsId
                        .Where(instructorId => !_gymDbContext.Instructors.Any(i => i.Id == instructorId))
                        .Select(instructorIdNotFound =>
                        {
                            return new ErrorResponse(35, "El instructor: " + instructorIdNotFound + " no existe");
                        })
                        .ToList();

            if (errorResponse.Count > 0)
            {
                return null;
            }

            // Asignamos el ID autoincremental en la capa de datos (como no se exige insertar en BBDD según el enunciado)
            int nextId = (_gymDbContext.Activities.Max(a => (int?)a.Id) ?? 0) + 1;
            activity.Id = nextId;

            // Devolvemos el objeto completo tal como se ha guardado
            return activity;
        }
    }
}