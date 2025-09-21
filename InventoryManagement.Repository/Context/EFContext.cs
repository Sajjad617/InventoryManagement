using InventoryManagement.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Context
{
    public partial class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }
        public virtual DbSet<categoriesVM> Categories { get; set; }

        public virtual DbSet<ProductVM> Products { get; set; }

        public virtual DbSet<authVM> Auth { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-H90K7JR\\SQLEXPRESS;Database=Inventory;Trusted_Connection=True;TrustServerCertificate=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<categoriesVM>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Categori__3214EC072AB628D0");

                entity.HasIndex(e => e.Name, "UQ__Categori__737584F6BFDD36C3").IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.Name).HasMaxLength(200);
            });


            modelBuilder.Entity<ProductVM>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Products__3214EC075A8C95CE");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.Image).HasMaxLength(500);
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Stock).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<authVM>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0728D7942F");

                entity.HasIndex(e => e.Email, "UQ__Users__A9D1053456314AD8").IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getutcdate())");
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.Password).HasMaxLength(255);
                entity.Property(e => e.Username).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    }
}
