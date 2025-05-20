using Microsoft.EntityFrameworkCore;
using SKWebChatBot.Configuration;
using SKWebChatBot.Data;
using SKWebChatBot.Services;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("SKWebChatBotBdtConnection"),
        o => o.CommandTimeout(300));
    o.EnableSensitiveDataLogging();
}, ServiceLifetime.Transient);

builder.Services.Configure<SemanticKernelSettings>(builder.Configuration.GetSection("SemanticKernelSettings"));
builder.Services.AddSingleton<SemanticKernelService>();
builder.Services.AddTransient<ChatService>();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDistributedMemoryCache();


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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
