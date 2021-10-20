namespace Data
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.Extensions.Options;
    using System;

    public class DataContext : DbContext
    {
        private readonly IOptions<DataSettings> options;

        public DataContext(IOptions<DataSettings> options)
        {
            this.options = options;
        }

        public DbSet<Building> Building { get; set; } = null!;
        public DbSet<BuildingOwner> BuildingOwner { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(options.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            EntityTypeBuilder<Building> buildingModel = modelBuilder.Entity<Building>();
            buildingModel.HasKey(b => b.Id);

            EntityTypeBuilder<BuildingOwner> buildingOwnerModel = modelBuilder.Entity<BuildingOwner>();
            buildingOwnerModel.HasKey(b => b.Id);
        }
    }
}
