using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Services;
using dto;
using MediatR;

namespace api.Handlers;

public class GetWalletsInfoHandler : IRequestHandler<GetWalletsInfo.Request, GetWalletsInfo.Response>
{
    private readonly IWalletsStorage _walletsStorage;
    public GetWalletsInfoHandler(IWalletsStorage walletsStorage) => _walletsStorage = walletsStorage;
    public Task<GetWalletsInfo.Response> Handle(GetWalletsInfo.Request request, CancellationToken cancellationToken)
    {
        // await _walletsStorage.CheckUpdates();
        var all = _walletsStorage.Wallets.OrderBy(x => x.Balance);
        var response = new GetWalletsInfo.Response
        {
            Data = all.Select(x => new WalletInfo
            {
                Id = x.Id,
                Address = x.Address,
                Balance = x.Balance
            }).ToList(),
            BlockNumber = (long)_walletsStorage.CurrentBlock
        };
        return Task.FromResult<GetWalletsInfo.Response>(response);
    }
}