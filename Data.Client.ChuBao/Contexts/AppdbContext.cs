using Data.Client.ChuBao.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Client.ChuBao.Contexts
{
    public class AppdbContext : DbContext
    {
        public AppdbContext(DbContextOptions<AppdbContext> options) : base(options) { }
        
        public DbSet<Linkman> Linkmen { get; set; }
    }
}
