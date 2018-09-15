namespace ShoesProjectModels.Model
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Shoes : DbContext
    {
        public Shoes()
            : base("name=HungModel")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Permisson> Permissons { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminPassword)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .Property(e => e.AdminUsername)
                .IsUnicode(false);

            modelBuilder.Entity<Admin>()
                .HasMany(e => e.Permissons)
                .WithMany(e => e.Admins)
                .Map(m => m.ToTable("GrantedPermissons").MapLeftKey("AdminId").MapRightKey("PermissonId"));

            modelBuilder.Entity<Brand>()
                .Property(e => e.BrandImage)
                .IsUnicode(false);

            modelBuilder.Entity<Brand>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Brand)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Business>()
                .Property(e => e.BusinessId)
                .IsUnicode(false);

            modelBuilder.Entity<Business>()
                .HasMany(e => e.Permissons)
                .WithRequired(e => e.Business)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .Property(e => e.ColorValue)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .Property(e => e.ColorCode)
                .IsUnicode(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.OrdersDetails)
                .WithRequired(e => e.Color)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Color>()
                .HasMany(e => e.Products)
                .WithMany(e => e.Colors)
                .Map(m => m.ToTable("ProductColors").MapLeftKey("ColorId").MapRightKey("ProductId"));

            modelBuilder.Entity<Order>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrdersDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Permisson>()
                .Property(e => e.PermissonName)
                .IsUnicode(false);

            modelBuilder.Entity<Permisson>()
                .Property(e => e.BusinessId)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrdersDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.ProductImages)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Sizes)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("ProductSizes").MapLeftKey("ProductId").MapRightKey("SizeId"));

            modelBuilder.Entity<Size>()
                .Property(e => e.SizeValue)
                .IsUnicode(false);

            modelBuilder.Entity<Size>()
                .HasMany(e => e.OrdersDetails)
                .WithRequired(e => e.Size)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Voucher>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ProductImage>()
                .Property(e => e.Image)
                .IsUnicode(false);
        }
    }
}
