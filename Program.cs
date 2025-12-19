using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

// Serves wwwroot of the app and static web assets from referenced projects
// app.MapStaticAssets();

// Middleware-based static files (still fine to keep for wwwroot)
app.UseStaticFiles();

app.MapGet("/", () => "Hello");

app.Run();