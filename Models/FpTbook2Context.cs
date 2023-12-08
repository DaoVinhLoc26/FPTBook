using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeMoGCS10035.Models;

public partial class FpTbook2Context : DbContext
{
    public FpTbook2Context()
    {
    }

    public FpTbook2Context(DbContextOptions<FpTbook2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAuthor> TblAuthors { get; set; }

    public virtual DbSet<TblBook> TblBooks { get; set; }

    public virtual DbSet<TblBookAuthor> TblBookAuthors { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCustomer> TblCustomers { get; set; }

    public virtual DbSet<TblPublisher> TblPublishers { get; set; }

    public virtual DbSet<TblStoreOwner> TblStoreOwners { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAuthor>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__tblAutho__8E2731D9CA5E91DE");

            entity.ToTable("tblAuthor");

            entity.Property(e => e.AuthorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("authorID");
            entity.Property(e => e.AuthorAdress)
                .HasMaxLength(50)
                .HasColumnName("authorAdress");
            entity.Property(e => e.AuthorEmail)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("authorEmail");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(30)
                .HasColumnName("authorName");
        });

        modelBuilder.Entity<TblBook>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__tblBook__8BE5A12DFB39BE51");

            entity.ToTable("tblBook");

            entity.Property(e => e.BookId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("bookID");
            entity.Property(e => e.BookDetailes)
                .HasMaxLength(500)
                .HasColumnName("bookDetailes");
            entity.Property(e => e.BookPrice)
                .HasDefaultValueSql("((10))")
                .HasColumnName("bookPrice");
            entity.Property(e => e.BookTitle)
                .HasMaxLength(30)
                .HasColumnName("bookTitle");
            entity.Property(e => e.CatId).HasColumnName("catID");
            entity.Property(e => e.OwnerId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ownerID");
            entity.Property(e => e.PublisherId).HasColumnName("publisherID");

            entity.HasOne(d => d.Cat).WithMany(p => p.TblBooks)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_catID");

            entity.HasOne(d => d.Owner).WithMany(p => p.TblBooks)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ownerID");

            entity.HasOne(d => d.Publisher).WithMany(p => p.TblBooks)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_publisherID");
        });

        modelBuilder.Entity<TblBookAuthor>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.AuthorId }).HasName("pk_bookauthor");

            entity.ToTable("tblBookAuthor");

            entity.Property(e => e.BookId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("bookID");
            entity.Property(e => e.AuthorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("authorID");
            entity.Property(e => e.Details)
                .HasMaxLength(300)
                .HasColumnName("details");

            entity.HasOne(d => d.Author).WithMany(p => p.TblBookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_authorID");

            entity.HasOne(d => d.Book).WithMany(p => p.TblBookAuthors)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_bookID");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK__tblCateg__17B6DD263D7A4261");

            entity.ToTable("tblCategory");

            entity.HasIndex(e => e.CatName, "UQ__tblCateg__14D6C89B3730D5E5").IsUnique();

            entity.Property(e => e.CatId).HasColumnName("catID");
            entity.Property(e => e.CatDetails)
                .HasMaxLength(300)
                .HasColumnName("catDetails");
            entity.Property(e => e.CatName)
                .HasMaxLength(50)
                .HasColumnName("catName");
        });

        modelBuilder.Entity<TblCustomer>(entity =>
        {
            entity.HasKey(e => e.CustomerEmail).HasName("PK__tblCusto__FFE82D7335B75F1A");

            entity.ToTable("tblCustomer");

            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("customerEmail");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(30)
                .HasColumnName("customerAddress");
            entity.Property(e => e.CustomerFullname)
                .HasMaxLength(30)
                .HasColumnName("customerFullname");
            entity.Property(e => e.CustomerPassword)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("customerPassword");
            entity.Property(e => e.CustomerPhone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("customerPhone");
        });

        modelBuilder.Entity<TblPublisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__tblPubli__7E8A0DF6EBEB7FB9");

            entity.ToTable("tblPublisher");

            entity.HasIndex(e => e.PublisherName, "UQ__tblPubli__22E7F39502AE2BBC").IsUnique();

            entity.Property(e => e.PublisherId).HasColumnName("publisherID");
            entity.Property(e => e.PublisherAddress)
                .HasMaxLength(50)
                .HasColumnName("publisherAddress");
            entity.Property(e => e.PublisherDetails)
                .HasMaxLength(300)
                .HasColumnName("publisherDetails");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(30)
                .HasColumnName("publisherName");
        });

        modelBuilder.Entity<TblStoreOwner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__tblStore__7E4B716C819CF4D9");

            entity.ToTable("tblStoreOwner");

            entity.Property(e => e.OwnerId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ownerID");
            entity.Property(e => e.OwnerAddress)
                .HasMaxLength(50)
                .HasColumnName("ownerAddress");
            entity.Property(e => e.OwnerDetails)
                .HasMaxLength(300)
                .HasColumnName("ownerDetails");
            entity.Property(e => e.OwnerName)
                .HasMaxLength(30)
                .HasColumnName("ownerName");
            entity.Property(e => e.OwnerPhone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("ownerPhone");
            entity.Property(e => e.OwnerTaxCode)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("ownerTaxCode");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
