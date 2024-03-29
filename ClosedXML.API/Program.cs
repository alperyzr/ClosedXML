using ClosedXML.API.Repositories.Interfaces;
using ClosedXML.API.Repositories;
using ClosedXML.API.Services;
using ClosedXML.API.Services.Interfaces;
using ClosedXML.API.UnitOfWorks;
using ClosedXML.API;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(_IRepository<>), typeof(_Repository<>));

builder.Services.AddScoped<ICovidRepository,CovidRepository>();
builder.Services.AddScoped<ICovidService, CovidService>();

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Data Source=.;Initial Catalog=SignalRDb;User ID=sa;Password=0123456789aA.;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


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
