using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using api.Models;
using dto;

namespace api.Services;

public interface IWalletsStorage
{
    public record WalletCache(long Id, string Address, decimal Balance);
    //TODO: Use correct thread-safety in-memory storage
    List<WalletCache> Wallets { get; }
    BigInteger CurrentBlock{get;}
    Task CheckUpdates();
}