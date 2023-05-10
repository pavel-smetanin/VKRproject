using VKRproject.Modules;
using VKRproject.Models;
using VKRproject.Tools;

//DataUpdaterModule module = new DataUpdaterModule();
//await module.Run();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Analytic}/{action=Index}");

app.Run();

//TelegramModule module = new TelegramModule();
//module.Run();
/*
AnalyticModule module = new AnalyticModule();
Console.WriteLine($"{module.GeneralToursCount()}    {module.GeneralAvgPrice()}    {module.GeneralAvgNights()}");
Console.WriteLine($"{module.MaxPrice().Value} {module.MinPrice().Value}");
Console.WriteLine($"{module.MaxNightsCount().Value} {module.MinNightsCount().Value}");
var countries = module.ToursCountsByCountry();
foreach(var c in countries)
{
    Console.WriteLine($"{c.Key} {c.Value}");
}
var operators = module.ToursCountsByOperator();
foreach(var o in operators)
{
    Console.WriteLine($"{o.Key} {o.Value}");
}
Console.WriteLine($"{module.CountryWithMaxTours()}  {module.CityWithMaxTours()} {module.HotelWithMaxTours()}");
*/