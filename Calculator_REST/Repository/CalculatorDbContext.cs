using Calculator_REST.Entity;
using Microsoft.EntityFrameworkCore;

namespace Calculator_REST.Repository;

public class CalculatorDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CalculatorDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseNpgsql(_configuration.GetConnectionString("NpgsqlConnection"));
    }

    public DbSet<OperationEntity>? OperationEntities { get; set; }
    public DbSet<ExceptionEntity>? ExceptionEntities { get; set; }
}