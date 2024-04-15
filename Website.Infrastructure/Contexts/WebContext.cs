using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            //modelBuilder.Entity<Item>(entity =>
            //{
            //    entity.ToTable("item", "account");
            //    entity.Property(e => e.Id)
            //                        .HasColumnName("id")
            //                        .HasDefaultValueSql("nextval('account.item_id_seq'::regclass)");
            //    entity.Property(e => e.Description).HasColumnName("description");
            //    entity.Property(e => e.Name)
            //                        .IsRequired()
            //                        .HasColumnName("name");
            //});
            //modelBuilder.HasSequence("item_id_seq", "account");
        }

        #region Tables
        public DbSet<Articles> Articles { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Images> Images { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Videos> Videos { get; set; }
        #endregion
    }
}
