namespace NewsAggregator.Models;

/// <summary>
/// Новость, опубликованная источником.
/// </summary>
public class News
{
    public string Title { get; }
    public string Text { get; }
    public string SourceName { get; }
    public DateTime PublishedAt { get; }
    public int Popularity { get; private set; }

    public News(string title, string text, string sourceName, DateTime publishedAt, int popularity = 0)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Заголовок новости не может быть пустым.", nameof(title));

        if (string.IsNullOrWhiteSpace(sourceName))
            throw new ArgumentException("Название источника не может быть пустым.", nameof(sourceName));

        if (popularity < 0)
            throw new ArgumentOutOfRangeException(nameof(popularity), "Популярность не может быть отрицательной.");

        Title = title;
        Text = text;
        SourceName = sourceName;
        PublishedAt = publishedAt;
        Popularity = popularity;
    }

    public void AddViews(int count = 1)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count), "Количество просмотров не может быть отрицательным.");

        Popularity += count;
    }

    public override string ToString()
        => $"[{PublishedAt:dd.MM.yyyy HH:mm}] {Title} | источник: {SourceName} | популярность: {Popularity}";
}
