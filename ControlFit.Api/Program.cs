using ControlFit.Application.CasosUso.Auditoria;
using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.CasosUso.CRUDMembresia;
using ControlFit.Application.CasosUso.CRUDMiembro;
using ControlFit.Application.CasosUso.CRUDAsignacion;
using ControlFit.Application.CasosUso.Ingreso;
using ControlFit.Application.CasosUso.Configuracion;
using ControlFit.Application.CasosUso.Dashboard;
using ControlFit.Application.CasosUso.Seed;
using ControlFit.Application.Repository;
using ControlFit.Application.Servicios;
using ControlFit.Domain.Interfaz_puertos_;
using ControlFit.Infrastructure.Persistencia;
using ControlFit.Infrastructure.Repository;
using ControlFit.Infrastructure.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:4200" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var jwtKey = builder.Configuration["Jwt:Key"]
    ?? Environment.GetEnvironmentVariable("JWT__KEY");

if (string.IsNullOrWhiteSpace(jwtKey))
{
    if (builder.Environment.IsDevelopment())
        jwtKey = "12345678901234567890123456789012";
    else
        throw new InvalidOperationException("Jwt:Key must be configured via appsettings or JWT__KEY environment variable.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            RoleClaimType = System.Security.Claims.ClaimTypes.Role
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SuperAdmin", policy =>
        policy.RequireRole("Super Admin"));
    options.AddPolicy("GymAdmin", policy =>
        policy.RequireRole("Admin Gimnasio"));
    options.AddPolicy("AnyAdmin", policy =>
        policy.RequireRole("Super Admin", "Admin Gimnasio"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<RegistrarAdministrador>();
builder.Services.AddScoped<LoginAdministrador>();
builder.Services.AddScoped<RefreshTokenAdministrador>();
builder.Services.AddScoped<LogoutAdministrador>();
builder.Services.AddScoped<IMiembroRepository, MiembroRepository>();
builder.Services.AddScoped<MiembroService>();
builder.Services.AddScoped<IGimnasioRepository, GimnasioRepository>();
builder.Services.AddScoped<GimnasioService>();
builder.Services.AddScoped<IMembresiaRepository, MembresiaRepository>();
builder.Services.AddScoped<MembresiaService>();
builder.Services.AddScoped<IAsignacionMembresiaRepository, AsignacionMembresiaRepository>();
builder.Services.AddScoped<AsignacionMembresiaService>();
builder.Services.AddScoped<IAsistenciaRepository, AsistenciaRepository>();
builder.Services.AddScoped<RegistrarIngreso>();
builder.Services.AddScoped<PreviewIngreso>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
builder.Services.AddScoped<IAuditLogRepository, AuditLogRepository>();
builder.Services.AddScoped<AuditService>();
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<IConfiguracionPlataformaRepository, ConfiguracionPlataformaRepository>();
builder.Services.AddScoped<ConfiguracionPlataformaService>();
builder.Services.AddScoped<ConsultarAuditoria>();
builder.Services.AddScoped<SeedDemoDataService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await db.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("Startup");
        logger.LogError(ex,
            "No se pudo conectar a SQL Server. Inicia la base de datos con: cd ControFit && docker compose up -d sqlserver");
        throw new InvalidOperationException(
            "SQL Server no disponible en localhost:1433. Ejecuta: cd ControFit && docker compose up -d sqlserver " +
            "(espera ~30s) y vuelve a correr dotnet run.", ex);
    }
}

await app.RunAsync();

namespace ControlFit.Api
{
    public partial class Program { }
}
