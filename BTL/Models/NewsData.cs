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

        public virtual DbSet<administrative> administratives { get; set; }
        public virtual DbSet<article> articles { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<comment> comments { get; set; }
        public virtual DbSet<journalist> journalists { get; set; }
        public virtual DbSet<keyword> keywords { get; set; }
        public virtual DbSet<role> roles { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<administrative>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<administrative>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<administrative>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<administrative>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.journalistId)
                .IsUnicode(false);

            modelBuilder.Entity<article>()
                .Property(e => e.categoryId)
                .IsUnicode(false);

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

            modelBuilder.Entity<category>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<comment>()
                .Property(e => e.userId)
                .IsUnicode(false);

            modelBuilder.Entity<journalist>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<journalist>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<journalist>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<journalist>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<journalist>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<role>()
                .Property(e => e.rolename)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.img)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .HasMany(e => e.comments)
                .WithRequired(e => e.user)
                .WillCascadeOnDelete(false);
        }

    }
}
