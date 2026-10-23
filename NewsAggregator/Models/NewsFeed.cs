using NewsAggregator.Patterns;

namespace NewsAggregator.Models;

/// <summary>
/// Пользовательская лента. Сортировку можно менять во время работы.
/// </summary>
public class NewsFeed
{
    private readonly NewsUser _user;
    private INewsSortStrategy _sortStrategy;

    public NewsFeed(NewsUser user, INewsSortStrategy sortStrategy)
    {
        _user = user;
        _sortStrategy = sortStrategy;
    }

    public void SetSortStrategy(INewsSortStrategy sortStrategy)
    {
        _sortStrategy = sortStrategy;
        Console.WriteLine($"\n[Strategy] Способ сортировки изменён: {_sortStrategy.Name}.");
    }

    public void Show()
    {
        Console.WriteLine($"\n===== Лента пользователя «{_user.Name}» — сортировка {_sortStrategy.Name} =====");
        var sortedNews = _sortStrategy.Sort(_user.Feed).ToList();

        if (sortedNews.Count == 0)
        {
            Console.WriteLine("Лента пока пуста.");
            return;
        }

        for (int i = 0; i < sortedNews.Count; i++)
        {
            var news = sortedNews[i];
            Console.WriteLine($"{i + 1}. {news}");
            Console.WriteLine($"   {news.Text}");
        }
    }
}
