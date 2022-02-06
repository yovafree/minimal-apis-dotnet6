var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/hello", (Func<string>)(() => "Hola Mundo"));

app.MapGet("/hello-async", async httpContext =>
{
    await httpContext.Response.WriteAsync("Hola mundo");
});

app.MapGet("/person", () => new Person("Alberto", "Sousa"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public record Person(string FirstName, string LastName);