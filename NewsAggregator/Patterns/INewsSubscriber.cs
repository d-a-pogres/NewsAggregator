using NewsAggregator.Models;

namespace NewsAggregator.Patterns;

/// <summary>
/// Интерфейс получателя уведомлений в паттерне Observer.
/// </summary>
public interface INewsSubscriber
{
    void Update(News news);
}
