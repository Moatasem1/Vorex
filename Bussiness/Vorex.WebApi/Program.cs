using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json;
using Vorex.Application;
using Vorex.Application.Cryptos.Contracts.Request;
using Vorex.Application.options;
using Vorex.Application.services;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.lib.Interfaces;
using Vorex.Domain.User;
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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vorex API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT token:"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

//for authentication
var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
jwtOptions!.SigningKey = Environment.GetEnvironmentVariable("JWT_KEY")!;

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
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IPasswordHashService,HashPasswordService>();
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

var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidAudience = jwtOptions.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
        ClockSkew = TimeSpan.Zero
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            // Prevent default behavior
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new
            {

                ResponseData = new
                {
                    Errors = new Error[]
                    {
                      Error.Unauthorized()
                    }
                },
                ApiVersion = "1.0",
            }, jsonSerializerOptions);

            return context.Response.WriteAsync(result);
        },

        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var result = JsonSerializer.Serialize(new
            {
                ResponseData = new
                {
                    Errors = new Error[]
                    {
                      Error.Forbidden()
                    }
                },
                ApiVersion = "1.0",

            },jsonSerializerOptions);

            return context.Response.WriteAsync(result);
        }
    };

});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins(jwtOptions.Audience!) 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
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
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
