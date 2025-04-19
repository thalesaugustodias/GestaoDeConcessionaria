using GestaoDeConcessionaria.Application.DTOs;

namespace GestaoDeConcessionaria.Application.Extensions
{
    public static class EnumerableExtensions
    {       
        public static List<DataPoint> ToDataPoints<T>(
            this IEnumerable<T>? source,
            Func<T, string> keySelector)
        {
            if (source == null)
                return [];

            return [.. source
                .GroupBy(keySelector)
                .Select(g => new DataPoint(g.Key, g.Count()))];
        }
    }
}
