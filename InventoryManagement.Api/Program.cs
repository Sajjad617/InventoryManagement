using InventoryManagement.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using InventoryManagement.Repository.RegisterService;
using InventoryManagement.Interface.ServiceInterface;
using InventoryManagement.Repository.Services;
using InventoryManagement.Repository.Extra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EFContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnection")));
builder.Services.AddInfrastructure();
builder.Services.AddScoped<Iauth, authService>();
builder.Services.AddScoped<Icategories, categoriesService>();
builder.Services.AddScoped<Iproducts, productService>();
builder.Services.AddScoped<IEncryptPassword, EncryptPasswordService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
