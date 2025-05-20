using Microsoft.EntityFrameworkCore;
using SKWebChatBot.Data;
using SKWebChatBot.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<SemanticKernelService>();
builder.Services.AddControllersWithViews();

var connexion = builder.Configuration.GetConnectionString("SKWebChatBotBdtConnection");
builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SKWebChatBotBdtConnection"),
        o => o.CommandTimeout(300));
    o.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);


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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
