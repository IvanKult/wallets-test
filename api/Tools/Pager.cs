namespace api.Tools;

using System.Linq;
using dto;

public static class Pager
{
    public static IQueryable<T> Paging<T>(IQueryable<T> target, IPaged req)
    {
        if (req.PageSize == null) return target;
        var offset = (int)((req.Page ?? 0) * req.PageSize);
        return target.Skip(offset).Take((int)req.PageSize);
    }
}
