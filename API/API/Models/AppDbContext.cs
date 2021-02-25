using API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {


        }

        public DbSet<QuestionAnswers> QA { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var serverR = new AppRole { Id = Guid.NewGuid().ToString(), Name = "Server", NormalizedName = "SERVER" };
            var adminR = new AppRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" };
            builder.Entity<AppRole>().HasData(serverR);
            builder.Entity<AppRole>().HasData(adminR);

            var hasher = new PasswordHasher<AppUser>();

            var server = new AppUser { Id = Guid.NewGuid().ToString(), UserName = "Server", NormalizedUserName = "SERVER", PasswordHash = hasher.HashPassword(null, "123456789"), LockoutEnabled = false, TwoFactorEnabled = false };
            var admin = new AppUser { Id = Guid.NewGuid().ToString(), UserName = "Admin", NormalizedUserName = "ADMIN", PasswordHash = hasher.HashPassword(null, "987654321"), LockoutEnabled = false, TwoFactorEnabled = false, LockoutEnd = DateTime.Now.AddMinutes(10) };
            builder.Entity<AppUser>().HasData(server);
            builder.Entity<AppUser>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasKey(s => new { s.UserId, s.RoleId});
            builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles");
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = server.Id, RoleId = serverR.Id });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminR.Id });

            builder.Entity<QuestionAnswers>().HasData(new QuestionAnswers { Id = Guid.NewGuid() , Type = Type.closed , Answers = String.Join("°", new string[]{"1", "2", "3", "4" }), Question = "How much is 2+2", Area = Area.Math });
            builder.Entity<QuestionAnswers>().HasData(new QuestionAnswers { Id = Guid.NewGuid() , Type = Type.open , Answer = "1021", Question = "How much is 1000+21", Area = Area.Math });
            builder.Entity<QuestionAnswers>().HasData(new QuestionAnswers { Id = Guid.NewGuid() , Type = Type.closed , Answers = String.Join("°", new string[]{"c", "d", "3", "z" }), Question = "Last letter in alphabet", Area = Area.Math });

        }

    }
}
