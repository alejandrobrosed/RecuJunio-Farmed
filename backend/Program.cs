using farmed.Data;
using farmed.Repositories;
using farmed.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.DataProtection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);
});

//conexion a la bbdd
builder.Services.AddDbContext<FarmedContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

//habilitar controladores + json
builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    }
);

//session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

//configurar cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddAuthentication("Cookies").AddCookie("Cookies", options =>{
    options.LoginPath = "/forbidden";
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo("@/tmp/keys"));

//repositorios y servicios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IMedicamentoRepository, MedicamentoRepository>();
builder.Services.AddScoped<IMedicamentoService, MedicamentoService>();

builder.Services.AddScoped<ICompraRepository, CompraRepository>();
builder.Services.AddScoped<ICompraService, CompraService>();

builder.Services.AddScoped<IDetalleCompraRepository, DetalleCompraRepository>();
builder.Services.AddScoped<IDetalleCompraService, DetalleCompraService>();


//constuir la app
var app = builder.Build();

app.UseSession();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run("http://0.0.0.0:5000");