using GradeInput_Advanced_Programming.Trading;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Register TradingContext as a scoped service
//Register it with app setttings connection string

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


if (string.IsNullOrWhiteSpace(connectionString) ||
    !connectionString.Contains("Password=", StringComparison.OrdinalIgnoreCase))
{
    builder.Services.AddDbContext<TradingContext>(options =>
        options.UseInMemoryDatabase("Trading"));
}
else
{
    builder.Services.AddDbContext<TradingContext>(options =>
        options.UseNpgsql(connectionString));
}
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UAppGrade}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
