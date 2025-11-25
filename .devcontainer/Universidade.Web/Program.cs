// Imports necessários para conectar nossas camadas
using Microsoft.EntityFrameworkCore;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. REGISTRAR DEPENDÊNCIAS (IoC - Injeção de Dependência)
// Aqui "ensinamos" ao programa qual classe usar quando alguém pedir uma interface.

// Registros de Curso
builder.Services.AddScoped<ICursoAppService, CursoAppService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();

// Registros de Disciplina (ADICIONADO AGORA)
builder.Services.AddScoped<IDisciplinaAppService, DisciplinaAppService>();
builder.Services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();


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

// Rota padrão: Se não digitar nada, vai para Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();