using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestOcbc.Models
{
    public class bankContext : DbContext
    {
        private IConfiguration Configuration;

        public bankContext(DbContextOptions<bankContext> options, IConfiguration config) : base(options)
        {
            Configuration = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL(Configuration.GetConnectionString("MysqlConnection"));

        public DbSet<Customers> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<MasterTransaksi> MasterTransaksis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MasterTransaksi>().HasData(new MasterTransaksi[] {
            new MasterTransaksi{
                MasterTransaksiId = 1,
                TransactionCode = "bp",
                TransactionName = "Beli Pulsa"
                },
            new MasterTransaksi{
                MasterTransaksiId = 2,
                TransactionCode = "bt",
                TransactionName = "Beli Listrik"
                },
            new MasterTransaksi{
                MasterTransaksiId = 3,
                TransactionCode = "st",
                TransactionName = "Setor Tunai"
                },
        });
        }
    }
}