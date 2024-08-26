using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Request> Requests { get; set; }

    public virtual DbSet<RequestStatus> RequestStatuses { get; set; }

    public virtual DbSet<RequestType> RequestTypes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UsersRole> UsersRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=TicketingSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Request>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Requests_Id");

            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.IdAdminNavigation).WithMany(p => p.RequestIdAdminNavigations)
                .HasForeignKey(d => d.IdAdmin)
                .HasConstraintName("FK_Requests_IdAdmin");

            entity.HasOne(d => d.IdManagerNavigation).WithMany(p => p.RequestIdManagerNavigations)
                .HasForeignKey(d => d.IdManager)
                .HasConstraintName("FK_Requests_IdManager");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.RequestIdUserNavigations)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_IdUser");

            entity.HasOne(d => d.RequestStatusNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestStatus)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Status");

            entity.HasOne(d => d.RequestTypeNavigation).WithMany(p => p.Requests)
                .HasForeignKey(d => d.RequestType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Requests_Type");
        });

        modelBuilder.Entity<RequestStatus>(entity =>
        {
            entity.ToTable("RequestStatus");

            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<RequestType>(entity =>
        {
            entity.ToTable("RequestType");

            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Users_ID");

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.JobTitle).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(24);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UserRole_ID");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleName).HasMaxLength(25);
        });

        modelBuilder.Entity<UsersRole>(entity =>
        {
            entity.HasKey(e => e.IdUsersRoles).HasName("PK_Users_Roles_ID");

            entity.ToTable("Users_Roles");

            entity.Property(e => e.IdUsersRoles).HasColumnName("Id_Users_Roles");

            entity.Property(e => e.IdUser).HasColumnName("IdUser");

            entity.Property(e => e.IdRole).HasColumnName("IdRole");

            entity.HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.UsersRoles)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User");

            entity.HasOne(d => d.IdRoleNavigation)
                .WithMany(p => p.UsersRoles)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Role");
        });



        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
