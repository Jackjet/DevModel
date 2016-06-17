using System.Collections.Generic;
using System.Linq;

namespace Baibaomen.DevModel.Infrastructure
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> items, long? count)
        {
            Items = items;
            Count = count;
        }

        public IEnumerable<T> Items { get; set; }

        public long? Count { get; set; }
    }

    public class PagedResult
    {
        public PagedResult(IQueryable items, long? count)
        {
            Items = items;
            Count = count;
        }

        public IQueryable Items { get; set; }

        public long? Count { get; set; }
    }
}