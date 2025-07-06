using System;
using System.Collections.Generic;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccessLayer
{
    public partial class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {
        }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Borrowing> Borrowings { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Fine> Fines { get; set; } = null!;
        public virtual DbSet<Patron> Patrons { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string basePath = AppContext.BaseDirectory;

                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(basePath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                string? connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    Console.WriteLine("Connection string 'DefaultConnection' not found in appsettings.json. Using fallback for DbContext.");
                    optionsBuilder.UseSqlServer("server=LAPTOP-CMSU7LK1;database=LibraryDB;uid=sa;pwd=12345; TrustServerCertificate=True");
                }
                else
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C207C909D2D6");

                entity.Property(e => e.Author)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Genre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Isbn)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ISBN");
                entity.Property(e => e.Publisher)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.ShelfLocation)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category).WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Books__CategoryI__2C3393D0");
            });

            modelBuilder.Entity<Borrowing>(entity =>
            {
                entity.HasKey(e => e.BorrowingId).HasName("PK__Borrowin__6CD933D7EB0F50CD");

                entity.Property(e => e.BorrowDate).HasColumnType("date");
                entity.Property(e => e.DueDate).HasColumnType("date");
                entity.Property(e => e.IsReturned).HasDefaultValue(false);
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ReturnDate).HasColumnType("date");

                entity.HasOne(d => d.Book).WithMany(p => p.Borrowings)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__Borrowing__BookI__32E0915F");

                entity.HasOne(d => d.Patron).WithMany(p => p.Borrowings)
                    .HasForeignKey(d => d.PatronId)
                    .HasConstraintName("FK__Borrowing__Patro__33D4B598");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BF9683907");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(e => e.FineId).HasName("PK__Fines__9D4A9B2CA4FE8EC0");

                entity.Property(e => e.FineAmount).HasColumnType("decimal(10, 2)");
                entity.Property(e => e.Paid).HasDefaultValue(false);
                entity.Property(e => e.FineDate).HasColumnType("datetime");
                entity.Property(e => e.PatronId);

                entity.HasOne(d => d.Borrowing).WithMany(p => p.Fines)
                    .HasForeignKey(d => d.BorrowingId)
                    .HasConstraintName("FK__Fines__Borrowing__3A81B327");
            });

            modelBuilder.Entity<Patron>(entity =>
            {
                entity.HasKey(e => e.PatronId).HasName("PK__Patrons__14EC69FA037D8DDA");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.MembershipType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.RegistrationDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CF8BACD31");

                entity.HasIndex(e => e.UserName, "UQ__Users__536C85E4B0CFBE3A").IsUnique();

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}