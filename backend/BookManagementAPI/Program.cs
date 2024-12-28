using MongoDB.Driver;
using BookManagementAPI.Controllers;
using BookManagementAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS policy setup
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Allow Angular frontend
              .AllowAnyHeader() // Allow all headers
              .AllowAnyMethod(); // Allow all HTTP methods
    });
});

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

// Apply the CORS policy
app.UseCors("AllowAngularApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
