using EquipmentRental.API.Data;
using EquipmentRental.API.Mappings;
using EquipmentRental.API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Database Connections
builder.Services.AddDbContext<EquipmentRentalsDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("EquipmentRentalsConnectionString")));

// Customer Interface and Repository
builder.Services.AddScoped<ICustomerRepository, SQLCustomerRepository>();

// Equipment Rentals Interface and Repository
builder.Services.AddScoped<IEquipmentRepository, SQLEquipmentRepository>();

// Mapping Profiles
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


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
