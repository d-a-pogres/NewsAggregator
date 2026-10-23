using NewsAggregator.Models;

namespace NewsAggregator.Patterns;

/// <summary>
/// Посредник событий: хранит подписки и рассылает новости подписчикам.
/// NewsSource не содержит список подписчиков.
/// </summary>
public class NewsEventHub
{
    private readonly Dictionary<string, HashSet<INewsSubscriber>> _subscribers =
        new(StringComparer.OrdinalIgnoreCase);

    public void Subscribe(string sourceName, INewsSubscriber subscriber)
    {
        if (!_subscribers.TryGetValue(sourceName, out var subscribers))
        {
            subscribers = new HashSet<INewsSubscriber>();
            _subscribers[sourceName] = subscribers;
        }

        if (subscribers.Add(subscriber))
            Console.WriteLine($"[Подписка] Пользователь подписан на источник «{sourceName}».");
        else
            Console.WriteLine($"[Подписка] Повторная подписка на «{sourceName}» не добавлена.");
    }

    public void Unsubscribe(string sourceName, INewsSubscriber subscriber)
    {
        if (_subscribers.TryGetValue(sourceName, out var subscribers) && subscribers.Remove(subscriber))
            Console.WriteLine($"[Отписка] Пользователь отписался от источника «{sourceName}».");
        else
            Console.WriteLine($"[Отписка] Активная подписка на «{sourceName}» не найдена.");
    }

    public void Publish(News news)
    {
        if (!_subscribers.TryGetValue(news.SourceName, out var subscribers) || subscribers.Count == 0)
        {
            Console.WriteLine($"  [Observer] У источника «{news.SourceName}» нет подписчиков.");
            return;
        }

        // Создаём копию набора, чтобы обход был безопаснее при изменении подписок в обработчике.
        foreach (var subscriber in subscribers.ToArray())
            subscriber.Update(news);
    }
}
