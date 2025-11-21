using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using DLTD.GestionPm.UI;
using DLTD.GestionPm.UI.Proxies.Implementaciones;
using DLTD.GestionPm.UI.Proxies.Interfaces;
using DLTD.GestionPm.UI.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration.GetValue<string>("services:urlBackend")!) });

builder.Services.AddScoped<ISecurityProxy, SecurityProxy>();
builder.Services.AddScoped<ITecnicoProxy, TecnicoProxy>();

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSweetAlert2();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
