using CRM.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CRM.DataAccess.Concrete;

public partial class CRMContext : DbContext
{
	public CRMContext()
	{
	}

	public CRMContext(DbContextOptions<CRMContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Company> Companies { get; set; }

	public virtual DbSet<Invoice> Invoices { get; set; }

	public virtual DbSet<Lifetime> Lifetimes { get; set; }

	public virtual DbSet<Membership> Memberships { get; set; }

	public virtual DbSet<Order> Orders { get; set; }

	public virtual DbSet<OrderInvoiceMap> OrderInvoiceMaps { get; set; }

	public virtual DbSet<Product> Products { get; set; }

	public virtual DbSet<ProductType> ProductTypes { get; set; }

	public virtual DbSet<User> Users { get; set; }

	public virtual DbSet<UserRole> UserRoles { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
		=> optionsBuilder.UseNpgsql("User ID=postgres;Password=postgre;Server=127.0.0.1;Database=CRM;Integrated Security=true;Pooling=true;");

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Company>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("company_id_pkey");

			entity.Property(e => e.Id).HasDefaultValueSql("nextval('company_id_id_seq'::regclass)");
		});

		modelBuilder.Entity<Invoice>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("Invoince_pkey");

			entity.Property(e => e.Id).HasDefaultValueSql("nextval('\"Invoince_id_seq\"'::regclass)");

			entity.HasOne(d => d.User).WithMany(p => p.Invoices)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("Invoince_user_id_fkey");
		});

		modelBuilder.Entity<Lifetime>(entity =>
		{
			entity.HasKey(e => e.OrderId).HasName("lifetime_pkey");

			entity.Property(e => e.OrderId).ValueGeneratedNever();

			entity.HasOne(d => d.Order).WithOne(p => p.Lifetime)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("lifetime_order_id_fkey");
		});

		modelBuilder.Entity<Membership>(entity =>
		{
			entity.HasKey(e => e.OrderId).HasName("membership_pkey");

			entity.Property(e => e.OrderId).ValueGeneratedNever();

			entity.HasOne(d => d.Order).WithOne(p => p.Membership)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("membership_order_id_fkey");
		});

		modelBuilder.Entity<Order>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("product_pkey");

			entity.Property(e => e.Id).HasDefaultValueSql("nextval('product_id_seq'::regclass)");

			entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("order_customer_id_fkey");

			entity.HasOne(d => d.Product).WithMany(p => p.Orders)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("order_product_id_fkey");
		});

		modelBuilder.Entity<OrderInvoiceMap>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("orderInvoiceMap_pkey");

			entity.HasOne(d => d.Invoice).WithMany(p => p.OrderInvoiceMaps)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("orderInvoiceMap_invoice_id_fkey");

			entity.HasOne(d => d.Order).WithMany(p => p.OrderInvoiceMaps)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("orderInvoiceMap_order_id_fkey");
		});

		modelBuilder.Entity<Product>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("product_pkey1");

			entity.Property(e => e.Id).HasDefaultValueSql("nextval('product_id_seq1'::regclass)");

			entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("product_product_type_id_fkey");

			entity.HasOne(d => d.Seller).WithMany(p => p.Products)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("product_seller_id_fkey");
		});

		modelBuilder.Entity<ProductType>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("product_type_pkey");
		});

		modelBuilder.Entity<User>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("user_pkey");

			entity.HasOne(d => d.Company).WithMany(p => p.Users)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("user_company_id_fkey");

			entity.HasOne(d => d.Role).WithMany(p => p.Users)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("user_role_id_fkey");
		});

		modelBuilder.Entity<UserRole>(entity =>
		{
			entity.HasKey(e => e.Id).HasName("user_role_pkey");
		});

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
