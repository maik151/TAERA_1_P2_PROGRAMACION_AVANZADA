using REST_GestionUsuarios.Application.Interfaces;
using REST_GestionUsuarios.Infrastructure.Data;
using REST_GestionUsuarios.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICIOS ---
builder.Services.AddSingleton<IDbConnectionFactory, OracleConnectionFactory>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();

// --- CORS (Configuración permisiva para producción) ---
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// --- CONFIGURACIÓN ORACLE WALLET (RUTAS DINÁMICAS) ---
string walletPath = Path.Combine(Directory.GetCurrentDirectory(), "Infraestructure", "OracleWallet");
Oracle.ManagedDataAccess.Client.OracleConfiguration.TnsAdmin = walletPath;
Oracle.ManagedDataAccess.Client.OracleConfiguration.WalletLocation = walletPath;

var app = builder.Build();

// --- MIDDLEWARES ---
app.UseSwagger();
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = "swagger";
});

// Comentado para evitar errores 400 en Render
// app.UseHttpsRedirection(); 

app.UseCors("AllowAngular");

app.UseAuthorization();
app.MapControllers();

app.Run();