using ChatboxDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatboxDemo.Data
{
  public class ChatboxDbContext : DbContext
  {
    public ChatboxDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Message> Messages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Create Relationships
    }
  }
}
