using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Website.Domain.Entities;

namespace Website.Infrastructure.Contexts
{
    public class WebContext :DbContext
    {
        protected readonly IConfiguration Configuration;

        public WebContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

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

        public DbSet<Test> Tests { get; set; }
        public DbSet<Test> Test2 { get; set; }
    }
}
