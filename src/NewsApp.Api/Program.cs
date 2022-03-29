using MediatR;
using Microsoft.OpenApi.Models;
using NewsApp.Api.Filters;
using NewsApp.Api.Middleware;
using NewsApp.Api.ServiceExtensions;
using NewsApp.Manager;
using NewsApp.Manager.Abstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChannelManager, ChannelManager>();
builder.Services.AddScoped<ICategoryManager, CategoryManager>();
builder.Services.AddScoped<INewsManager, NewsManager>();
builder.Services.AddScoped<ITagManager, TagManager>();
builder.Services.AddScoped<IUserManager, UserManager>();
builder.Services.AddScoped<INewsImageManager, NewsImageManager>();
builder.Services.AddScoped<IChannelCategoryMapManager, ChannelCategoryMapManager>();
builder.Services.AddScoped<IUserInterestManager, UserInterestManager>();
builder.Services.AddScoped<INewsNewsTagMapManager, NewsNewsTagMapManager>();
builder.Services.AddScoped<ISearchHistoryManager, SearchHistoryManager>();
builder.Services.AddScoped<IJwtAuthManager, JwtAuthManager>();


// Register JWTToken
builder.Services.AddJWTToken();

// Register MongoDB
builder.Services.AddMongoDB();
//builder.Services.AddJwtBearer(builder);
// Register MediatR

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Register Redis
builder.Services.AddRedis();


// Configure the HTTP request pipeline.

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "News API",
        /// <summary></summary>
        Version = "v1",
        Description = "News Services",
        Contact = new OpenApiContact
        {
            Name = "Hakan Mirze",
            Email = "hakanmirze@gmail.com"
        }
    });
    c.OperationFilter<SwaggerHeaderParameterOperationFilter>();
    c.CustomSchemaIds(x => x.FullName);
});
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// For unit testing
public partial class Program { }