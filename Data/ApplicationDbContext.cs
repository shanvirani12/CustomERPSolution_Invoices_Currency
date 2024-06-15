using CustomERPSolution_Invoices_Currency.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CustomERPSolution_Invoices_Currency.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<QuotationItem> QuotationItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }


        // Include DbSet for Identity if needed, e.g., Users, Roles, etc.
    }
}
