using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Bibliothèque.Entities
{
    public partial class BibliothequeContext : DbContext
    {
        public BibliothequeContext()
        {
        }

        public BibliothequeContext(DbContextOptions<BibliothequeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Auteur> Auteurs { get; set; }
        public virtual DbSet<AuteurLivre> AuteurLivres { get; set; }
        public virtual DbSet<Livre> Livres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=SONIC-12;Initial Catalog=Bibliotheque;Uid=sa;Pwd=formation");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "French_CI_AS");

            modelBuilder.Entity<Auteur>(entity =>
            {
                entity.ToTable("Auteur");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Prenom).HasMaxLength(150);

                entity.Property(e => e.Pseudonyme).HasMaxLength(150);
            });

            modelBuilder.Entity<AuteurLivre>(entity =>
            {
                entity.HasKey(e => new { e.IdLivre, e.IdAuteur })
                    .HasName("PK__AuteurLi__A1BBD8E03B18F4E3");

                entity.ToTable("AuteurLivre");

                entity.HasOne(d => d.IdAuteurNavigation)
                    .WithMany(p => p.AuteurLivres)
                    .HasForeignKey(d => d.IdAuteur)
                    .HasConstraintName("FK_AuteurLivre_Auteur");

                entity.HasOne(d => d.IdLivreNavigation)
                    .WithMany(p => p.AuteurLivres)
                    .HasForeignKey(d => d.IdLivre)
                    .HasConstraintName("FK_AuteurLivre_Livre");
            });

            modelBuilder.Entity<Livre>(entity =>
            {
                entity.ToTable("Livre");

                entity.Property(e => e.Editeur)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Isbn)
                    .HasMaxLength(50)
                    .HasColumnName("ISBN");

                entity.Property(e => e.Serie).HasMaxLength(150);

                entity.Property(e => e.Titre)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
