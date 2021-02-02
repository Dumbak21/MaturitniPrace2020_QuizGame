using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<QuestionAnswers> QA { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<QuestionAnswers>().HasData(new QuestionAnswers { Id = Guid.NewGuid() , Type = Type.closed , Answers = String.Join("°", new string[]{"1", "2", "3", "4" }), Question = "How much is 2+2", Area = Area.Math });
            builder.Entity<QuestionAnswers>().HasData(new QuestionAnswers { Id = Guid.NewGuid() , Type = Type.open , Answer = "1021", Question = "How much is 1000+21", Area = Area.Math });
            builder.Entity<QuestionAnswers>().HasData(new QuestionAnswers { Id = Guid.NewGuid() , Type = Type.closed , Answers = String.Join("°", new string[]{"c", "d", "3", "z" }), Question = "Last letter in alphabet", Area = Area.Math });

        }
    }
}
