using Microsoft.EntityFrameworkCore;
using TheosBookStore.Stock.Infra.Mappers;
using TheosBookStore.Stock.Infra.Models;

namespace TheosBookStore.Stock.Infra.Context
{
    public class TheosBookStoreStockDB : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }

        public TheosBookStoreStockDB() : base() { }
        public TheosBookStoreStockDB(DbContextOptions<TheosBookStoreStockDB> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AuthorMap());
            modelBuilder.ApplyConfiguration(new BookAuthorMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
