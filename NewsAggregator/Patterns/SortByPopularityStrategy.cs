using NewsAggregator.Models;

namespace NewsAggregator.Patterns;

public class SortByPopularityStrategy : INewsSortStrategy
{
    public string Name => "по популярности (сначала популярные)";

    public IEnumerable<News> Sort(IEnumerable<News> news)
        => news.OrderByDescending(item => item.Popularity)
                .ThenByDescending(item => item.PublishedAt);
}
