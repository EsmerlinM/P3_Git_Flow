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
    Console.WriteLine("5. Buscar por nombre");
    Console.WriteLine("6. Total de estudiantes");
    Console.WriteLine("7. Filtrar por carrera");
    Console.WriteLine("8. Listar ordenado por nombre");            
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
        case "5":
            Buscar();
            break; 
        case "6":
            Total();
            break; 
        case "7":
            FiltrarPorCarrera();
            break;
        case "8":
            ListarOrdenado();
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
         Nombre = LeerObligatorio("Nombre: "),
        Matricula = LeerMatricula(),
        Carrera = LeerObligatorio("Carrera: ")
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
    if (matricula != "" && MatriculaValida(matricula, estudiante.Id)) estudiante.Matricula = matricula;

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

string LeerObligatorio(string mensaje)
{
    var valor = Leer(mensaje);
    while (valor == "")   // repite hasta que escriban algo
    {
        Console.WriteLine("Este campo es obligatorio.");
        valor = Leer(mensaje);
    }
    return valor;
}

bool MatriculaValida(string matricula, int idActual)
{
    // formato: 4 números, guion, 4 números
    if (!System.Text.RegularExpressions.Regex.IsMatch(matricula, @"^\d{4}-\d{4}$"))
    {
        Console.WriteLine("Formato inválido. Ejemplo: 2025-0912");
        return false;
    }
    // no puede repetirse en otro estudiante
    if (estudiantes.Any(e => e.Matricula == matricula && e.Id != idActual))
    {
        Console.WriteLine("Esa matrícula ya está registrada.");
        return false;
    }
    return true;
}

string LeerMatricula()
{
    var matricula = LeerObligatorio("Matrícula (0000-0000): ");
    while (!MatriculaValida(matricula, 0))
    {
        matricula = LeerObligatorio("Matrícula (0000-0000): ");
    }
    return matricula;
}
void Buscar()
{
    var texto = LeerObligatorio("Nombre a buscar: ");
    var encontrados = estudiantes.Where(s => s.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase)).ToList();
    if (encontrados.Count == 0) Console.WriteLine("No se encontraron estudiantes.");
    foreach (var item in encontrados)
    {
        Console.WriteLine($"{item.Id}. {item.Nombre} | {item.Matricula} | {item.Carrera}");
    }
}

void Total()
{
    Console.WriteLine($"Total de estudiantes: {estudiantes.Count}");
}

void FiltrarPorCarrera()
{
    var carrera = LeerObligatorio("Carrera: ");
    var encontrados = estudiantes.Where(s => s.Carrera.Equals(carrera, StringComparison.OrdinalIgnoreCase)).ToList();
    if (encontrados.Count == 0) Console.WriteLine("No hay estudiantes en esa carrera.");
    foreach (var item in encontrados)
    {
        Console.WriteLine($"{item.Id}. {item.Nombre} | {item.Matricula} | {item.Carrera}");
    }
}

void ListarOrdenado()
{
    if (estudiantes.Count == 0) Console.WriteLine("No hay estudiantes registrados.");
    foreach (var item in estudiantes.OrderBy(s => s.Nombre))   // ordena de A a Z
    {
        Console.WriteLine($"{item.Id}. {item.Nombre} | {item.Matricula} | {item.Carrera}");
    }
}
string Leer(string mensaje)
{
    Console.Write(mensaje);
    return Console.ReadLine()?.Trim() ?? "";
}
