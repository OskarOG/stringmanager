using Microsoft.EntityFrameworkCore;
using StringManager.Application.Persistence;
using StringManager.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IUnitOfWork, StringManagerDbContext>(options =>
    options.UseSqlServer("name=ConnectionStrings:StringManagerDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();


/// <summary>
/// This is required so that the StringManagerWebApiFactory is able to access and use Program as startup class.
/// </summary>
public partial class Program
{
}