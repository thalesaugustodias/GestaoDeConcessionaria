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

builder.Services.AddApiConfig();
builder.Services.AddSwaggerConfig();
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseApiConfig(app.Environment);

app.UseSwaggerConfig();

app.MapRazorPages();

app.Run();
