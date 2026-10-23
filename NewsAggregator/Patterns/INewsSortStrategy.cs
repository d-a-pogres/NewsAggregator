using NewsAggregator.Models;

namespace NewsAggregator.Patterns;

/// <summary>
/// Общий интерфейс алгоритма сортировки ленты (паттерн Strategy).
/// </summary>
public interface INewsSortStrategy
{
    string Name { get; }
    IEnumerable<News> Sort(IEnumerable<News> news);
}
