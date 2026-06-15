using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TechShop.Inventory.Infrastructure.Persistence.Models;

namespace TechShop.Inventory.Infrastructure.Persistence.Context;

public partial class TechShopInventoryContext : DbContext
{
    public TechShopInventoryContext()
    {
    }

    public TechShopInventoryContext(DbContextOptions<TechShopInventoryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryMovementEntity> InventoryMovements { get; set; }

    public virtual DbSet<StockItemEntity> StockItems { get; set; }

    public virtual DbSet<StockReservationEntity> StockReservations { get; set; }

    public virtual DbSet<WarehouseEntity> Warehouses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InventoryMovementEntity>(entity =>
        {
            entity.HasKey(e => e.IdInventoryMovement);

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.MovementType).HasMaxLength(20);
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.Property(e => e.ReferenceId).HasMaxLength(50);

            entity.HasOne(d => d.IdStockItemNavigation).WithMany(p => p.InventoryMovements)
                .HasForeignKey(d => d.IdStockItem)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StockItemEntity>(entity =>
        {
            entity.HasKey(e => e.IdStockItem);

            entity.HasIndex(e => new { e.IdWarehouse, e.Sku }, "UQ_StockItems_IdWarehouse_Sku").IsUnique();

            entity.Property(e => e.IdStockItem).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.QuantityTotal).HasComputedColumnSql("([QuantityAvailable]+[QuantityReserved])", true);
            entity.Property(e => e.Sku).HasMaxLength(50);
            entity.Property(e => e.VersionRow)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.StockItems)
                .HasForeignKey(d => d.IdWarehouse)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<StockReservationEntity>(entity =>
        {
            entity.HasKey(e => e.IdStockReservation);

            entity.Property(e => e.IdStockReservation).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.Property(e => e.ReferenceId).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("PENDING");

            entity.HasOne(d => d.IdStockItemNavigation).WithMany(p => p.StockReservations)
                .HasForeignKey(d => d.IdStockItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockReservations_StockItems");
        });

        modelBuilder.Entity<WarehouseEntity>(entity =>
        {
            entity.HasKey(e => e.IdWarehouse);

            entity.HasIndex(e => e.Code, "UQ_Warehoses_Code").IsUnique();

            entity.Property(e => e.IdWarehouse).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
