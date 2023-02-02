using ClosedXML.API.Repositories.Interfaces;
using ClosedXML.API.Repositories;
using ClosedXML.API.Services;
using ClosedXML.API.Services.Interfaces;
using ClosedXML.API.UnitOfWorks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(_IRepository<>), typeof(_Repository<>));


builder.Services.AddScoped<ICovidService, CovidService>();
builder.Services.AddScoped<ICovidRepository, CovidRepository>();

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
