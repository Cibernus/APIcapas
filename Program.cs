using Areyes.BaseDedatos;
using AReyes.Services.Implementations;
using AReyes.Services.Interfaces;
using AReyes.Repositories.Implementations;
using AReyes.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddDbContext<AbarrotesReyesContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnectionString"))

.LogTo(Console.WriteLine, LogLevel.Information) 
           .EnableSensitiveDataLogging()

);
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProveedorService, ProveedorService>();
builder.Services.AddScoped<IProveedorRepository, ProveedorRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IImagenService, ImagenService>();
builder.Services.AddHttpContextAccessor();




builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();
app.UseCors();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
app.Run();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();

