using Innkeep.Client.Data.Repositories.FileOperations;
using Innkeep.Client.Data.Repositories.Pretix;
using Innkeep.Core.Interfaces;
using Innkeep.Core.Interfaces.Pretix;
using Innkeep.Core.Interfaces.Repositories;
using Innkeep.Core.Interfaces.Services;
using Innkeep.DI;
using Innkeep.DI.Services;
using Serilog;

using var log =
    new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.Debug()
        .WriteTo.Trace()
        .MinimumLevel.Verbose()
        .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

DependencyManager.InitializeClient(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();