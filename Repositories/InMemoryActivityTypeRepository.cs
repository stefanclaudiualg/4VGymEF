using _4VGymAPI.Data;
using _4VGymAPI.Models;

namespace _4VGymAPI.Repositories
{
    public class ActivityTypeRepository
    {
        private readonly GymDbContext _gymDbContext;

        public ActivityTypeRepository(GymDbContext gymDbContext)
        {
            _gymDbContext = gymDbContext;
        }

        // Método para obtener todos los tipos de actividades
        public IEnumerable<ActivityType> GetAll()
        {
            return _gymDbContext.ActivityTypes.ToList();
        }

        /// <summary>
        /// Guarda un nuevo tipo de actividad generando su ID de forma automática. (Autonumerico)
        /// </summary>
        /// <param name="activityType">Objeto con los datos a guardar (sin ID o con ID no asignado).</param>
        /// <returns>La entidad guardada con su ID autogenerado.</returns>
        public ActivityType Add(ActivityType activityType)
        {
            // Asignamos el ID autoincremental (el enunciado indica que los POST no tienen por qué insertar en BBDD)
            int nextId = (_gymDbContext.ActivityTypes.Max(at => (int?)at.Id) ?? 0) + 1;
            activityType.Id = nextId;

            // Devolvemos el objeto completo tal como se ha guardado
            return activityType;
        }

        // Método para obtener todos los tipos de actividades por Nombre
        public ActivityType? GetByName(string name)
        {
            return _gymDbContext.ActivityTypes.SingleOrDefault(at =>
                at.Name.ToLower() == name.ToLower());
        }

        public ActivityType? GetById(int? id)
        {
            if (id == null)
                return null;

            return _gymDbContext.ActivityTypes
                .SingleOrDefault(i => i.Id == id);
        }
    }
}