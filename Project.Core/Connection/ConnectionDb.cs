using System.Data.Entity;
using Project.Core.Entities;

namespace Project.Core.Connection
{
    public class ConnectionDb : DbContext
    {
        public ConnectionDb() : base("name=Data01")
        {
            Database.SetInitializer(new Initializer());
        }
        //DbSet tanımları yapılacak
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(I => I.Notes).WithRequired(X => X.Category).WillCascadeOnDelete(true);
            modelBuilder.Entity<Note>().HasMany(I => I.Comments).WithRequired(X => X.Note).WillCascadeOnDelete(true);
            modelBuilder.Entity<Note>().HasMany(I => I.Likes).WithRequired(X => X.Note).WillCascadeOnDelete(true);
        }
        public virtual DbSet<BlogUser> TableUser { get; set; }
        public virtual DbSet<Category> TableCategory { get; set; }
        public virtual DbSet<Comment> TableComment { get; set; }
        public virtual DbSet<Note> TableNote { get; set; }
        public virtual DbSet<Like> TableLike { get; set; }

    }
}
