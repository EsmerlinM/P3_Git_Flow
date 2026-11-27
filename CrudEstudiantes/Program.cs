var estudiantes = new List<Estudiante>();   // aquí se guardan en memoria
var siguienteId = 1;
var salir = false;

while (!salir)   // el menú se repite hasta elegir salir
{
    Console.WriteLine("\n=== Gestión de Estudiantes ===");
    Console.WriteLine("1. Agregar estudiante");
    Console.WriteLine("2. Listar estudiantes");
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
        case "0":
            salir = true;
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
string Leer(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine()?.Trim() ?? "";
}
