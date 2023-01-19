using System.Text.Json.Serialization;
using APICatalogo.Context;
using APICatalogo.Extensions;
using APICatalogo.Filters;
using APICatalogo.Logging;
using APICatalogo.Repository;
using APICatalogo.Services;
using Microsoft.EntityFrameworkCore;
using AppDbContext = APICatalogo.Context.AppDbContext;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions
        .ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration()
{
    LogLevel = LogLevel.Information
}));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ApiLoggingFilter>();

string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection"); //configuration

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlConnection, 
        ServerVersion.AutoDetect(mySqlConnection)));
builder.Services.AddTransient<IMeuServico, MeuServico>(); //tempo de vida do serviço

builder.Services.AddScoped<APICatalogo.Repository.IUnitOfWork, UnitOfWork>(); //cada request cria um novo escopo de serviço

var app = builder.Build();

app.ConfigureExcepetionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
