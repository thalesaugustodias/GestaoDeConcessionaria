using GestaoDeConcessionaria.Infrastructure.ApiConfigurations;
using GestaoDeConcessionaria.Infrastructure.Context;
using GestaoDeConcessionaria.IoC;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ResolveDependencies();

builder.Services.AddDbContext<AplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpClient();
builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DbSeeder.SeedAsync(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao semear o banco de dados: " + ex.Message);
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseApiConfig(app.Environment);
app.UseSwaggerConfig();
app.MapRazorPages();
app.Run();