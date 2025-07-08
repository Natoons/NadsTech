using NadsTech.Components;
using NadsTech.Data;
using NadsTech.Models;
using NadsTech.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DotNetEnv;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add MVC services
builder.Services.AddControllersWithViews();

// Configuration de la base de données
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuration d'Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
    
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Configuration des cookies d'authentification
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
    options.AccessDeniedPath = "/access-denied";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

// Configuration des services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IAnalysisService, OpenRouterAnalysisService>();
builder.Services.AddScoped<IDataSeedService, DataSeedService>();
builder.Services.AddScoped<IIdentityService, IdentityService>();
builder.Services.AddScoped<IArticleQaService, ArticleQaService>();
builder.Services.AddScoped<IOpenRouterService, OpenRouterAnalysisService>();
// Ajout du service KeywordTrendsService pour l'injection de dépendances
builder.Services.AddHttpClient<IKeywordTrendsService, KeywordTrendsService>();

// Configuration de SignalR pour les commentaires en temps réel
builder.Services.AddSignalR();

// Active les erreurs détaillées Blazor Server pour faciliter le debug des exceptions côté client et serveur.
builder.Services.Configure<Microsoft.AspNetCore.Components.Server.CircuitOptions>(options => { options.DetailedErrors = true; });

// Enregistrement du BackgroundService KeywordTrendsBackgroundService
builder.Services.AddHostedService<KeywordTrendsBackgroundService>();


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

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers(); // ← Ajouté pour exposer les routes API

// Map MVC routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Création de la base de données si elle n'existe pas
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    // Initialisation des rôles et de l'admin
    var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
    identityService.InitializeRolesAsync().GetAwaiter().GetResult();
    identityService.CreateDefaultAdminAsync().GetAwaiter().GetResult();
}

app.Run();
