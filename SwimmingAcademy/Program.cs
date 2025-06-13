using Microsoft.EntityFrameworkCore;
using SwimmingAcademy.Data;
using SwimmingAcademy.Helpers;
using SwimmingAcademy.Repositories;
using SwimmingAcademy.Repositories.Interfaces;
using SwimmingAcademy.Services;
using SwimmingAcademy.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SwimmingAcademyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ISwimmerRepository, SwimmerRepository>();
builder.Services.AddScoped<ISwimmerService, SwimmerService>();
builder.Services.AddScoped<ICoachService, CoachService>();
builder.Services.AddScoped<IPreTeamService, PreTeamService>();
builder.Services.AddScoped<ISchoolService, SchoolService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddControllers();
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
