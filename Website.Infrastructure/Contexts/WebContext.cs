using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Configuration;
using Website.Domain.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace Website.Infrastructure.Contexts
{
    public class WebContext :DbContext
    {
        protected readonly IConfiguration Configuration;

        public WebContext(DbContextOptions<WebContext> options) : base(options) { 

        }
        //public WebContext(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to postgres with connection string from app settings
        //    options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(b => b.Guid).Metadata.SetValueGeneratorFactory((_, _) => new SequentialGuidValueGenerator());
                entity.HasIndex(b => b.Guid).IsUnique();
            });


        }

        #region Tables
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Domain.Entities.Image> Images { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        #endregion
    }
}
