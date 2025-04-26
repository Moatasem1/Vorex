using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Vorex.Application;
using Vorex.Application.Cryptos.Contracts.Request;
using Vorex.Application.options;
using Vorex.Application.services;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;
using Vorex.Infrastructure.Email;
using Vorex.Infrastructure.Persistence;
using Vorex.Infrastructure.Persistence.Repositories;
using Vorex.Infrastructure.Persistence.Repositories.interfaces;
using Vorex.WebApi.Controllers.abstraction;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"))
);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JwtOptions>(options =>
{
    builder.Configuration.GetSection("JwtOptions").Bind(options);
    options.SigningKey = Environment.GetEnvironmentVariable("JWT_KEY")!;
});
builder.Services.Configure<EmailConfig>(options =>
{
    builder.Configuration.GetSection("EmailConfig").Bind(options);
    options.EmailPassword = Environment.GetEnvironmentVariable("EmailPassword")!;
});


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IReadOnlyRepository<>), typeof(ReadOnlyRepository<>));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CurrentUserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEmailService, SmtpEmailService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<EmailTemplateBuilder>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<LoadOptionsValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AnalyzeCryptoRiskRequestValidator>();

builder.Services.AddMediatR(
    cfg =>
    {
        cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly);
        cfg.RegisterServicesFromAssemblies(typeof(Vorex.Application.AssemblyReference).Assembly);
    });


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new {
                Code = "VALUE_INVALID",
                Source = e.Key,
                Message = e.Value?.Errors.First().ErrorMessage
            }).ToList();

        var result = new
        {
            ResponseData = new { Errors = errors },
            ApiVersion = 1,
        };

        return new BadRequestObjectResult(result);
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
