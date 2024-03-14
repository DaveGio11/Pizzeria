using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pizzeria.Models
{
    public partial class ModeldbContext : DbContext
    {
        public ModeldbContext()
            : base("name=ModeldbContext")
        {
        }

        public virtual DbSet<Bibite> Bibite { get; set; }
        public virtual DbSet<Ordini> Ordini { get; set; }
        public virtual DbSet<Pizze> Pizze { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bibite>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Bibite>()
                .Property(e => e.Prezzo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Bibite>()
                .HasMany(e => e.Ordini)
                .WithOptional(e => e.Bibite)
                .HasForeignKey(e => e.FK_ID_Bibita);

            modelBuilder.Entity<Ordini>()
                .Property(e => e.Totale)
                .HasPrecision(4, 2);

            modelBuilder.Entity<Pizze>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Pizze>()
                .Property(e => e.Prezzo)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Pizze>()
                .HasMany(e => e.Ordini)
                .WithRequired(e => e.Pizze)
                .HasForeignKey(e => e.FK_ID_Pizza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.Ruolo)
                .IsFixedLength();

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Ordini)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.FK_ID_Utente)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Ordini1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.FK_ID_Utente)
                .WillCascadeOnDelete(false);
        }
    }
}
