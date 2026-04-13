using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class ChampionContext : DbContext
{
    public ChampionContext()
    {
    }

    public ChampionContext(DbContextOptions<ChampionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BatchMaterial> BatchMaterials { get; set; }

    public virtual DbSet<BatchStep> BatchSteps { get; set; }

    public virtual DbSet<BatchStepParam> BatchStepParams { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Deviation> Deviations { get; set; }

    public virtual DbSet<Equipment> Equipments { get; set; }

    public virtual DbSet<EquipmentTelemetry> EquipmentTelemetries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<LabDecision> LabDecisions { get; set; }

    public virtual DbSet<LabParam> LabParams { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<LabTestParam> LabTestParams { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialBatch> MaterialBatches { get; set; }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductBatch> ProductBatches { get; set; }

    public virtual DbSet<ProductForm> ProductForms { get; set; }

    public virtual DbSet<ProductOrder> ProductOrders { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeComponent> RecipeComponents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StatusHistory> StatusHistories { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<TechCard> TechCards { get; set; }

    public virtual DbSet<TechStep> TechSteps { get; set; }

    public virtual DbSet<TechStepParam> TechStepParams { get; set; }

    public virtual DbSet<TechStepType> TechStepTypes { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1:40001;Username=xaliks;Password=coolPaSsw0rd;Database=champion");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BatchMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchMaterials_pkey");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchMaterials)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchMaterials_BatchId_fkey");

            entity.HasOne(d => d.MaterialBatch).WithMany(p => p.BatchMaterials)
                .HasForeignKey(d => d.MaterialBatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchMaterials_MaterialBatchId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.BatchMaterials)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchMaterials_UnitId_fkey");
        });

        modelBuilder.Entity<BatchStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchSteps_pkey");

            entity.Property(e => e.FinishedAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.StartedAt).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchSteps)
                .HasForeignKey(d => d.BatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchSteps_BatchId_fkey");

            entity.HasOne(d => d.Operator).WithMany(p => p.BatchSteps)
                .HasForeignKey(d => d.OperatorId)
                .HasConstraintName("BatchSteps_OperatorId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.BatchSteps)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchSteps_StatusId_fkey");

            entity.HasOne(d => d.TechStep).WithMany(p => p.BatchSteps)
                .HasForeignKey(d => d.TechStepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchSteps_TechStepId_fkey");
        });

        modelBuilder.Entity<BatchStepParam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("BatchStepParams_pkey");

            entity.HasOne(d => d.BatchStep).WithMany(p => p.BatchStepParams)
                .HasForeignKey(d => d.BatchStepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchStepParams_BatchStepId_fkey");

            entity.HasOne(d => d.TechParam).WithMany(p => p.BatchStepParams)
                .HasForeignKey(d => d.TechParamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchStepParams_TechParamId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.BatchStepParams)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("BatchStepParams_UnitId_fkey");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Departments_pkey");
        });

        modelBuilder.Entity<Deviation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Deviations_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Batch).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("Deviations_BatchId_fkey");

            entity.HasOne(d => d.BatchStepParam).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.BatchStepParamId)
                .HasConstraintName("Deviations_BatchStepParamId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Deviations_CreatedBy_fkey");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Equipments_pkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Equipments_StatusId_fkey");
        });

        modelBuilder.Entity<EquipmentTelemetry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("EquipmentTelemetry_pkey");

            entity.ToTable("EquipmentTelemetry");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Batch).WithMany(p => p.EquipmentTelemetries)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("EquipmentTelemetry_BatchId_fkey");

            entity.HasOne(d => d.Equipment).WithMany(p => p.EquipmentTelemetries)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("EquipmentTelemetry_EquipmentId_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Events_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Events_CreatedBy_fkey");
        });

        modelBuilder.Entity<LabDecision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LabDecisions_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LabDecisions)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabDecisions_CreatedBy_fkey");

            entity.HasOne(d => d.Test).WithMany(p => p.LabDecisions)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabDecisions_TestId_fkey");
        });

        modelBuilder.Entity<LabParam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LabParams_pkey");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LabTests_pkey");

            entity.Property(e => e.CompletedAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabTests_CreatedBy_fkey");

            entity.HasOne(d => d.MaterialBatch).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.MaterialBatchId)
                .HasConstraintName("LabTests_MaterialBatchId_fkey");

            entity.HasOne(d => d.ProductBatch).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.ProductBatchId)
                .HasConstraintName("LabTests_ProductBatchId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabTests_StatusId_fkey");
        });

        modelBuilder.Entity<LabTestParam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LabTestParams_pkey");

            entity.HasOne(d => d.Param).WithMany(p => p.LabTestParams)
                .HasForeignKey(d => d.ParamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabTestParams_ParamId_fkey");

            entity.HasOne(d => d.Test).WithMany(p => p.LabTestParams)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabTestParams_TestId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.LabTestParams)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("LabTestParams_UnitId_fkey");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Materials_pkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Materials)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Materials_StatusId_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Materials)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Materials_TypeId_fkey");
        });

        modelBuilder.Entity<MaterialBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MaterialBatches_pkey");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialBatches)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MaterialBatches_MaterialId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.MaterialBatches)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MaterialBatches_StatusId_fkey");

            entity.HasOne(d => d.SupplierNavigation).WithMany(p => p.MaterialBatches)
                .HasForeignKey(d => d.Supplier)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MaterialBatches_Supplier_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.MaterialBatches)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("MaterialBatches_UnitId_fkey");
        });

        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("MaterialTypes_pkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Products_pkey");

            entity.HasIndex(e => e.Code, "Products_Code_key").IsUnique();

            entity.HasOne(d => d.Form).WithMany(p => p.Products)
                .HasForeignKey(d => d.FormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_FormId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Products)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_StatusId_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Products)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Products_TypeId_fkey");
        });

        modelBuilder.Entity<ProductBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductBatches_pkey");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");
            entity.Property(e => e.EndedAt).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductBatches_OrderId_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductBatches_RecipeId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductBatches_StatusId_fkey");

            entity.HasOne(d => d.TechCard).WithMany(p => p.ProductBatches)
                .HasForeignKey(d => d.TechCardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductBatches_TechCardId_fkey");
        });

        modelBuilder.Entity<ProductForm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductForms_pkey");
        });

        modelBuilder.Entity<ProductOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductOrders_pkey");

            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductOrders_CreatedBy_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductOrders_ProductId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.ProductOrders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("ProductOrders_StatusId_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ProductTypes_pkey");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Recipes_pkey");

            entity.HasIndex(e => e.ProductId, "Recipes_ProductId_idx")
                .IsUnique()
                .HasFilter("(\"StatusId\" = 1)");

            entity.Property(e => e.ApprovedAt).HasColumnType("timestamp without time zone");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.ApprovedByNavigation).WithMany(p => p.RecipeApprovedByNavigations)
                .HasForeignKey(d => d.ApprovedBy)
                .HasConstraintName("Recipes_ApprovedBy_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.RecipeCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Recipes_CreatedBy_fkey");

            entity.HasOne(d => d.Product).WithOne(p => p.Recipe)
                .HasForeignKey<Recipe>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Recipes_ProductId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Recipes_StatusId_fkey");
        });

        modelBuilder.Entity<RecipeComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("RecipeComponents_pkey");

            entity.Property(e => e.Percentage).HasPrecision(5, 2);

            entity.HasOne(d => d.Material).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RecipeComponents_MaterialId_fkey");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RecipeComponents_RecipeId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pkey");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Statuses_pkey");
        });

        modelBuilder.Entity<StatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("StatusHistory_pkey");

            entity.ToTable("StatusHistory");

            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.StatusHistories)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("StatusHistory_ChangedBy_fkey");

            entity.HasOne(d => d.NewStatus).WithMany(p => p.StatusHistoryNewStatuses)
                .HasForeignKey(d => d.NewStatusId)
                .HasConstraintName("StatusHistory_NewStatusId_fkey");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.StatusHistoryOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .HasConstraintName("StatusHistory_OldStatusId_fkey");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Suppliers_pkey");
        });

        modelBuilder.Entity<TechCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TechCards_pkey");

            entity.HasIndex(e => e.RecipeId, "TechCards_RecipeId_idx")
                .IsUnique()
                .HasFilter("(\"StatusId\" = 1)");

            entity.HasOne(d => d.Recipe).WithOne(p => p.TechCard)
                .HasForeignKey<TechCard>(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechCards_RecipeId_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.TechCards)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechCards_StatusId_fkey");
        });

        modelBuilder.Entity<TechStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TechSteps_pkey");

            entity.HasOne(d => d.Card).WithMany(p => p.TechSteps)
                .HasForeignKey(d => d.CardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechSteps_CardId_fkey");

            entity.HasOne(d => d.StatusNavigation).WithMany(p => p.TechSteps)
                .HasForeignKey(d => d.Status)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechSteps_Status_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.TechSteps)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechSteps_TypeId_fkey");
        });

        modelBuilder.Entity<TechStepParam>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TechStepParams_pkey");

            entity.HasOne(d => d.Step).WithMany(p => p.TechStepParams)
                .HasForeignKey(d => d.StepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechStepParams_StepId_fkey");

            entity.HasOne(d => d.Unit).WithMany(p => p.TechStepParams)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("TechStepParams_UnitId_fkey");
        });

        modelBuilder.Entity<TechStepType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("TechStepTypes_pkey");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Units_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_DepartmentId_fkey");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Users_RoleId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
