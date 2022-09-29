using Microsoft.EntityFrameworkCore;
using DockerMonitor.Domain.Interfaces;
using DockerMonitor.Infastructure;
using DockerMonitor.Services;
using DockerMonitor.Services.Abstractions;
using DockerMonitor.Infastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddScoped<IDockerInformation, DockerInformation>();
builder.Services.AddScoped<IDockerService, DockerService>();
builder.Services.AddScoped<IContainerRepository, ContainerRepository>();
builder.Services.AddHostedService<ContainerStatsService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContextPool<RepositoryContext>(contextBuilder =>
{
    // ConnectionString

    var connectionString = builder.Configuration.GetConnectionString("DockerStatsContext");
    var serverVersion= ServerVersion.AutoDetect(connectionString);
    contextBuilder.UseMySql(connectionString, serverVersion);
});

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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.GetRequiredService<RepositoryContext>().Database.MigrateAsync();
}

app.Run();
