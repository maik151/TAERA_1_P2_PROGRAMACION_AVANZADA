using Taller_REST_API.Infraestructure;
using Taller_REST_API.Repository;
using Taller_REST_API.Services;

var builder = WebApplication.CreateBuilder(args);


var nombrePoliticaCors = "NuevaPolitica";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: nombrePoliticaCors, policy =>
    {
        policy.AllowAnyOrigin()  
              .AllowAnyHeader()  
              .AllowAnyMethod(); 
    });
});

builder.Services.AddSingleton<IDbConnectionFactory, OracleConnectionFactory>();
builder.Services.AddScoped<TestRepository>();
builder.Services.AddScoped<ActivoRepository>();
builder.Services.AddScoped<ActivoService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger"; // Esto asegura que la ruta sea /swagger
});

app.UseHttpsRedirection();


app.UseCors(nombrePoliticaCors);

app.UseAuthorization();

app.MapControllers();

app.Run();