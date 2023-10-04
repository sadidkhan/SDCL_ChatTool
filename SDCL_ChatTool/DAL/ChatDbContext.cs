using Microsoft.EntityFrameworkCore;
using SDCL_ChatTool.DAL.Models;

namespace SDCL_ChatTool.DAL
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options)
            : base(options)
        {
        }

        public DbSet<ChatLog> ChatLogs { get; set; }
        public DbSet<ChatGptResponseDump> ChatGptResponseDumps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatLog>().ToTable("ChatLog");
            modelBuilder.Entity<ChatGptResponseDump>().ToTable("ChatGptResponseDump");
        }
    }
}
