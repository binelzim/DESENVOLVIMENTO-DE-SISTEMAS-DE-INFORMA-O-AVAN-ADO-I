// Imports necessários para conectar nossas camadas
using Universidade.Application.Interfaces;
using Universidade.Application.Services;
using Universidade.Domain.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// --- Início da Configuração ---

// 1. ADICIONAR CONTEXTO DO BANCO (Conecta a Fase 4)
// Lê a "ConnectionStrings" do appsettings.json que você acabou de editar.
builder.Services.AddDbContext<AppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. REGISTRAR DEPENDÊNCIAS (IoC) (Conecta a Fase 3 e 4)
// Isso diz ao sistema como criar os objetos quando eles forem solicitados.
// Usamos AddScoped, que significa "crie uma instância por requisição HTTP".

// Quando um Controller pedir 'ICursoAppService'...
builder.Services.AddScoped<ICursoAppService, CursoAppService>();

// Quando o 'CursoAppService' pedir 'ICursoRepository'...
builder.Services.AddScoped<ICursoRepository, ICursoRepository>();


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