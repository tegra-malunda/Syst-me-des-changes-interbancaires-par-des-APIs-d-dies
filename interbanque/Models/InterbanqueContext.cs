using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace interbanque.Models;

public partial class InterbanqueContext : DbContext
{
    public InterbanqueContext()
    {
    }

    public InterbanqueContext(DbContextOptions<InterbanqueContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banque> Banques { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Compte> Comptes { get; set; }

    public virtual DbSet<Transfert> Transferts { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Banque>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("banque");

            entity.HasIndex(e => e.Code, "code").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .HasColumnName("code");
            entity.Property(e => e.EndpointDepot)
                .HasColumnType("text")
                .HasColumnName("endpoint_depot");
            entity.Property(e => e.EndpointRetrait)
                .HasColumnType("text")
                .HasColumnName("endpoint_retrait");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Token)
                .HasMaxLength(1000)
                .HasColumnName("token");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("client");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Motpasse)
                .HasMaxLength(20)
                .HasColumnName("motpasse");
            entity.Property(e => e.Nom)
                .HasMaxLength(20)
                .HasColumnName("nom");
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

        modelBuilder.Entity<Compte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("compte");

            entity.HasIndex(e => new { e.IdBanque, e.NumeroCompte }, "id_banque").IsUnique();

            entity.HasIndex(e => e.IdClient, "id_client");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Devise)
                .HasColumnType("enum('CDF','USD')")
                .HasColumnName("devise");
            entity.Property(e => e.IdBanque)
                .HasMaxLength(100)
                .HasColumnName("id_banque");
            entity.Property(e => e.IdClient)
                .HasMaxLength(100)
                .HasColumnName("id_client");
            entity.Property(e => e.NumeroCompte)
                .HasMaxLength(20)
                .HasColumnName("numero_compte");

            entity.HasOne(d => d.IdBanqueNavigation).WithMany(p => p.Comptes)
                .HasForeignKey(d => d.IdBanque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("compte_ibfk_2");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Comptes)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("compte_ibfk_1");
        });

        modelBuilder.Entity<Transfert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("transfert");

            entity.HasIndex(e => e.FromIdCompte, "from_id_compte");

            entity.HasIndex(e => e.ToBanque, "to_banque");

            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("timestamp")
                .HasColumnName("date");
            entity.Property(e => e.FromIdCompte)
                .HasMaxLength(100)
                .HasColumnName("from_id_compte");
            entity.Property(e => e.Montant)
                .HasPrecision(20, 4)
                .HasColumnName("montant");
            entity.Property(e => e.ToBanque)
                .HasMaxLength(100)
                .HasColumnName("to_banque");
            entity.Property(e => e.ToNumeroCompte)
                .HasMaxLength(20)
                .HasColumnName("to_numero_compte");

            entity.HasOne(d => d.FromIdCompteNavigation).WithMany(p => p.Transferts)
                .HasForeignKey(d => d.FromIdCompte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfert_ibfk_3");

            entity.HasOne(d => d.ToBanqueNavigation).WithMany(p => p.Transferts)
                .HasForeignKey(d => d.ToBanque)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("transfert_ibfk_4");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
