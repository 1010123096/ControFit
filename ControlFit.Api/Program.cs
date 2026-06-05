using ControlFit.Application.CasosUso.Auth;
using ControlFit.Application.CasosUso.CRUDGimnasio;
using ControlFit.Application.CasosUso.CRUDMembresia;
using ControlFit.Application.CasosUso.CRUDMiembro;
using ControlFit.Application.CasosUso.CRUDAsignacion;
using ControlFit.Application.CasosUso.Ingreso;
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



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
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

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();
// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserContextService, UserContextService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAdministradorRepository, AdministradorRepository>();
builder.Services.AddScoped<RegistrarAdministrador>();
builder.Services.AddScoped<LoginAdministrador>();
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

var app = builder.Build();


// Configure the HTTP request pipeline.
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

app.Run();

public partial class Program { }
