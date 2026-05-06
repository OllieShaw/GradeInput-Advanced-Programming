using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GradeInput_Advanced_Programming.Trading;

public partial class TradingContext : DbContext
{
    public TradingContext()
    {
    }

    public TradingContext(DbContextOptions<TradingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Currency> Currencies { get; set; }


    public virtual DbSet<UAppGrade> UAppGrades { get; set; }


    public virtual DbSet<Volumetype> Volumetypes { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("postgres_fdw");

     

        modelBuilder.Entity<Currency>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkey_currencies");

            entity.ToTable("currencies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CurrencyName)
                .HasMaxLength(7)
                .HasColumnName("currencyName");
            entity.Property(e => e.Symbol)
                .HasMaxLength(1)
                .HasColumnName("symbol");
        });


        modelBuilder.Entity<UAppGrade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("u_app_grades_pkey");

            entity.ToTable("u_app_grades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CostsAddOn).HasColumnName("costs_add_on");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.DefaultCostsAddOn).HasColumnName("default_costs_add_on");
            entity.Property(e => e.DiffAddOn).HasColumnName("diff_add_on");
            entity.Property(e => e.ForwardGrade)
                .HasMaxLength(50)
                .HasColumnName("forward_grade");
            entity.Property(e => e.GradeName)
                .HasMaxLength(50)
                .HasColumnName("grade_name");
            entity.Property(e => e.IsContractMonthSpecific).HasColumnName("is_contract_month_specific");
            entity.Property(e => e.IsIndicAvailable).HasColumnName("is_indic_available");
            entity.Property(e => e.IsSplitGrade).HasColumnName("is_split_grade");
            entity.Property(e => e.LitresPerTonne).HasColumnName("litres_per_tonne");
            entity.Property(e => e.MultipleToMt).HasColumnName("multiple_to_mt");
            entity.Property(e => e.ReportedCurrency)
                .HasDefaultValue(1)
                .HasColumnName("reported_currency");
            entity.Property(e => e.Rounding).HasColumnName("rounding");
            entity.Property(e => e.SplitData)
                .HasColumnType("jsonb")
                .HasColumnName("split_data");
            entity.Property(e => e.SpotName)
                .HasMaxLength(50)
                .HasColumnName("spot_name");
            entity.Property(e => e.SpotTable)
                .HasMaxLength(50)
                .HasColumnName("spot_table");
            entity.Property(e => e.TableName)
                .HasMaxLength(50)
                .HasColumnName("table_name");
            entity.Property(e => e.Volumetype)
                .HasDefaultValue(1)
                .HasColumnName("volumetype");
            entity.Property(e => e.WinterGradeEndDay).HasColumnName("winter_grade_end_day");
            entity.Property(e => e.WinterGradeEndMonth).HasColumnName("winter_grade_end_month");
            entity.Property(e => e.WinterGradeStartDay).HasColumnName("winter_grade_start_day");
            entity.Property(e => e.WinterGradeStartMonth).HasColumnName("winter_grade_start_month");

            entity.HasOne(d => d.ReportedCurrencyNavigation).WithMany(p => p.UAppGrades)
                .HasForeignKey(d => d.ReportedCurrency)
                .HasConstraintName("fk_currencies");

            entity.HasOne(d => d.VolumetypeNavigation).WithMany(p => p.UAppGrades)
                .HasForeignKey(d => d.Volumetype)
                .HasConstraintName("fk_volumetype");
        });

     
        modelBuilder.Entity<Volumetype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pkey_volumetype");

            entity.ToTable("volumetype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
