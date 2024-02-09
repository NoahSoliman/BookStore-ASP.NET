using BookStore.Models;
using BookStore.Models.repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Design;

var builder = WebApplication.CreateBuilder(args);





//public StartUp ()
//{


//}
// Add services to the container.
builder.Services.AddOptions();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
builder.Services.AddSingleton<IBookStoreRepo<Author>, AuthorRepo>();
builder.Services.AddSingleton<IBookStoreRepo<Book>, BookRepo>();
builder.Services.AddDbContext<BookStoreDBContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("sqlCon"))

);


builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{

    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMvcWithDefaultRoute();

app.MapRazorPages();

app.Run();