using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DeMoGCS10035.Models;

public partial class FptbookdbContext : DbContext
{
    public FptbookdbContext()
    {
    }

    public FptbookdbContext(DbContextOptions<FptbookdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI;Initial Catalog=FPTBOOKDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Author__3213E83F4665414D");

            entity.ToTable("Author");

            entity.Property(e => e.Id)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Adress)
                .HasMaxLength(50)
                .HasColumnName("adress");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Book__3213E83F44AE2B8C");

            entity.ToTable("Book");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.AuthorId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("authorId");
            entity.Property(e => e.CatId).HasColumnName("catId");
            entity.Property(e => e.Detailes)
                .HasMaxLength(500)
                .HasColumnName("detailes");
            entity.Property(e => e.Imagel1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("imagel1");
            entity.Property(e => e.Imagel2)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("imagel2");
            entity.Property(e => e.Imagel3)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("imagel3");
            entity.Property(e => e.Imagel4)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("imagel4");
            entity.Property(e => e.Imagel5)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("imagel5");
            entity.Property(e => e.OwnerId)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ownerId");
            entity.Property(e => e.Price)
                .HasDefaultValueSql("((10))")
                .HasColumnName("price");
            entity.Property(e => e.PublisherId).HasColumnName("publisherID");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_authorId");

            entity.HasOne(d => d.Cat).WithMany(p => p.Books)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_catId");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_publisherId");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cart__3213E83F5217E2F8");

            entity.ToTable("Cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Cart__user_id__5BE2A6F2");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CartItem__3213E83F389B2594");

            entity.ToTable("CartItem");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("book_id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Book).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("FK__CartItem__book_i__5EBF139D");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__CartItem__cart_i__5FB337D6");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3213E83F77818475");

            entity.ToTable("Category");

            entity.HasIndex(e => e.Name, "UQ__Category__72E12F1B7F67047B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Details)
                .HasMaxLength(300)
                .HasColumnName("details");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Publishe__3213E83F25FF616D");

            entity.ToTable("Publisher");

            entity.HasIndex(e => e.Name, "UQ__Publishe__72E12F1B95CFFBF6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Details)
                .HasMaxLength(300)
                .HasColumnName("details");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F318391C5");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Details)
                .HasMaxLength(300)
                .HasColumnName("details");
            entity.Property(e => e.FullName)
                .HasMaxLength(30)
                .HasColumnName("fullName");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('User')")
                .HasColumnName("role");
            entity.Property(e => e.TaxCode)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("taxCode");
            entity.Property(e => e.Username)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
