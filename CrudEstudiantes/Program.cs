var estudiantes = new List<Estudiante>();   // aquí se guardan en memoria
var siguienteId = 1;
var salir = false;

while (!salir)   // el menú se repite hasta elegir salir
{
    Console.WriteLine("\n=== Gestión de Estudiantes ===");
    Console.WriteLine("1. Agregar estudiante");
    Console.WriteLine("0. Salir");
    Console.Write("Opción: ");

    switch (Console.ReadLine())
    {
        case "1":
            Agregar();
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

string Leer(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine()?.Trim() ?? "";
}
