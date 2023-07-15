using Calculator_REST.Repository;
using Calculator_REST.Services;
using Calculator_REST.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add db context into dependency container
builder.Services.AddDbContext<CalculatorDbContext>();

// Add dependencies into dependency container
builder.Services.AddScoped<CalculatorService>();
// builder.Services.AddScoped<Expression>();
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

ConfigDb(app);

app.Run();

// Create db if not exists
void ConfigDb(IApplicationBuilder applicationBuilder)
{
    using var serviceScope = applicationBuilder.ApplicationServices
        .GetService<IServiceScopeFactory>()
        ?.CreateScope();

    var context = serviceScope
        ?.ServiceProvider
        .GetRequiredService<CalculatorDbContext>();
    context?.Database.EnsureCreated();
}