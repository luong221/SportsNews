using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BTL.Models
{
    public partial class NewsData : DbContext
    {
        public NewsData()
            : base("name=NewsData")
        {
        }

        public virtual DbSet<article> articles { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<info> infoes { get; set; }
        public virtual DbSet<journalist> journalists { get; set; }
        public virtual DbSet<keyword> keywords { get; set; }
        public virtual DbSet<role> roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<article>()
                .Property(e => e.thumbnail)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .HasMany(e => e.keywords)
                .WithMany(e => e.articles)
                .Map(m => m.ToTable("keyword_article").MapLeftKey("articleId").MapRightKey("keywordId"));

            modelBuilder.Entity<info>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<info>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<info>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<info>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<info>()
                .HasMany(e => e.comments)
                .WithRequired(e => e.info)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<role>()
                .Property(e => e.rolename)
                .IsUnicode(false);
        }
    }
}
