using Catalog.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Repository
{
    public class ProductContext : DbContext
    {
        private const string ProductContainerName = nameof(Products);
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {

        }
        /// Gets or sets the audits collection.
        /// </summary>
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasNoDiscriminator()
                .ToContainer(nameof(ProductContainerName))
                .HasPartitionKey(da => da.Category);
        }
    }
}
