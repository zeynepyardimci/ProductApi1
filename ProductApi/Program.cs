using Microsoft.EntityFrameworkCore;
using ProductApi.Data;
using ProductApi.DataStructures.LinkedList;
using ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ProductLinkedList>();
builder.Services.AddScoped<ProductDataInitializer>();
builder.Services.AddScoped<ProductService>();  

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
