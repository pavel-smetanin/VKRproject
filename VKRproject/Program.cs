using VKRproject.Modules;
using VKRproject.Models;

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
    pattern: "{controller=Authorization}/{action=Index}");

app.Run();
/*
SearchModule search = new SearchModule();
var list = search.GetToursListByFilter(null);
Console.WriteLine("List of tours is created! Press <enter>");
Console.ReadKey();
EmailModule module = new EmailModule();
module.SendToursByEmail(list, "pavel.smetanin.01@mail.ru");
Console.WriteLine("Message is send");
Console.ReadKey();
SearchModule search = new SearchModule();
Filter filter = new Filter
{
    CountryId = 40,
    DateLower = DateOnly.Parse("01.01.2000"),
    DateUpper = DateOnly.Parse("01.01.2020"),
    NightsCount = 13,
    AdultsCount = 2,
    ChildCount = 0,
    PriceLower = 1000,
    PriceUpper = 100000
};
var list = search.GetToursListByFilter(filter);
foreach(var t in list)
{
    Console.WriteLine($"{t.Name} {t.TourOperator.Name}");
}
Console.ReadKey();*/

//DataUpdaterModule module = new DataUpdaterModule();
//module.Run();