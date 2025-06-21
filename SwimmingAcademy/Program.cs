using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SwimmingAcademy.Data;
using SwimmingAcademy.Helpers;
using SwimmingAcademy.Interfaces;
using SwimmingAcademy.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsProduction()) // Or any condition for your Azure deployment
{
    // Get the Azure Key Vault URI from configuration (e.g., appsettings.json or Environment Variable)
    // For Production, it should come from an environment variable set in your Azure hosting environment.
    var keyVaultUri = builder.Configuration["KeyVaultUri"]; // e.g., "https://swimmingacademy-prod-kv.vault.azure.net/"

    if (!string.IsNullOrEmpty(keyVaultUri))
    {
        // Use DefaultAzureCredential to authenticate
        // This will automatically try Managed Identity, then AZ CLI, VS, etc.
        var secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

        // Add Azure Key Vault secrets as a configuration source
        builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

        // Optional: If you need to refresh secrets periodically
        // builder.Configuration.AddAzureKeyVault(secretClient, new KeyVaultSecretManager(),
        //     new AzureKeyVaultConfigurationOptions { ReloadInterval = TimeSpan.FromMinutes(5) });
    }
    else
    {
        // Log a warning if KeyVaultUri is not set in Production
        Console.WriteLine("Warning: KeyVaultUri is not set in production environment. Secrets will not be loaded from Azure Key Vault.");
    }
}
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});



builder.Services.AddDbContext<SwimmingAcademyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ISwimmerRepository, SwimmerRepository>();
builder.Services.AddScoped<ISchoolRepository, SchoolRepository>();
builder.Services.AddScoped<IPreTeamRepository, PreTeamRepository>();
builder.Services.AddScoped<ICoachRepository, CoachRepository>();
builder.Services.AddScoped<ILogger, Logger<Program>>();

// AutoMapper

builder.Services.AddControllers();
// Add JWT Authentication
//var jwtSettings = builder.Configuration.GetSection("Jwt");
//var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"];

if (string.IsNullOrEmpty(key))
{
    throw new InvalidOperationException("JWT Key is not configured properly in the application settings.");
}

var encodedKey = Encoding.UTF8.GetBytes(key);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(encodedKey)
    };
});
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddSingleton<JwtTokenGenerator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.UseExceptionHandler("/error"); // Already default in .NET 8 minimal hosting

app.MapControllers();

app.MapGet("/ping", () => "pong");

app.Run();
