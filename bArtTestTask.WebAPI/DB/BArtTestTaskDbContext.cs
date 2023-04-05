using bArtTestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace bArtTestTask.WebAPI.DB;

public class BArtTestTaskDbContext : DbContext
{
    public virtual DbSet<Incident> Incidents { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }

    public BArtTestTaskDbContext(DbContextOptions<BArtTestTaskDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Incident>(ib =>
        {
            ib.HasKey(i => i.Name);
            
            ib.Property(i => i.Name).HasColumnName("name")
                .HasColumnType("NVARCHAR(36)")
                .ValueGeneratedOnAdd();
            ib.Property(i => i.Description).HasColumnName("description").HasColumnType("NVARCHAR(255)");
            
            ib.HasMany(i => i.Accounts).WithOne(a => a.Incident);
            ib.Navigation(i => i.Accounts).AutoInclude();
        });

        modelBuilder.Entity<Account>(ab =>
        {
            ab.HasKey(a => a.Id);
            ab.HasIndex(a => a.Name).IsUnique();
            
            ab.Property(a => a.Id).HasColumnName("id").ValueGeneratedOnAdd();
            ab.Property(a => a.Name).HasColumnName("name").HasColumnType("NVARCHAR(50)");
            ab.Property<string>("IncidentName").HasColumnName("incident_name").HasColumnType("NVARCHAR(36)");
            
            ab.HasMany(a => a.Contacts).WithOne(c => c.Account);
            ab.Navigation(a => a.Contacts).AutoInclude();
            ab.Navigation(a => a.Incident).AutoInclude();
        });

        modelBuilder.Entity<Contact>(cb =>
        {
            cb.HasKey(c => c.Id);
            
            cb.HasIndex(c => c.Email).IsUnique();

            cb.Property(c => c.Id).HasColumnName("id").ValueGeneratedOnAdd();
            cb.Property(c => c.Email).HasColumnName("email").HasColumnType("NVARCHAR(50)");
            cb.Property(c => c.FirstName).HasColumnName("first_name").HasColumnType("NVARCHAR(50)");
            cb.Property(c => c.LastName).HasColumnName("last_name").HasColumnType("NVARCHAR(50)");
            cb.Property<Guid?>("AccountId").HasColumnName("account_id");
            
            cb.Navigation(c => c.Account).AutoInclude();
        });
        base.OnModelCreating(modelBuilder);
    }
}