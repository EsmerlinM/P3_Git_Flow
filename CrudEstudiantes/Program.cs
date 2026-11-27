var estudiantes = new List<Estudiante>();   // aquí se guardan en memoria
var siguienteId = 1;
var salir = false;

while (!salir)   // el menú se repite hasta elegir salir
{
    Console.WriteLine("\n=== Gestión de Estudiantes ===");
    Console.WriteLine("1. Agregar estudiante");
    Console.WriteLine("2. Listar estudiantes");
    Console.WriteLine("3. Editar estudiante");
    Console.WriteLine("4. Eliminar estudiante");    
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            Agregar();
            break;
        case "2":
            Listar();
            break;
        case "3":
            Editar();
            break;    
        case "0":
            salir = true;
            break;
        case "4":
            Eliminar();
            break;        
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}

void Agregar()
{
    var estudiante = new Estudiante
    {
        Id = siguienteId++,
        Nombre = Leer("Nombre: "),
        Matricula = Leer("Matrícula: "),
        Carrera = Leer("Carrera: ")
    };
    estudiantes.Add(estudiante);
    Console.WriteLine($"Estudiante agregado con ID {estudiante.Id}.");
}

void Listar()
{
    if (estudiantes.Count == 0)   // lista vacía
    {
        Console.WriteLine("No hay estudiantes registrados.");
        return;                   // sale de la función
    }

    foreach (var e in estudiantes)   // recorre uno por uno
    {
        Console.WriteLine($"{e.Id}. {e.Nombre} | {e.Matricula} | {e.Carrera}");
    }
}

Estudiante? BuscarPorId()
{
    if (!int.TryParse(Leer("ID del estudiante: "), out var id))   // ¿es un número?
    {
        Console.WriteLine("El ID debe ser un número.");
        return null;
    }

    var estudiante = estudiantes.FirstOrDefault(e => e.Id == id);   // busca el ID
    if (estudiante == null)
    {
        Console.WriteLine("No existe un estudiante con ese ID.");
    }
    return estudiante;
}

void Editar()
{
    var estudiante = BuscarPorId();
    if (estudiante == null) return;

    Console.WriteLine("Deja vacío para conservar el valor actual.");

    var nombre = Leer($"Nombre ({estudiante.Nombre}): ");
    if (nombre != "") estudiante.Nombre = nombre;

    var matricula = Leer($"Matrícula ({estudiante.Matricula}): ");
    if (matricula != "") estudiante.Matricula = matricula;

    var carrera = Leer($"Carrera ({estudiante.Carrera}): ");
    if (carrera != "") estudiante.Carrera = carrera;

    Console.WriteLine("Estudiante actualizado.");
}

void Eliminar()
{
    var estudiante = BuscarPorId();   // reutiliza la búsqueda
    if (estudiante == null) return;

    var respuesta = Leer($"¿Eliminar a {estudiante.Nombre}? (s/n): ");
    if (respuesta.ToLower() != "s")   // pide confirmación
    {
        Console.WriteLine("Operación cancelada.");
        return;
    }

    estudiantes.Remove(estudiante);
    Console.WriteLine("Estudiante eliminado.");
}

string Leer(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine()?.Trim() ?? "";
}
