using Web3MusicStore.API.Models;
using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API.Data;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Data.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StoreDbContext>(options =>
    {
      var connectionString = builder.Configuration.GetConnectionString("StoreConnection");
      options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
);
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
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
