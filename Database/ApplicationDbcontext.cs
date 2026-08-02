using Microsoft.EntityFrameworkCore;

namespace Pharmacy_managment.Database
{
    public class ApplicationDbcontext(DbContextOptions<ApplicationDbcontext> options): IdentityDbContext<ApplicationUser>(options)
    {
       public DbSet<Supplier> Suppliers { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Medicene> Medicene { get; set; }
       public DbSet<Customer> Customers { get; set; }
       public DbSet<Pharmacist> pharmacists { get; set; }
       public DbSet<Invoice> Invoices { get; set; }
       public DbSet<PurChaseOrder> Purchases { get; set; }
       public DbSet<InvoiceMedicenedetails> InvoicesMedicenedetails { get; set; }
       public DbSet<PurchaseOrderMedicene> purchaseOrderMedicenes { get; set; }
       public DbSet<MedicineBatch> medicineBatches { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbcontext).Assembly);
        }
    }
}
