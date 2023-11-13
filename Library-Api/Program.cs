using Application.Commands.Book;
using Application.Patterns;
using Application.UseCases.Author;
using Application.UseCases.Book;
using Application.UseCases.Category;
using Application.UseCases.Inventory;
using Application.UseCases.User;
using Infrastructure.Database;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.   

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(CreateBookCommandHandler)));
});


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<DbSession>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var dbSession = new DbSession(connectionString);
    return dbSession;
});


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBookWriteRepository, BookWriteRepository>();
builder.Services.AddScoped<IBookReadRepository, BookReadRepository>(); 
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IAuthorService,AuthorService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();

//builder.Logging.ClearProviders();
//XmlConfigurator.Configure(new FileInfo("log4net.config"));
//Environment.SetEnvironmentVariable("LogFileName", "app.log");


builder.Services.AddControllers();
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

//app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
