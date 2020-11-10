using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness_Tracker.Data.QueryObjects
{
    public static class GenericPagingQuery
    {
        public static IQueryable<T> Page<T>(
            this IQueryable<T> query,
            int pageNumZeroStart, int pageSize)
        {
            if (pageSize == 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (pageNumZeroStart != 0)
            {
                query = query
                    .Skip(pageNumZeroStart * pageSize);
            }

            return query.Take(pageSize);
        }
    }
}
