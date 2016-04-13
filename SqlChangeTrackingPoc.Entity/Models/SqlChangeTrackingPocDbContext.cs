using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SqlChangeTrackingPoc.Entity.Models.Mapping;

namespace SqlChangeTrackingPoc.Entity.Models
{
    public partial class SqlChangeTrackingPocDbContext : DbContext
    {
        static SqlChangeTrackingPocDbContext()
        {
            Database.SetInitializer<SqlChangeTrackingPocDbContext>(null);
        }

        public SqlChangeTrackingPocDbContext()
            : base("Name=SqlChangeTrackingPocDbContext")
        {
        }

        public DbSet<DevTest> DevTests { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DevTestMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
