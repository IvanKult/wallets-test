using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api.Services;
using dto;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static api.Tools.Pager;

namespace api.Handlers;

public class GetWalletsInfoHandler : IRequestHandler<GetWalletsInfo.Request, GetWalletsInfo.Response>
{
    private readonly IDbContext _dbContext;
    public GetWalletsInfoHandler(IDbContext dbContext) => _dbContext = dbContext;
    public async Task<GetWalletsInfo.Response> Handle(GetWalletsInfo.Request request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Wallets.OrderBy(x => x.Id);
        var paged = Paging(query, request);
        var all = await paged.ToListAsync(cancellationToken);
        return new GetWalletsInfo.Response { Data = all.Select(x => new WalletInfo { Id = x.Id, Address = x.Address, Balance = 0 }).ToList() };
    }
}