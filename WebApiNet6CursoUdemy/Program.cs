using WebApiNet6CursoUdemy;
using WebApiNet6CursoUdemy.Services;
using NLog.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;

// Add services to the container.
//Configuramos el acceso al conection string de la base de datos ubicado en el app.settings
var config = builder.Configuration;
var cadenaConexionSql = new ConexionBaseDatos(config.GetConnectionString("SQL"));
builder.Services.AddSingleton(cadenaConexionSql); //Con esto nos asegurmos en tener la cadena en todo el ambito de la aplicacion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["JWT:Issuer"],
        ValidAudience = config["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:ClaveSecreta"]))
    };

});

builder.Services.AddControllers();
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiRest", Version= "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorizacion JWT esquema. \r\n\r\n Escribe 'Bearer' [espacio] y escribe el token proporcionado. \r\n\r\nExample: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Aquí se agregan las inyecciones de dependencias
//AddTransient: Los objetos creados por Transient son siempre diferentes, y cada servicio y cada controlador crea una instancia diferente.
//AddScoped: ScopedEl objeto creado es el mismo en la misma sesión de solicitud, y cada sesión diferente crea una instancia diferente.
//AddSingleton: Los objetos creados por Singleton son iguales en todas partes en todas las sesiones de solicitud.
builder.Services.AddScoped<IServicioEmpleado, ServicioEmpleado>();
builder.Services.AddScoped<IServicioEmpleadoSQL, ServicioEmpleadoSQL>();
builder.Services.AddScoped<IServicioUsuarioAPI, ServicioUsuarioAPI>();

//De esta forma agregamos la libreria NLog para ser usado desde la aplicación para hacer la inyección de dependencias
builder.Host.ConfigureLogging((hostingContext, loggin) =>
{
    loggin.AddNLog();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
