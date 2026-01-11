using REST_GestionUsuarios.Application.Interfaces;
using REST_GestionUsuarios.Infrastructure.Data;
using REST_GestionUsuarios.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IDbConnectionFactory, OracleConnectionFactory>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.AllowAnyOrigin()   
              .AllowAnyHeader()   
              .AllowAnyMethod(); 
    });
});

builder.Services.AddControllers();

// Configuración de Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Esto asegura que la ruta sea /swagger
});

app.UseHttpsRedirection();

app.UseCors("AllowAngular");

app.UseAuthorization();
app.MapControllers();


app.Run();