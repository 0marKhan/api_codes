

using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data;

public class DataContextEntityFramework : DbContext{

    private string? _connectionString;

    public DataContextEntityFramework(IConfiguration config){
        _connectionString = config.GetConnectionString("DefaultConnection");
    }



    // matches computer model with computer table in the database
    // question mark checks if its available
    public DbSet<Computer>? Computer{get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured){
            optionsBuilder.UseSqlServer(_connectionString, 
            options=>options.EnableRetryOnFailure());
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){

        // we can use this to change the default table that entity is using
        modelBuilder.HasDefaultSchema("TutorialAppSchema");

        modelBuilder.Entity<Computer>().HasKey(c => c.ComputerId);
            // or we can use this to tell entity which table to use from which schema
            // .ToTable("Computer", "TutorialAppSchema");
            // .ToTable("Table name", "Schema name");
    }

}