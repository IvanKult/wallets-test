using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public interface IDbContext
{
    DbSet<Wallet> Wallets { get; set; }
}