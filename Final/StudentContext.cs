using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class StudentContext : DbContext
{
    // This DbSet represents the collection of Student entities in the database
    public DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure the connection string to your local database
        // The database will be created for you if it doesn't exist
        optionsBuilder.UseSqlServer("Server=.;Database=StudentDataBase;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}