using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApiDbConnector.Models;

namespace WebApiDbConnector.Context;

public partial class TripsContext : DbContext
{
    public TripsContext()
    {
    }

    public TripsContext(DbContextOptions<TripsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Employe> Employes { get; set; }

    public virtual DbSet<Manager> Managers { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    public virtual DbSet<Triptype> Triptypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("TripsData"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("company_pkey");

            entity.ToTable("company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("company_name");
        });

        modelBuilder.Entity<Employe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("employes_pkey");

            entity.ToTable("employes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("lastname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("patronymic");
        });

        modelBuilder.Entity<Manager>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("managers_pkey");

            entity.ToTable("managers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("lastname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(30)
                .IsFixedLength()
                .HasColumnName("patronymic");
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("trips_pkey");

            entity.ToTable("trips");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Caption)
                .HasMaxLength(500)
                .IsFixedLength()
                .HasColumnName("caption");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.ContractDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("contract_date");
            entity.Property(e => e.Customer)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasColumnName("customer");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(200)
                .IsFixedLength()
                .HasColumnName("customer_address");
            entity.Property(e => e.DeadlineContract)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline_contract");
            entity.Property(e => e.EmployeId).HasColumnName("employe_id");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.TripDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("trip_date");
            entity.Property(e => e.TripTypeId).HasColumnName("trip_type_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Trips)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("trips_company_id_fkey");

            entity.HasOne(d => d.Employe).WithMany(p => p.Trips)
                .HasForeignKey(d => d.EmployeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("trips_employe_id_fkey");

            entity.HasOne(d => d.Manager).WithMany(p => p.Trips)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("trips_manager_id_fkey");

            entity.HasOne(d => d.TripType).WithMany(p => p.Trips)
                .HasForeignKey(d => d.TripTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("trips_trip_type_id_fkey");
        });

        modelBuilder.Entity<Triptype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("triptype_pkey");

            entity.ToTable("triptype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Typecontract)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("typecontract");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
