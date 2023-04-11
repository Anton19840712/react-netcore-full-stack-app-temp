// Import the required namespaces
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

// ...

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
services.AddControllersWithViews();

// Access configuration values
var config = builder.Configuration;
var staticFilesDirectory = config.GetValue<string>("StaticFilesDirectory");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

app.UseHttpsRedirection();

// Use the configuration value to set the file provider
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
		Path.Combine(app.Environment.ContentRootPath, staticFilesDirectory)
	)
});

app.UseRouting();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();