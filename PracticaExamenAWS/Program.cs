using Amazon.S3;
using Microsoft.EntityFrameworkCore;
using PracticaExamenAWS.Data;
using PracticaExamenAWS.Repositories;
using PracticaExamenAWS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<RepositorySeries>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceBucketS3>();
builder.Services.AddDbContext<SeriesContext>(x => x.UseMySQL(builder.Configuration.GetConnectionString("MySQLAWS")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
