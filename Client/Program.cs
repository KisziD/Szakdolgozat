using Client.Services;
using Client.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<ValveService>();
var app = builder.Build();

IHostApplicationLifetime lifetime = app.Lifetime;

lifetime.ApplicationStarted.Register(ConnectionManager.Login);
// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
