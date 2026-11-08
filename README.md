# Entrega 4VGym - API con Entity Framework

Proyecto que une la API de 4VGym con Entity Framework Core utilizando SQL Server.

Se mantiene el mismo Swagger y funcionamiento que en la práctica en memoria, pero ahora los datos se leen desde la base de datos a través de `GymDbContext` y las clases repositorio.

---

### Decisiones tomadas

#### 1. ¿Separar modelos de BBDD y DTOs para la API?
Sí, se han separado. 
En la base de datos la relación entre actividades y monitores es de muchos a muchos (M:N) y utiliza una tabla intermedia (`ActivityInstructor`), mientras que la API necesita recibir y devolver directamente una lista de IDs (`instructors_id: [1, 2]`). Además, separar los modelos evita problemas de referencias circulares al serializar a JSON.

#### 2. ¿Dónde se hacen las validaciones?
Las validaciones se dividen en dos partes:
* **En la API / DTOs:** Validaciones de formato y campos obligatorios usando DataAnnotations (`[Required]`, `[Range]`, `[FutureDate]`, etc.) y expresiones regulares en los controladores (como el teléfono y el correo).
* **En los Repositorios:** Validaciones contra la base de datos mediante LINQ. Por ejemplo, comprobar que no se repitan nombres o emails, y comprobar que existan en la BBDD tanto el tipo de actividad como los monitores asignados.

*Nota: Siguiendo las instrucciones de la entrega, los métodos POST validan todos los datos contra la base de datos y devuelven 201 Created, pero no realizan inserción física (no hacen `SaveChanges`).*

---

### Pasos para probar el proyecto

1. **Configurar la conexión:**
   Revisar la cadena de conexión en `appsettings.json` con la instancia local de SQL Server (`\SQLEXPRESS`).

2. **Crear la base de datos:**
   Ejecutar en la consola de comandos dentro de la carpeta del proyecto:
   ```bash
   dotnet ef database update
   ```

3. **Insertar los datos de prueba:**
   Ejecutar el script `seed_data.sql` en SQL Server Management Studio sobre la base de datos `4vGym`.

4. **Arrancar la API:**
   Ejecutar el proyecto (`F5` en Visual Studio o con `dotnet run`) y abrir Swagger para probar los endpoints.