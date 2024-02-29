using cumples.DataModel.Entity;
using cumples.Services.Interfaces;
using cumples.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using cumples.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CumplesContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"))
);

//Evitar redundancia ciclica
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

//Declaración de Servicios e interfases

builder.Services.AddScoped<ILogServices, LogServices>();

builder.Services.AddScoped<IGenderServices, GenderServices>();

builder.Services.AddScoped<IPersonsServices, PersonsServices>();

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
EmailService._config = builder.Configuration;

builder.Services.AddScoped<IBirthdayServices, BirthdayServices>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Configuración del Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "cumples.API",
        Version = "v1",
        Description = "Aplicación administración de cumpleaños",
        Contact = new OpenApiContact
        {
            Name = "CSC - Gcia. Sistemas - AAM",
            Email = "DESARROLLO@irsa.com.ar"
        }
    });
});

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
