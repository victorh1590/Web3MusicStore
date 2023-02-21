using Microsoft.EntityFrameworkCore;
using Web3MusicStore.API;
using Web3MusicStore.API.Data;
using Web3MusicStore.API.Data.Repositories;
using Web3MusicStore.API.Data.UnitOfWork;
using Web3MusicStore.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IStoreDbContext, StoreDbContext>(options =>
    {
      var connectionString = builder.Configuration.GetConnectionString("StoreConnection");
      options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
          .LogTo(new SimpleLogger().Log);
    }
);

builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddTransient<IUnitOfWork, StoreUnitOfWork>();

builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

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
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
