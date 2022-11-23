using Server.Model;
using Microsoft.EntityFrameworkCore;
using Server.Contexts;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.SetIsOriginAllowed(origin => true);
                      });
});
builder.Services.AddControllersWithViews();
builder.Services.AddCors();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
MySqlServerVersion serverVersion = new MySqlServerVersion(new System.Version(8, 0, 25));

DbContextOptions<DatabaseContext> mySqlDbContextOptions = new DbContextOptionsBuilder<DatabaseContext>().UseMySql(connectionString, serverVersion).Options;

DatabaseContext mySqlDBContext = new DatabaseContext(mySqlDbContextOptions);
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
}
);

var app = builder.Build();

new ClientService(mySqlDBContext).SetTimer();

app.UseCors(MyAllowSpecificOrigins);

System.Environment.SetEnvironmentVariable("SYNC", "0");
System.Environment.SetEnvironmentVariable("SYNCTEMP", "25");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
