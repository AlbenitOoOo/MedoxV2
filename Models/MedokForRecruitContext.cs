using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Medox.Models;

public partial class MedokForRecruitContext : DbContext
{
    public MedokForRecruitContext()
    {
    }

    public MedokForRecruitContext(DbContextOptions<MedokForRecruitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<KorSkd> KorSkds { get; set; }

    public virtual DbSet<KorZst> KorZsts { get; set; }

    public virtual DbSet<KorZstMain> KorZstMains { get; set; }

    public virtual DbSet<KorZstSklad> KorZstSklads { get; set; }

    public virtual DbSet<KorZstToKorZstSklad> KorZstToKorZstSklads { get; set; }

    public virtual DbSet<SkdInZst> SkdInZsts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Latin1_General_CI_AS");

        modelBuilder.Entity<KorSkd>(entity =>
        {
            entity.HasKey(e => e.SkdId);

            entity.ToTable("korSKD");

            entity.Property(e => e.SkdId).HasComment("Identyfikator reprezentacji skladnika.");
            entity.Property(e => e.DbDataWpisu)
                .HasComment("Data pierwszego zapisu do bazy")
                .HasColumnType("datetime")
                .HasColumnName("dbDataWpisu");
            entity.Property(e => e.DbPerId)
                .HasComment("Osoba dokonująca pierwszego zapisu do bazy")
                .HasColumnName("dbPerId");
            entity.Property(e => e.SkdNazwa)
                .HasMaxLength(50)
                .HasComment("Nazwa typu składnika.");
            entity.Property(e => e.SkdOpis)
                .IsUnicode(false)
                .HasComment("Opis typu składnika.");
        });

        modelBuilder.Entity<KorZst>(entity =>
        {
            entity.HasKey(e => e.ZstId).HasName("PK_korZst");

            entity.ToTable("korZST");

            entity.Property(e => e.ZstId).HasComment("Identyfikator zestawu (Składniki też są traktowane jak zestawy)");
            entity.Property(e => e.DbDataWpisu)
                .HasColumnType("datetime")
                .HasColumnName("dbDataWpisu");
            entity.Property(e => e.DbPerId).HasColumnName("dbPerId");
            entity.Property(e => e.ZstMainId).HasComment("Identyfikator zestawu który był pierwszy raz tworzony. Jest powtarzany dla wszystkich modyfikacji - w ten sposób mamy historię modyfikacji zestawu.");
            entity.Property(e => e.ZstToZstSkladId).HasComment("Identyfikator modyfikacji składu zestawu");

            entity.HasOne(d => d.ZstMain).WithMany(p => p.KorZsts)
                .HasForeignKey(d => d.ZstMainId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_korZST_korZST_Main");

            entity.HasOne(d => d.ZstToZstSklad).WithMany(p => p.KorZsts)
                .HasForeignKey(d => d.ZstToZstSkladId)
                .HasConstraintName("FK_korZST_korZST_To_korZST-SKLAD");
        });

        modelBuilder.Entity<KorZstMain>(entity =>
        {
            entity.HasKey(e => e.ZstMainId);

            entity.ToTable("korZST_Main");

            entity.Property(e => e.ZstMainId).HasComment("Identyfikator zestawu który był pierwszy raz tworzony.");
            entity.Property(e => e.DbDataWpisu)
                .HasColumnType("datetime")
                .HasColumnName("dbDataWpisu");
            entity.Property(e => e.DbPerId).HasColumnName("dbPerId");
            entity.Property(e => e.ZstMainNazwa)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasComment("Nazwa zestawu.");
            entity.Property(e => e.ZstMainOpis)
                .IsUnicode(false)
                .HasComment("Opis zestawu.");
        });

        modelBuilder.Entity<KorZstSklad>(entity =>
        {
            entity.HasKey(e => e.ZstSkladId).HasName("PK_korZST-SKLAD-2");

            entity.ToTable("korZST-SKLAD");

            entity.Property(e => e.ZstSkladId).HasComment("Index jednoznacznie określający składnik w składzie zestawu.");
            entity.Property(e => e.DbDataWpisu)
                .HasColumnType("datetime")
                .HasColumnName("dbDataWpisu");
            entity.Property(e => e.DbPerId).HasColumnName("dbPerId");
            entity.Property(e => e.SkdId).HasComment("Index składnika wchodzącego w skład zestawu.");
            entity.Property(e => e.ZstSkladCzyAktywny).HasComment("Określa czy dany składnik jest przypisany do zestawu (zarejestrowany). Domyślnie jest.");
            entity.Property(e => e.ZstSkladIloscSkd).HasComment("Ilość składników.");
            entity.Property(e => e.ZstSkladnikUwagi)
                .IsUnicode(false)
                .HasComment("Uwagi do składnika wchodzącego w skład zestawu.");
            entity.Property(e => e.ZstToZstSkladId).HasComment("Identyfikator modyfikacji składu zestawu");

            entity.HasOne(d => d.Skd).WithMany(p => p.KorZstSklads)
                .HasForeignKey(d => d.SkdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_korZST-SKLAD_korSKD");

            entity.HasOne(d => d.ZstToZstSklad).WithMany(p => p.KorZstSklads)
                .HasForeignKey(d => d.ZstToZstSkladId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_korZST-SKLAD_korZST_To_korZST-SKLAD");
        });

        modelBuilder.Entity<KorZstToKorZstSklad>(entity =>
        {
            entity.HasKey(e => e.ZstToZstSkladId);

            entity.ToTable("korZST_To_korZST-SKLAD");

            entity.Property(e => e.ZstToZstSkladId).HasComment("Identyfikator modyfikacji składu zestawu");
            entity.Property(e => e.PerId).HasComment("Identyfikator osoby dokonującej modyfikacji składu zestawu");
            entity.Property(e => e.ZstToZstSkladData)
                .HasComment("Data modyfikacji składu zestawu")
                .HasColumnType("smalldatetime");
        });

        modelBuilder.Entity<SkdInZst>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("SKD_IN_ZST");

            entity.Property(e => e.SkdNazwa).HasMaxLength(50);
            entity.Property(e => e.ZstMainNazwa)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
