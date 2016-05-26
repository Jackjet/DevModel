using System.Collections.Generic;

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
}