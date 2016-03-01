using System;
using System.Collections.Generic;

namespace StringAnalyzer.Helper
{
    public static class Extensions
    {
        public static IEnumerable<T> ReportProgress<T>(this IEnumerable<T> source, int interval, Action<int> callback)
        {
            if (interval == 0)
            {
                interval = 1;
            }

            var count = 0;
            foreach (var item in source)
            {
                yield return item;
                if (++count % interval == 0)
                {
                    callback(count);
                }
            }
        }
    }
}