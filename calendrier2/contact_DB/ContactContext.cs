using System;
using System.Collections.Generic;
using System.Configuration;
using calendrier2.service;
using Microsoft.EntityFrameworkCore;



namespace calendrier2.contact_DB;

public partial class ContactContext : DbContext
{
    public ContactContext()
    {
        BDD_modifier bDD_Modifier = new BDD_modifier();
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

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string host = ConfigurationManager.AppSettings["host"];
            string port = ConfigurationManager.AppSettings["port"];
            string user = ConfigurationManager.AppSettings["user"];
            string password = ConfigurationManager.AppSettings["password"];
            string database = ConfigurationManager.AppSettings["database"];
            string mysqlVer = ConfigurationManager.AppSettings["mysqlVer"];
            

            string connectionString = $"server={host};port={port};user={user};password={password};database={database}";
            optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse(mysqlVer));
        }
    }


    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //         => optionsBuilder.UseMySql("server=mysql-clementvabre.alwaysdata.net;port=3306;user=352900;password=Clementvabre74;database=clementvabre_contact", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.17-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.IdContact).HasName("PRIMARY");

            entity.ToTable("contact");

            entity.Property(e => e.IdContact)
                .HasColumnType("int(11)")
                .HasColumnName("id_contact");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .HasColumnName("prenom");
            entity.Property(e => e.Status)
                .HasColumnType("enum('famille','ami','travail')")
                .HasColumnName("status");
            entity.Property(e => e.Tel)
                .HasMaxLength(30)
                .HasColumnName("tel");
        });

        modelBuilder.Entity<ContactReseauxSociaux>(entity =>
        {
            entity.HasKey(e => new { e.IdContact, e.IdReseau })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("contact_reseaux_sociaux");

            entity.HasIndex(e => e.IdReseau, "id_reseau");

            entity.Property(e => e.IdContact)
                .HasColumnType("int(11)")
                .HasColumnName("id_contact");
            entity.Property(e => e.IdReseau)
                .HasColumnType("int(11)")
                .HasColumnName("id_reseau");
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

            entity.Property(e => e.IdReseau)
                .HasColumnType("int(11)")
                .HasColumnName("id_reseau");
            entity.Property(e => e.NomReseau)
                .HasMaxLength(50)
                .HasColumnName("nom_reseau");
        });

        modelBuilder.Entity<Tache>(entity =>
        {
            entity.HasKey(e => e.Idtache).HasName("PRIMARY");

            entity.ToTable("tache");

            entity.HasIndex(e => e.TodolistIdtodolist, "fk_tache_todolist1_idx");

            entity.Property(e => e.Idtache)
                .HasColumnType("int(11)")
                .HasColumnName("idtache");
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.Fait).HasColumnName("fait");
            entity.Property(e => e.Lieux).HasMaxLength(40);
            entity.Property(e => e.Temps)
                .HasColumnType("time")
                .HasColumnName("temps");
            entity.Property(e => e.TodolistIdtodolist)
                .HasColumnType("int(11)")
                .HasColumnName("todolist_idtodolist");

            entity.HasOne(d => d.TodolistIdtodolistNavigation).WithMany(p => p.Taches)
                .HasForeignKey(d => d.TodolistIdtodolist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tache_todolist1");
        });

        modelBuilder.Entity<Todolist>(entity =>
        {
            entity.HasKey(e => e.Idtodolist).HasName("PRIMARY");

            entity.ToTable("todolist");

            entity.Property(e => e.Idtodolist)
                .HasColumnType("int(11)")
                .HasColumnName("idtodolist");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
