using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Domain;

namespace News.Data
{
    public class DataContext : IdentityDbContext<User, IdentityRole, string>
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
            builder.Entity<StudentExcersise>().HasKey(x => new { x.userId, x.taskId});
            builder.Entity<Subject>().HasKey(x => new { x.Id});
            //builder.Entity<Tag>().HasKey(x => new {x.TagId, x.TagName});
            //builder.Entity<UserBusiness>().HasNoKey();
        }
    }
}
