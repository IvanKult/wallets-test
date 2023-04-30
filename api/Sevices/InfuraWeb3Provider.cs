using System;
using System.Numerics;
using System.Threading.Tasks;
using api.Options;
using Microsoft.Extensions.Options;
using Nethereum.Web3;

namespace api.Services;
public class InfuraWeb3Provider : IWeb3Provider
{
    private readonly Web3 _provider;
    public InfuraWeb3Provider(IOptions<Web3ProviderOptions> settings)
    {
        var endpoint = $"{settings.Value.EndpointUrl.TrimEnd('/')}/{settings.Value.ApiKey}";
        _provider = new Web3(endpoint);
    }
    public async Task<BigInteger> GetlastBlockNumberAsync()
    {
        var latestBlockNumber = await _provider.Eth.Blocks.GetBlockNumber.SendRequestAsync();
        return latestBlockNumber.Value;
    }
    public async Task<decimal> GetWalletBalanceAsync(string walletAddress)
    {
        if (string.IsNullOrEmpty(walletAddress)) throw new ArgumentException();//TODO: add validation for wallet address format using Regex
        var balance = await _provider.Eth.GetBalance.SendRequestAsync(walletAddress);//TODO: add handling possible errors
        var etherAmount = Web3.Convert.FromWei(balance.Value);
        return etherAmount;
    }
}