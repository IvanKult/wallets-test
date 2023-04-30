using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using api.Models;
using dto;
using Microsoft.EntityFrameworkCore;
using static api.Services.IWalletsStorage;

namespace api.Services;

public class WalletsStorage : IWalletsStorage
{
    public List<WalletCache> Wallets { get; private set; } = new List<WalletCache>();
    public BigInteger CurrentBlock { get; private set; } = 0;
    private readonly IWeb3Provider _web3Provider;
    private readonly IDbContext _dbContext;
    public WalletsStorage(IWeb3Provider web3Provider, IDbContext dbContext)
    {
        _web3Provider = web3Provider;
        _dbContext = dbContext;
    }
    public async Task CheckUpdates()
    {
        var lastBlock = await _web3Provider.GetlastBlockNumberAsync();
        if (lastBlock <= CurrentBlock) return;
        CurrentBlock = lastBlock;
        var wallets = await _dbContext.Wallets.ToListAsync();
        var tasks = wallets.Select(x => Process(x)).ToList();
        await Task.WhenAll(tasks);
        Wallets = tasks.Select(x => x.Result).ToList();
    }
    private async Task<WalletCache> Process(Wallet wallet)
    {
        var newBalance = await _web3Provider.GetWalletBalanceAsync(wallet.Address);
        return new WalletCache(wallet.Id, wallet.Address, newBalance);
    }
}