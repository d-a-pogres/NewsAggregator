using NewsAggregator.Models;

namespace NewsAggregator.Patterns;

public class SortBySourceStrategy : INewsSortStrategy
{
    public string Name => "по источнику (по алфавиту)";

    public IEnumerable<News> Sort(IEnumerable<News> news)
        => news.OrderBy(item => item.SourceName, StringComparer.OrdinalIgnoreCase)
                .ThenByDescending(item => item.PublishedAt);
}
