using NewsAggregator.Patterns;

namespace NewsAggregator.Models;

/// <summary>
/// Источник публикует новости и уведомляет подписчиков через отдельный объект.
/// Сам источник не хранит список пользователей.
/// </summary>
public class NewsSource
{
    private readonly NewsEventHub _eventHub;

    public string Name { get; }

    public NewsSource(string name, NewsEventHub eventHub)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Название источника не может быть пустым.", nameof(name));

        Name = name;
        _eventHub = eventHub;
    }

    public News Publish(string title, string text, DateTime publishedAt, int popularity = 0)
    {
        var news = new News(title, text, Name, publishedAt, popularity);
        Console.WriteLine($"\n[Источник «{Name}»] Опубликована новость: «{news.Title}».");
        _eventHub.Publish(news);
        return news;
    }
}
