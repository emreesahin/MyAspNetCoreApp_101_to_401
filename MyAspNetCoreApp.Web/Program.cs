using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetCoreApp.Web.Filters;
using MyAspNetCoreApp.Web.Helpers;
using MyAspNetCoreApp.Web.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDbContext>( options =>
{
    //dbContext.data butun veriyi getirecek EFCore
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon")); 
});


builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddScoped<IHelper, Helper>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<NotFoundFilter>();

var app = builder.Build();



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

//app.MapControllerRoute(
//    name: "pages",
//    pattern: "blog/{*articles}",
//    defaults: new { controller = "Blog", action = "Article" });


//app.MapControllerRoute(
//    name: "article",
//    pattern: "{controller=Blog}/{action=Article}/{name}/{id}");

//app.MapControllerRoute(
//    name: "pages",
//    pattern: "{controller}/{action}/{page}/{pagesize}");

//app.MapControllerRoute(
//    name: "productgetbyid",
//    pattern: "{controller}/{action}/{productid}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapControllers();

app.Run();



// View Component 1. Video
