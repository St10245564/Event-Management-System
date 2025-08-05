using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using MyPart3.Data;
using MyPart3.Services;
using MyPart3.Services.MyPart3.Services;
using MyPart3.Services.MyPart3.Services.MyPart3.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(x =>
    new BlobServiceClient(builder.Configuration.GetSection("AzureBlob")["ConnectionString"]));

builder.Services.AddScoped<IBlobService, BlobService>(); 



builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddSingleton<BlobService>();

var app = builder.Build();

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