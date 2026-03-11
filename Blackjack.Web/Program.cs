using Blackjack.Game;
using Blackjack.Web.Hubs;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<GameManager>();
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serve static files from wwwroot
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapHub<BlackjackHub>("/blackjackhub");

if (app.Environment.IsDevelopment())
{
    // In development, Vite dev server handles the SPA on port 3000.
    // The .NET backend only needs to serve the API and SignalR hub.
    // Access the app at http://localhost:3000 (Vite proxies API/SignalR here).
    app.Logger.LogInformation("Development mode: Use Vite dev server at http://localhost:3000");
    app.Logger.LogInformation("Start Vite with: cd ClientApp && npm run dev");
}
else
{
    // In production, serve the pre-built SPA from ClientApp/dist
    var spaPath = Path.Combine(app.Environment.ContentRootPath, "ClientApp", "dist");
    if (Directory.Exists(spaPath))
    {
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(spaPath),
            RequestPath = ""
        });

        app.MapFallbackToFile("index.html", new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(spaPath)
        });
    }
}

app.Run();
