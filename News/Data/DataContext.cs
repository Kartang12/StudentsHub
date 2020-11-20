using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Domain;

namespace News.Data
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Group> Groups{ get; set; }
        
        public DbSet<Subject> Subjects{ get; set; }

        public DbSet<Excersise> Excersises{ get; set; }

        public DbSet<StudentExcersise> StudentExcersises { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StudentExcersise>().HasNoKey();
            //builder.Entity<Tag>().HasKey(x => new {x.TagId, x.TagName});
            //builder.Entity<UserBusiness>().HasNoKey();
        }
    }
}
