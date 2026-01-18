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

builder.Services.AddScoped<GrupoTrabajoEstado>();
builder.Services.AddScoped<ISecurityProxy, SecurityProxy>();
builder.Services.AddScoped<ITecnicoProxy, TecnicoProxy>();
builder.Services.AddScoped<IGrupoTrabajoProxy, GrupoTrabajoProxy>();
builder.Services.AddScoped<IMarcaProxy, MarcaProxy>();
builder.Services.AddScoped<IModeloProxy, ModeloProxy>();
builder.Services.AddScoped<IMaquinaProxy, MaquinaProxy>();
builder.Services.AddScoped<IRutaProxy, RutaProxy>();
builder.Services.AddScoped<ITareaProxy, TareaProxy>();
builder.Services.AddScoped<ITipoActividadProxy, TipoActividadProxy>();
builder.Services.AddScoped<ITipoDemoraProxy, TipoDemoraProxy>();
builder.Services.AddScoped<ITipoHallazgoProxy, TipoHallazgoProxy>();
builder.Services.AddScoped<ITipoPmProxy, TipoPmProxy>();
builder.Services.AddScoped<ICheckListProxy, CheckListProxy>();
builder.Services.AddScoped<IPmProxy, PmProxy>();
builder.Services.AddScoped<IPmTareaTecnicoProxy, PmTareaTecnicoProxy>();
builder.Services.AddScoped<IPmTareaHallazgoProxy, PmTareaHallazgoProxy>();


builder.Services.AddBlazoredSessionStorage();
builder.Services.AddSweetAlert2();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();

builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
