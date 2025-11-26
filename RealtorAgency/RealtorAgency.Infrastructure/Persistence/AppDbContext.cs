using Microsoft.EntityFrameworkCore;
using RealtorAgency.Domain.Entities;
namespace RealtorAgency.Infrastructure.Persistence;

/// <summary>
/// EF Core database context for the real estate agency application.
/// </summary>
/// <param name="options">The options for this context.</param>
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// <summary>
    /// DbSet of clients in the agency.
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// DbSet of properties managed by the agency.
    /// </summary>
    public DbSet<Property> Properties { get; set; }

    /// <summary>
    /// DbSet of requests, representing client requests to buy or sell properties.
    /// </summary>
    public DbSet<Request> Requests { get; set; }

    /// <summary>
    /// Configures the EF Core model.
    /// </summary>
    /// <param name="modelBuilder">Model builder instance.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(c =>
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.Id)
                .ValueGeneratedOnAdd();
            c.Property(c => c.FullName)
                .IsRequired()
                .HasMaxLength(128);
            c.Property(c => c.PassportNumber)
                .IsRequired()
                .HasMaxLength(20);
            c.Property(c => c.ContactPhone)
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Property>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Id)
                .ValueGeneratedOnAdd();
            p.Property(p => p.Type)
                .HasConversion<string>()
                .IsRequired();
            p.Property(p => p.Purpose)
                .HasConversion<string>()
                .IsRequired();
            p.Property(p => p.CadastralNumber)
                .IsRequired()
                .HasMaxLength(50);
            p.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(256);
            p.Property(p => p.Floors)
                .IsRequired();
            p.Property(p => p.TotalArea)
                .IsRequired()
                .HasPrecision(18, 2);
            p.Property(p => p.Rooms)
                .IsRequired();
            p.Property(p => p.CeilingHeight)
                .IsRequired()
                .HasPrecision(18, 2);
            p.Property(p => p.FloorNumber)
                .IsRequired();
            p.Property(p => p.HasEncumbrances)
                .IsRequired();
        });

        modelBuilder.Entity<Request>(r =>
        {
            r.HasKey(r => r.Id);
            r.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            r.Property(r => r.Date)
                .HasColumnName("date")
                .IsRequired();

            r.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey("ClientId")
                .OnDelete(DeleteBehavior.Cascade);
            r.HasOne(r => r.Property)
                .WithMany()
                .HasForeignKey("PropertyId")
                .OnDelete(DeleteBehavior.Cascade);
            r.Property(r => r.Type)
                .HasConversion<string>()
                .IsRequired();
            r.Property(r => r.Amount)
                .IsRequired()
                .HasPrecision(18, 2);
        });
    }
}
