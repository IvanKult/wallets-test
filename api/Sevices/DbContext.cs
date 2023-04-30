using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IDbContext
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbContext(DbContextOptions<DbContext> options) : base(options)
    {
    }
}