using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.WebApplication.Data;

public partial class HtqlkoiContext : DbContext
{
    public HtqlkoiContext()
    {
    }

    public HtqlkoiContext(DbContextOptions<HtqlkoiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<EmployeePermission> EmployeePermissions { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Delivery>(entity =>
        {
            entity.HasKey(e => e.DeliveryId).HasName("PK__Delivery__1C5CF4F53FD9464C");

            entity.ToTable("Delivery");

            entity.Property(e => e.DeliveryId).HasColumnName("delivery_id");
            entity.Property(e => e.ActualTime)
                .HasColumnType("datetime")
                .HasColumnName("actual_time");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .HasColumnName("delivery_address");
            entity.Property(e => e.DeliveryStaffId).HasColumnName("delivery_staff_id");
            entity.Property(e => e.DeliveryStatus)
                .HasMaxLength(50)
                .HasColumnName("delivery_status");
            entity.Property(e => e.EstimatedTime)
                .HasColumnType("datetime")
                .HasColumnName("estimated_time");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.DeliveryStaff).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.DeliveryStaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Delivery__delive__619B8048");

            entity.HasOne(d => d.Order).WithMany(p => p.Deliveries)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Delivery__order___60A75C0F");
        });

        modelBuilder.Entity<EmployeePermission>(entity =>
        {
            entity.HasKey(e => e.EmployeePermissionId).HasName("PK__Employee__13371F08CEC1E2E0");

            entity.Property(e => e.EmployeePermissionId).HasColumnName("employee_permission_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Permission)
                .HasMaxLength(255)
                .HasColumnName("permission");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeePermissions)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EmployeeP__emplo__76969D2E");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__7A6B2B8C5A414AB4");

            entity.ToTable("Feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Service).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__servic__6754599E");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Feedback__user_i__66603565");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoice__F58DFD490ED797CE");

            entity.ToTable("Invoice");

            entity.Property(e => e.InvoiceId).HasColumnName("invoice_id");
            entity.Property(e => e.AmountDue)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount_due");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__order_i__6D0D32F4");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Invoice__user_id__6E01572D");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__465962295EBAAD79");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.AdditionalServices)
                .HasMaxLength(255)
                .HasColumnName("additional_services");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DeliveryLocation)
                .HasMaxLength(255)
                .HasColumnName("delivery_location");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.PickupLocation)
                .HasMaxLength(255)
                .HasColumnName("pickup_location");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(50)
                .HasColumnName("shipping_method");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.TotalQuantity).HasColumnName("total_quantity");
            entity.Property(e => e.TotalWeight).HasColumnName("total_weight");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__customer__571DF1D5");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__3C5A4080EA391260");

            entity.Property(e => e.OrderDetailId).HasColumnName("order_detail_id");
            entity.Property(e => e.KoiFishType)
                .HasMaxLength(50)
                .HasColumnName("koi_fish_type");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderDeta__order__5BE2A6F2");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__3E0DB8AF27F19605");

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("price");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__85C600AF35727A83");

            entity.Property(e => e.TransactionId).HasColumnName("transaction_id");
            entity.Property(e => e.Amount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__order__72C60C4A");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Transacti__user___71D1E811");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370FEE1F5C11");

            entity.HasIndex(e => e.Email, "UQ__Users__AB6E616438772B53").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
