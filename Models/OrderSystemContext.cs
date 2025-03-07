using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderSystem.Models;

public partial class OrderSystemContext : DbContext
{
    public OrderSystemContext()
    {
    }

    public OrderSystemContext(DbContextOptions<OrderSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<MealPriceTracking> MealPriceTrackings { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MenuPriceTracking> MenuPriceTrackings { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<RolesDetail> RolesDetails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=OrderSystem;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F37ABB4E2");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Meal__3213E83F7AEC1B0E");

            entity.ToTable("Meal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.MealImage)
                .HasMaxLength(999)
                .IsUnicode(false)
                .HasColumnName("meal_image");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Category).WithMany(p => p.Meals)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Meal__category_i__440B1D61");
        });

        modelBuilder.Entity<MealPriceTracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MealPric__3213E83F265551D1");

            entity.ToTable("MealPriceTracking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("date");
            entity.Property(e => e.MealId).HasColumnName("meal_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.Meal).WithMany(p => p.MealPriceTrackings)
                .HasForeignKey(d => d.MealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Meal_MenuPriceTracking");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menu__3213E83FD8723607");

            entity.ToTable("Menu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MenuImage)
                .HasMaxLength(999)
                .IsUnicode(false)
                .HasColumnName("menu_image");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasMany(d => d.Meals).WithMany(p => p.Menus)
                .UsingEntity<Dictionary<string, object>>(
                    "MenuContent",
                    r => r.HasOne<Meal>().WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Meal_MenuContent"),
                    l => l.HasOne<Menu>().WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Menu_MenuContent"),
                    j =>
                    {
                        j.HasKey("MenuId", "MealId").HasName("MenuContent_pk");
                        j.ToTable("MenuContent");
                        j.IndexerProperty<int>("MenuId").HasColumnName("menu_id");
                        j.IndexerProperty<int>("MealId").HasColumnName("meal_id");
                    });
        });

        modelBuilder.Entity<MenuPriceTracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MenuPric__3213E83F3FA556E5");

            entity.ToTable("MenuPriceTracking");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("date");
            entity.Property(e => e.MenuId).HasColumnName("menu_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.Menu).WithMany(p => p.MenuPriceTrackings)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_MenuPriceTracking");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3213E83FCEE14E36");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderDate)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("order_date");
            entity.Property(e => e.OrderKuryeId).HasColumnName("order_kurye_id");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("order_status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.OrderKurye).WithMany(p => p.OrderDetailOrderKuryes)
                .HasForeignKey(d => d.OrderKuryeId)
                .HasConstraintName("FK__OrderDeta__order__3D5E1FD2");

            entity.HasOne(d => d.User).WithMany(p => p.OrderDetailUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__order__3C69FB99");

            entity.HasMany(d => d.Meals).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "MealOrder",
                    r => r.HasOne<Meal>().WithMany()
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Meal_MealOrder"),
                    l => l.HasOne<OrderDetail>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_OrderDetails_MealOrder"),
                    j =>
                    {
                        j.HasKey("OrderId", "MealId").HasName("MealOrder_pk");
                        j.ToTable("Meal_Order");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                        j.IndexerProperty<int>("MealId").HasColumnName("meal_id");
                    });

            entity.HasMany(d => d.Menus).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "MenuOrder",
                    r => r.HasOne<Menu>().WithMany()
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Menu_MenuOrder"),
                    l => l.HasOne<OrderDetail>().WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Order_MenuOrder"),
                    j =>
                    {
                        j.HasKey("OrderId", "MenuId").HasName("MenuOrder_pk");
                        j.ToTable("Menu_Order");
                        j.IndexerProperty<int>("OrderId").HasColumnName("order_id");
                        j.IndexerProperty<int>("MenuId").HasColumnName("menu_id");
                    });
        });

        modelBuilder.Entity<RolesDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolesDet__3213E83F85F4EFE5");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F60AAD67A");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("user_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_id__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
