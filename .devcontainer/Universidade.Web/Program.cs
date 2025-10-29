// Imports necessários para conectar nossas camadas
using Microsoft.EntityFrameworkCore; // <-- ESTA LINHA FALTAVA
using Universidade.Application.Interfaces;
using Universidade.Application.Services;
using Universidade.Domain.Interfaces;
using Universidade.Infrastructure.Data;
using Universidade.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// --- Início da Configuração ---

// 1. ADICIONAR CONTEXTO DO BANCO (Conecta a Fase 4)
// Lê a "ConnectionStrings" do appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Esta linha agora vai funcionar

// 2. REGISTRAR DEPENDÊNCIAS (IoC)
builder.Services.AddScoped<ICursoAppService, CursoAppService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();


// 3. Adicionar serviços do MVC (padrão)
builder.Services.AddControllersWithViews();

// --- Fim da Configuração ---

var app = builder.Build();

// Configuração do pipeline HTTP (padrão)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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