using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorApplication.Data;
using BlazorApplication.HttpClient.IClientInterface;
using BlazorApplication.HttpClient.ServiceImpl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IPlayerService,PlayerService>();
builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped(
    sp => 
        new System.Net.Http.HttpClient { 
            BaseAddress = new Uri("https://localhost:7235") 
        }
);
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