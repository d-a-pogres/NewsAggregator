using NewsAggregator.Patterns;

namespace NewsAggregator.Models;

/// <summary>
/// Пользователь хранит только собственную ленту и подписывается на события через NewsEventHub.
/// </summary>
public class NewsUser : INewsSubscriber
{
    private readonly List<News> _feed = new();

    public string Name { get; }
    public IReadOnlyList<News> Feed => _feed.AsReadOnly();

    public NewsUser(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Имя пользователя не может быть пустым.", nameof(name));

        Name = name;
    }

    public void Update(News news)
    {
        _feed.Add(news);
        Console.WriteLine($"  [Observer] Пользователь «{Name}» получил уведомление: «{news.Title}» от «{news.SourceName}».");
    }

    public void ClearFeed() => _feed.Clear();
}
