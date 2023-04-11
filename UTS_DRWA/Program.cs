using Microsoft.EntityFrameworkCore;
using Guru.Models;
using Mapel.Models;
using JadwalGuru.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<GuruContext>(opt => opt.UseInMemoryDatabase("GuruItem"));
builder.Services.AddDbContext<MapelContext>(opt => opt.UseInMemoryDatabase("MapelItem"));
builder.Services.AddDbContext<JadwalGuruContext>(opt => opt.UseInMemoryDatabase("JadwalGuruItem"));
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
