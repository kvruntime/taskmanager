using TaskManager.ClientServer.Components;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();

var backendUrl = builder.Configuration["BACKEND_URL"] ?? throw new NullReferenceException("BACKEND_URL must be provided");

builder.Services.AddHttpClient("backendApi", client =>
{
    // client.BaseAddress = new Uri(backendUrl, UriKind.RelativeOrAbsolute);
    client.BaseAddress = new Uri(backendUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddInteractiveServerRenderMode();
// .AddAdditionalAssemblies(typeof(TaskManager.)._Imports);

app.Run();
