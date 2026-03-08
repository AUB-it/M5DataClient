using DataClient.Components;
using DataClient.Services;
using DataClient.Services.Interfaces;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

var userConnectionString = Environment.GetEnvironmentVariable("USER_CONNECTION_STRING");
var catalogConnectionString = Environment.GetEnvironmentVariable("CATALOG_CONNECTION_STRING");

if (string.IsNullOrEmpty(userConnectionString) || string.IsNullOrEmpty(catalogConnectionString))
{
    Console.WriteLine("One or both connectionstrings missing");
}

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<ICatalogService, CatalogService>();

builder.Services.AddHttpClient<UserService>("RemoteUserService", client =>
{
// PORT-nr. herunder skal tilpasses senere
    client.BaseAddress = new Uri(userConnectionString);
});

builder.Services.AddHttpClient<CatalogService>("RemoteCatalogService", client =>
{
// PORT-nr. herunder skal tilpasses senere
    client.BaseAddress = new Uri(catalogConnectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
