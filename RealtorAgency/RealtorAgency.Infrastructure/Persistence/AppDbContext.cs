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
            c.ToTable("clients");

            c.Property(c => c.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            c.Property(c => c.FullName)
                .HasColumnName("full_name")
                .IsRequired()
                .HasMaxLength(128);
            c.Property(c => c.PassportNumber)
                .HasColumnName("passport_number")
                .IsRequired()
                .HasMaxLength(20);
            c.Property(c => c.ContactPhone)
                .HasColumnName("contact_phone")
                .IsRequired()
                .HasMaxLength(20);
        });

        modelBuilder.Entity<Property>(p =>
        {
            p.HasKey(p => p.Id);
            p.ToTable("properties");

            p.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            p.Property(p => p.Type)
                .HasColumnName("type")
                .HasConversion<string>()
                .IsRequired();
            p.Property(p => p.Purpose)
                .HasColumnName("purpose")
                .HasConversion<string>()
                .IsRequired();
            p.Property(p => p.CadastralNumber)
                .HasColumnName("cadastral_number")
                .IsRequired()
                .HasMaxLength(50);
            p.Property(p => p.Address)
                .HasColumnName("address")
                .IsRequired()
                .HasMaxLength(256);
            p.Property(p => p.Floors)
                .HasColumnName("floors")
                .IsRequired();
            p.Property(p => p.TotalArea)
                .HasColumnName("total_area")
                .IsRequired()
                .HasPrecision(18, 2);
            p.Property(p => p.Rooms)
                .HasColumnName("rooms")
                .IsRequired();
            p.Property(p => p.CeilingHeight)
                .HasColumnName("ceiling_height")
                .IsRequired()
                .HasPrecision(18, 2);
            p.Property(p => p.FloorNumber)
                .HasColumnName("floor_number")
                .IsRequired();
            p.Property(p => p.HasEncumbrances)
                .HasColumnName("has_encumbrances")
                .IsRequired();
        });

        modelBuilder.Entity<Request>(r =>
        {
            r.HasKey(r => r.Id);
            r.ToTable("requests");

            r.Property(r => r.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();
            r.Property(r => r.ClientId)
                .HasColumnName("client_id")
                .IsRequired();
            r.Property(r => r.PropertyId)
                .HasColumnName("property_id")
                .IsRequired();
            r.Property(r => r.Type)
                .HasColumnName("type")
                .HasConversion<string>()
                .IsRequired();
            r.Property(r => r.Amount)
                .HasColumnName("amount")
                .IsRequired()
                .HasPrecision(18, 2);
            r.Property(r => r.Date)
                .HasColumnName("date")
                .IsRequired();

            r.HasOne(r => r.Client)
                .WithMany()
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            r.HasOne(r => r.Property)
                .WithMany()
                .HasForeignKey(r => r.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}