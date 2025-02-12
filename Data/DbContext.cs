using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace Library_Management_System.Data
{
    public class AppDbContext : DbContext
    {
        // Registering my models in the db context
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }

        /*    protected readonly IConfiguration Configuration;
    */
        /*   public AppDbContext(IConfiguration configuration)
           {
               Configuration = configuration;
           }*/

        /*   protected override void OnConfiguring(DbContextOptionsBuilder options)
           {
               // connect to postgres with connection string
               options.UseNpgsql(Configuration.GetConnectionString("Server=localhost;Port=5432;User Id=postgres;Password=U3k8ts7W2t;Database=Susan_Library_Db);"));
           }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=U3k8ts7W2t;Database=Susan_Library_Db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>().HasKey(x => x.Id);

            modelBuilder.Entity<Book>().HasKey(m => m.ISBN);
               
        }


    }
}
