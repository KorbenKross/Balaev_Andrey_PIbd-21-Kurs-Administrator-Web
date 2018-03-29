namespace Kurs
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AutoCenterDB : DbContext
    {
        public AutoCenterDB()
            : base("name=AutoCenterDB")
        {
        }

        public virtual DbSet<Administrator> Administrator { get; set; }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Car_kit> Car_kit { get; set; }
        public virtual DbSet<Detail> Detail { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.login)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .Property(e => e.password)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.Car)
                .WithRequired(e => e.Administrator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Administrator>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Administrator)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.brand)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Car_kit>()
                .Property(e => e.kit_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Car_kit>()
                .HasMany(e => e.Car)
                .WithOptional(e => e.Car_kit1)
                .HasForeignKey(e => e.car_kit);

            modelBuilder.Entity<Car_kit>()
                .HasMany(e => e.Detail)
                .WithRequired(e => e.Car_kit)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Detail>()
                .Property(e => e.detail_name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Detail>()
                .Property(e => e.type)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.order_status)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Car)
                .WithRequired(e => e.Stock1)
                .HasForeignKey(e => e.stock)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Supplier2)
                .WithRequired(e => e.Stock1)
                .HasForeignKey(e => e.Stockstock_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Stock>()
                .HasMany(e => e.Detail)
                .WithMany(e => e.Stock)
                .Map(m => m.ToTable("Stock_Detail").MapLeftKey("Stockstock_id").MapRightKey("Detaildetail_id"));

            modelBuilder.Entity<Supplier>()
                .Property(e => e.name)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Car_kit)
                .WithRequired(e => e.Supplier1)
                .HasForeignKey(e => e.supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Stock)
                .WithRequired(e => e.Supplier1)
                .HasForeignKey(e => e.supplier)
                .WillCascadeOnDelete(false);
        }
    }
}
