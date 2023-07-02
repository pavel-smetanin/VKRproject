using VKRproject.Modules;
using VKRproject.Tools;

Console.WriteLine($"{DateTime.Now}: The aplication TravelAgency is launched!");
if (Convert.ToBoolean(ConfigProvider.Configuration["Update:Enable"]))
{
    DataUpdaterModule module = new DataUpdaterModule();
    await module.Run();
}

Console.WriteLine($"{DateTime.Now}: The Web server part of application is started!");
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Search/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authorization}/{action=Index}");
Console.WriteLine("Using the route: {controller: Authorization} {action: Index}");
Console.WriteLine("Web server asp.net log:");
app.Run();

Console.WriteLine($"{DateTime.Now}: The Web server part of application is finished!");
Console.WriteLine($"{DateTime.Now}: The application TravelAgency is completed!");
