var builder = WebApplication.CreateBuilder(args);

// Se agregan servicios al contenedor.

// Proveedor de registro, JSON Logging a la consola
builder.Logging.AddJsonConsole();

var app = builder.Build();

// Se configura un redireccionamiento HTTP.

app.UseHttpsRedirection();

// Lectura del entorno

if (app.Environment.IsDevelopment())
{
    Console.WriteLine($"Corriendo en modo desarrollo");
}

// Manejo de Logs

app.Logger.LogInformation("La aplicación ha iniciado.");

// Lectura de la configuración

var message = builder.Configuration["Message"] ?? "Hola no encontramos el parámetro";
app.MapGet("/message", () => message);

var items = new[]
{
    "item1", "itemn2", "item3", "item4", "item5"
};

app.MapGet("/todo", () =>
{
    return items;
});

// Gestión de solicitudes

app.MapGet("/", () => "Solicitud GET");
app.MapPost("/", () => "Solicitud POST");
app.MapPut("/", () => "Solicitud PUT");
app.MapDelete("/", () => "Solicitud DELETE");

app.MapMethods("/options-o-head", new[] { "OPTIONS", "HEAD" },
                          () => "Esta es una solicitud de opciones o cabeza");

// Controlador de ruta
// Lambda
app.MapGet("/lambda1", () => "Es una expresion lambda de una linea");


var handler = () => "Esta es una variable lambda";
app.MapGet("/lambda2", handler);

// Función local
string LocalFunction() => "Esto es una función local";

app.MapGet("/local-function", LocalFunction);

var helloHandler = new HelloHandler();
app.MapGet("/hello-handler", helloHandler.Hello);


// Parámetros de ruta

app.MapGet("/users/{userId}/books/{bookId}",
    (int userId, int bookId) => $"El ID de Usuario es: {userId} y el ID del libro es {bookId}");

// Restricción de rutas

app.MapGet("/todos/{id:int}", (int id) => $"ID {id}");
app.MapGet("/todos/{text}", (string text) => $"Texto {text}");
app.MapGet("/posts/{slug:regex(^[a-z0-9_-]+$)}", (string slug) => $"Post {slug}");


// Parámetros opcionales

app.MapGet("/products", (int? pageNumber) => $"Requesting page {pageNumber ?? 1}");

// 

app.Run();

class HelloHandler
{
    public string Hello()
    {
        return "Método Hello";
    }
}