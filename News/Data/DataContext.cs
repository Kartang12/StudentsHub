using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using News.Domain;

namespace News.Data
{
    public class DataContext : IdentityDbContext<MyUser, IdentityRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        
        public DbSet<Form> Forms{ get; set; }
        
        public DbSet<Subject> Subjects{ get; set; }

        public DbSet<Exercise> Exersises{ get; set; }

        public DbSet<StudentExercise> StudentExercises { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StudentExercise>().HasKey(x => new { x.userId, x.exId });
            //builder.Entity<Subject>().HasKey(x => new { x.Id});
            //builder.Entity<UserBusiness>().HasNoKey();
        }
    }
}
