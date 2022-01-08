using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace GymModel1
{
    public partial class GymEntitiesModel1 : DbContext
    {
        public GymEntitiesModel1()
            : base("name=GymEntitiesModel1")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(e => e.Nume)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Prenume)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Customer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .Property(e => e.Nume1)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Prenume1)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Ziua)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Ora)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.IdCs)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Product)
                .WillCascadeOnDelete();
        }
    }
}
