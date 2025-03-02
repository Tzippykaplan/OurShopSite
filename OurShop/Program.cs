using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using OurShop.MiddleWare;
using Repositories;
using Services;


var builder = WebApplication.CreateBuilder(args);
var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Error("NLog is working!");
builder.Host.UseNLog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductrepository, Productrepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
var connectionString = builder.Configuration.GetConnectionString("Home");
builder.Services.AddDbContext<ShopApiContext>(options => options.UseSqlServer(connectionString)
);
//builder.Host.UseNLog();
var app = builder.Build();
app.UseHandleErrorMiddleware();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
 
app.UseAuthorization();

app.UseRatingMiddleware();

app.MapControllers();

app.Run();
