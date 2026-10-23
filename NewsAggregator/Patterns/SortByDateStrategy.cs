using NewsAggregator.Models;

namespace NewsAggregator.Patterns;

public class SortByDateStrategy : INewsSortStrategy
{
    public string Name => "по дате (сначала новые)";

    public IEnumerable<News> Sort(IEnumerable<News> news)
        => news.OrderByDescending(item => item.PublishedAt);
}
