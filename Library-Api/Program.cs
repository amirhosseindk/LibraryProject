using Application.Patterns;
using Application.UseCases.Author;
using Application.UseCases.Book;
using Application.UseCases.Category;
using Application.UseCases.Inventory;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Library_Api;
using log4net.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton(new DbContext(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddScoped<IRepository<Inventory>, InventoryRepository>();
builder.Services.AddScoped<IRepository<Author>, AuthorRepository>();
builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Logging.ClearProviders();
XmlConfigurator.Configure(new FileInfo("log4net.config"));
Environment.SetEnvironmentVariable("LogFileName", "app.log");


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
