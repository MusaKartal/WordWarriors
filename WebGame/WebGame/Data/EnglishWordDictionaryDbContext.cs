using Microsoft.EntityFrameworkCore;
using WebGame.Entities;

namespace WebGame.Data
{
    public class EnglishWordDictionaryDbContext : DbContext
    {

        public EnglishWordDictionaryDbContext(DbContextOptions<EnglishWordDictionaryDbContext> options) : base(options)  
        {

        }

        public DbSet<Word> Words { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ID sütunu üzerinde indeks oluşturma
            modelBuilder.Entity<Word>().HasIndex(w => w.Id).HasName("IX_Words_Id");
        }
    }
}
