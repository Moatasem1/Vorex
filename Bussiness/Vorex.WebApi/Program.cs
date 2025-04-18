using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;
using Vorex.Application.Users.Commands;
using Vorex.Domain.Interfaces;
using Vorex.Infrastructure.Persistence;
using Vorex.Infrastructure.Persistence.Repositories;
using Vorex.Application;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddMediatR(
    cfg =>
    {
        cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        cfg.RegisterServicesFromAssemblies(typeof(Vorex.Application.AssemblyReference).Assembly);
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
