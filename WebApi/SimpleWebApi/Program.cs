using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Infrastructure.Database;
using MediatR;
using System.Reflection;
using Serilog;
using FluentValidation;
using SimpleWebApi.Pipelines;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

ConfigureLogging(builder.Configuration);
builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AssemblyScanner.FindValidatorsInAssembly(Assembly.GetExecutingAssembly()).ForEach(pair =>
{
    builder.Services.AddScoped(typeof(IValidator), pair.ValidatorType);
    builder.Services.AddScoped(pair.InterfaceType, pair.ValidatorType);
});

builder.Services.AddMediatR(Assembly.GetExecutingAssembly())
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var connectionString = builder.Configuration.GetConnectionString("Main");
builder.Services.AddDbContext<SimpleWebApi.Infrastructure.Database.DatabaseContext>(settings =>
    settings.UseNpgsql(connectionString, sqlOptions => sqlOptions.CommandTimeout(300))
    .UseSnakeCaseNamingConvention()
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;
            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();
            var except = exceptionHandlerPathFeature?.Error?.InnerException;
            var mess = except?.Message;
            await context.Response.WriteAsync(mess??"unknow server exception");
            Log.Error("Server error {@Exception}", except);
        });
    });
}

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SimpleWebApi.Infrastructure.Database.DatabaseContext>();
    dbContext.Database.Migrate();
}

app.Run();


void ConfigureLogging(IConfigurationRoot configuration)
{
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .Enrich.WithEnvironmentName()
        .Enrich.WithProperty("ServiceName", "todolist-api")
        .WriteTo.Console()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}