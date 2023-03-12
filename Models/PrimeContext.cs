using Microsoft.EntityFrameworkCore;

namespace Models;
public class PrimeContext: DbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }
    public DbSet<Team> Teams { get; set; }
    public PrimeContext(DbContextOptions options): base(options) { }
}