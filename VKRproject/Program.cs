using VKRproject.Modules;
using VKRproject.Tools;
/*
if (Convert.ToBoolean(ConfigProvider.Configuration["Update:Enable"]))
{
    DataUpdaterModule module = new DataUpdaterModule();
    await module.Run();
}*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Search/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Search}/{action=Index}");

app.Run();
/*
Filter f = new Filter();
f.CountryId = 40;
f.OperatorsId = new List<int>{ 9, 38, 54};
f.DateLower = "01.01.2014";
f.DateUpper = "01.01.2015";
f.PriceLower = 100;
f.PriceUpper = 100000;
f.MinNightsCount = 1;
f.NightsCount = 13;
f.AdultsCount = 2;
f.ChildCount = 0;
f.DepCityId = 832;
f.MealCode = "AI";
f.Category = "5*";
f.Rate = 1.0;
SearchModule module = new SearchModule();
var r = module.SearchToursByFilter(f);
foreach (var e in r)
    Console.WriteLine($"{e.ID}  {e.Name}    {e.City.Name}");*/

