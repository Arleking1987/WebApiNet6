

using WebApiNet6CursoUdemy;
using WebApiNet6CursoUdemy.Services;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;

// Add services to the container.
//Configuramos el acceso al conection string de la base de datos ubicado en el app.settings
var config = builder.Configuration;
var cadenaConexionSql = new ConexionBaseDatos(config.GetConnectionString("SQL"));
builder.Services.AddSingleton(cadenaConexionSql); //Con esto nos asegurmos en tener la cadena en todo el ambito de la aplicacion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Aquí se agregan las inyecciones de dependencias
//AddTransient: Los objetos creados por Transient son siempre diferentes, y cada servicio y cada controlador crea una instancia diferente.
//AddScoped: ScopedEl objeto creado es el mismo en la misma sesión de solicitud, y cada sesión diferente crea una instancia diferente.
//AddSingleton: Los objetos creados por Singleton son iguales en todas partes en todas las sesiones de solicitud.
builder.Services.AddScoped<IServicioEmpleado, ServicioEmpleado>();
builder.Services.AddScoped<IServicioEmpleadoSQL, ServicioEmpleadoSQL>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
