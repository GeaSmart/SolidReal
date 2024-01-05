using Microsoft.EntityFrameworkCore;
using MySolidAPI;
using MySolidAPI.Entities;
using SolidReal.Factory;
using SolidReal.Logging;
using SolidReal.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//Registrar servicios
builder.Services.AddTransient<ILoggingFactory, LoggingFactory>();
builder.Services.AddTransient<IRepository<Tarea>,RepositoryTarea>();
builder.Services.AddTransient<IRepository<Usuario>,RepositoryUsuario>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
