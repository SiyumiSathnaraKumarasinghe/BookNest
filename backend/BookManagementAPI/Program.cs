using MongoDB.Driver;
using BookManagementAPI.Controllers;
using BookManagementAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Register MongoDB client service
var connectionString = builder.Configuration.GetConnectionString("MongoDb");
builder.Services.AddSingleton<IMongoClient>(serviceProvider =>
{
    return new MongoClient(connectionString);
});

// Register MongoDB service (if you have a service to handle MongoDB operations)
builder.Services.AddScoped<BooksController>(); // This registers your BooksController

// Register controllers
builder.Services.AddControllers();

// Add Swagger for API documentation (optional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
