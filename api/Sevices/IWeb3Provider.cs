using System.Numerics;
using System.Threading.Tasks;

namespace api.Services;
public interface IWeb3Provider
{
    Task<BigInteger> GetlastBlockNumberAsync();
    Task<decimal> GetWalletBalanceAsync(string walletAddress);
}