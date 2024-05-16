using Microsoft.EntityFrameworkCore;
namespace Tutorial_Create_a_web_API_with_ASP_NET_Core.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
    }
}
