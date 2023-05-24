using VKRproject.Modules;
using VKRproject.Tools;

if (Convert.ToBoolean(ConfigProvider.Configuration["Update:Enable"]))
{
    DataUpdaterModule module = new DataUpdaterModule();
    await module.Run();
}

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
app.Run();


