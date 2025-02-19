using CountryData.Standard;
using DotnetAuth.Domain.Entities;
using DotnetAuth.Exceptions;
using DotnetAuth.Extensions;
using DotnetAuth.Infrastructure.Context;
using DotnetAuth.Infrastructure.Mapping;
using DotnetAuth.Service;
using dotnetBlazorFullStuck.ApiService.Services.Abstract;
using dotnetBlazorFullStuck.ApiService.Services.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http; // Added for HttpContextAccessor

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor(); 

builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); 

// Adding Database context 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection"));
});

// Adding Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "User Auth", Version = "v1", Description = "Services to Authenticate user" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Please enter a valid token in the following format: {your token here} do not add the word 'Bearer' before it."
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
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// Adding Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Adding Services  
builder.Services.AddScoped<IUserServices, UserServiceImpl>();
builder.Services.AddScoped<ITokenService, ToekenServiceImple>();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<CountryHelper>();
builder.Services.AddScoped<ICountryDataServices, CountryDataServices>();

// Registering AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Adding Jwt from extension method
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.ConfigureCors();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseExceptionHandler(); 

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
    app.UseSwagger();
}

// Ensure proper middleware order
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.Run();
