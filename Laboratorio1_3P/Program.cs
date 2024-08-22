using Laboratorio1_3P.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Imprimir los datos de la base de datos en la consola
PrintClientes();

void PrintClientes()
{
    ClientSqlDataAccessLayer dataAccess = new ClientSqlDataAccessLayer();
    dataAccess.PrintAllClientes();

    ClientPostgresDataAccessLayer dataAccessPostgres = new ClientPostgresDataAccessLayer();
    dataAccessPostgres.PrintAllClientes();

    ClientMySqlDataAccessLayer dataAccessMySql = new ClientMySqlDataAccessLayer();
    dataAccessMySql.PrintAllClientes();

    ClienteSqlPruebaAccessLayer dataSqlPrueba = new ClienteSqlPruebaAccessLayer();
    dataSqlPrueba.PrintAllProducts();

}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
