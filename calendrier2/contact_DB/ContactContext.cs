using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace calendrier2.contact_DB;

public partial class ContactContext : DbContext
{
    public ContactContext()
    {
    }

    public ContactContext(DbContextOptions<ContactContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<ContactReseauxSociaux> ContactReseauxSociauxes { get; set; }

    public virtual DbSet<ReseauxSociauxList> ReseauxSociauxLists { get; set; }

    public virtual DbSet<Tache> Taches { get; set; }

    public virtual DbSet<Todolist> Todolists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=contact", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.2.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.IdContact).HasName("PRIMARY");

            entity.ToTable("contact");

            entity.Property(e => e.IdContact).HasColumnName("id_contact");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .HasColumnName("prenom");
        });

        modelBuilder.Entity<ContactReseauxSociaux>(entity =>
        {
            entity.HasKey(e => new { e.IdContact, e.IdReseau })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("contact_reseaux_sociaux");

            entity.HasIndex(e => e.IdReseau, "id_reseau");

            entity.Property(e => e.IdContact).HasColumnName("id_contact");
            entity.Property(e => e.IdReseau).HasColumnName("id_reseau");
            entity.Property(e => e.Pseudo)
                .HasMaxLength(50)
                .HasColumnName("pseudo");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.HasOne(d => d.IdContactNavigation).WithMany(p => p.ContactReseauxSociauxes)
                .HasForeignKey(d => d.IdContact)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_reseaux_sociaux_ibfk_1");

            entity.HasOne(d => d.IdReseauNavigation).WithMany(p => p.ContactReseauxSociauxes)
                .HasForeignKey(d => d.IdReseau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("contact_reseaux_sociaux_ibfk_2");
        });

        modelBuilder.Entity<ReseauxSociauxList>(entity =>
        {
            entity.HasKey(e => e.IdReseau).HasName("PRIMARY");

            entity.ToTable("reseaux_sociaux_list");

            entity.Property(e => e.IdReseau).HasColumnName("id_reseau");
            entity.Property(e => e.NomReseau)
                .HasMaxLength(50)
                .HasColumnName("nom_reseau");
        });

        modelBuilder.Entity<Tache>(entity =>
        {
            entity.HasKey(e => e.Idtache).HasName("PRIMARY");

            entity.ToTable("tache");

            entity.HasIndex(e => e.TodolistIdtodolist, "fk_tache_todolist1_idx");

            entity.Property(e => e.Idtache).HasColumnName("idtache");
            entity.Property(e => e.Fait).HasColumnName("fait");
            entity.Property(e => e.Temps)
                .HasColumnType("time")
                .HasColumnName("temps");
            entity.Property(e => e.TodolistIdtodolist).HasColumnName("todolist_idtodolist");

            entity.HasOne(d => d.TodolistIdtodolistNavigation).WithMany(p => p.Taches)
                .HasForeignKey(d => d.TodolistIdtodolist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tache_todolist1");
        });

        modelBuilder.Entity<Todolist>(entity =>
        {
            entity.HasKey(e => e.Idtodolist).HasName("PRIMARY");

            entity.ToTable("todolist");

            entity.Property(e => e.Idtodolist).HasColumnName("idtodolist");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
