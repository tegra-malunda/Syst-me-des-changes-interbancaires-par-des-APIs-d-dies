using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace banque1.Models;

public partial class Banque1Context : DbContext
{
    public Banque1Context()
    {
    }

    public Banque1Context(DbContextOptions<Banque1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Compte> Comptes { get; set; }

    public virtual DbSet<Depot> Depots { get; set; }

    public virtual DbSet<Interbanque> Interbanques { get; set; }

    public virtual DbSet<Retrait> Retraits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Compte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("compte")
                .UseCollation("utf8mb4_unicode_ci");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Adresse)
                .HasMaxLength(300)
                .HasColumnName("adresse");
            entity.Property(e => e.CodePin)
                .HasMaxLength(4)
                .HasColumnName("code_pin");
            entity.Property(e => e.Devise)
                .HasColumnType("enum('CDF','USD')")
                .HasColumnName("devise");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(20)
                .HasColumnName("nom");
            entity.Property(e => e.NumeroCompte)
                .HasMaxLength(20)
                .HasColumnName("numero_compte");
            entity.Property(e => e.Postnom)
                .HasMaxLength(20)
                .HasColumnName("postnom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(20)
                .HasColumnName("prenom");
            entity.Property(e => e.Sexe)
                .HasColumnType("enum('M','F')")
                .HasColumnName("sexe");
            entity.Property(e => e.Telephone)
                .HasMaxLength(14)
                .HasColumnName("telephone");
        });

        modelBuilder.Entity<Depot>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("depot")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.FromInterbanque, "from_interbanque");

            entity.HasIndex(e => e.IdCompte, "id_compte");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("date");
            entity.Property(e => e.FromInterbanque)
                .HasMaxLength(100)
                .HasColumnName("from_interbanque");
            entity.Property(e => e.IdCompte)
                .HasMaxLength(100)
                .HasColumnName("id_compte");
            entity.Property(e => e.Montant)
                .HasPrecision(20, 4)
                .HasColumnName("montant");

            entity.HasOne(d => d.FromInterbanqueNavigation).WithMany(p => p.Depots)
                .HasForeignKey(d => d.FromInterbanque)
                .HasConstraintName("depot_ibfk_2");

            entity.HasOne(d => d.IdCompteNavigation).WithMany(p => p.Depots)
                .HasForeignKey(d => d.IdCompte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("depot_ibfk_1");
        });

        modelBuilder.Entity<Interbanque>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("interbanque")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Code, "code").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Token)
                .HasMaxLength(1000)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Retrait>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("retrait")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.FromInterbanque, "from_interbanque");

            entity.HasIndex(e => e.IdCompte, "id_compte");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("date");
            entity.Property(e => e.FromInterbanque)
                .HasMaxLength(100)
                .HasColumnName("from_interbanque");
            entity.Property(e => e.IdCompte)
                .HasMaxLength(100)
                .HasColumnName("id_compte");
            entity.Property(e => e.Montant)
                .HasPrecision(20, 4)
                .HasColumnName("montant");

            entity.HasOne(d => d.FromInterbanqueNavigation).WithMany(p => p.Retraits)
                .HasForeignKey(d => d.FromInterbanque)
                .HasConstraintName("retrait_ibfk_2");

            entity.HasOne(d => d.IdCompteNavigation).WithMany(p => p.Retraits)
                .HasForeignKey(d => d.IdCompte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("retrait_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
