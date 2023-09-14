

using WebApiNet6CursoUdemy.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Aqu� se agregan las inyecciones de dependencias
//AddTransient: Los objetos creados por Transient son siempre diferentes, y cada servicio y cada controlador crea una instancia diferente.
//AddScoped: ScopedEl objeto creado es el mismo en la misma sesi�n de solicitud, y cada sesi�n diferente crea una instancia diferente.
//AddSingleton: Los objetos creados por Singleton son iguales en todas partes en todas las sesiones de solicitud.
builder.Services.AddScoped<IServicioEmpleado, ServicioEmpleado>();

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
