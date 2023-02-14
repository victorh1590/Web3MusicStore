using Web3MusicStore.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StoreDbContext>(options =>
    {
      var connectionString = builder.Configuration.GetConnectionString("StoreConnection");
      options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
);
builder.Services.AddScoped<IRepository<Album>, AlbumRepository>();
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
