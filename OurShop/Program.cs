using Microsoft.EntityFrameworkCore;
using Repositories;
using Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();
builder.Services.AddDbContext<ShopContext>(options => options.UseSqlServer("Server=SRV2\\PUPILS;Database=327581567_Shop_api; Trusted_Connection=True; TrustServerCertificate=True")
);

var app = builder.Build();

// Configure the HTTP request pipeline.0000000000000000000000
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
