using DLTD.GestionPm.AccesoDatos.Configuracion;
using DLTD.GestionPm.AccesoDatos.Contexto;
using DLTD.GestionPm.Dto.Request.Email;
using DLTD.GestionPm.Dto.Request.Security;
using DLTD.GestionPm.Negocios.Implementaciones;
using DLTD.GestionPm.Negocios.Interfaces;
using DLTD.GestionPm.Repositorios.Implementaciones;
using DLTD.GestionPm.Repositorios.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Mapeo JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

//Mapeo EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

//Inyectar cadenas de conexion
builder.Services.AddDbContext<SeguridadBdContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SeguridadBD"));
});

builder.Services.AddDbContext<GestionPmBdContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GestionPmBD"));
});

//Configurar AspNetCore Identity
builder.Services.AddIdentity<SecurityEntity, IdentityRole>(policy =>
{ 
    policy.Password.RequireLowercase=true;
    policy.Password.RequireUppercase=true;
    policy.Password.RequireDigit=true;
    policy.Password.RequiredLength=8;
    policy.Password.RequireNonAlphanumeric=false;

    policy.User.RequireUniqueEmail=true;

    policy.Lockout.MaxFailedAccessAttempts=3;
    policy.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);
    policy.Lockout.AllowedForNewUsers=true;
})
    .AddEntityFrameworkStores<SeguridadBdContext>()
    .AddDefaultTokenProviders();

//Configurar Jwt 
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]!))
    };
});

//Inyeccion de Dependencias de los Servicios y repositorios
builder.Services.AddScoped<ISecurityService, SecurityService>();
builder.Services.AddScoped<ITecnicoService, TecnicoService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddScoped<ITecnicoRepository, TecnicoRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var service = app.Services.CreateScope())
{
    await SeedData.SeedAsync(service.ServiceProvider);
}

app.Run();
