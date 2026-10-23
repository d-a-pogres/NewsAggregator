using NewsAggregator.Models;
using NewsAggregator.Patterns;

namespace NewsAggregator;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("======================================");
        Console.WriteLine("       ДЕМОНСТРАЦИЯ АГРЕГАТОРА НОВОСТЕЙ");
        Console.WriteLine("======================================");

        // Один общий центр событий хранит подписки отдельно от источников.
        var eventHub = new NewsEventHub();

        var techSource = new NewsSource("Технологии сегодня", eventHub);
        var scienceSource = new NewsSource("Наука рядом", eventHub);
        var citySource = new NewsSource("Городские новости", eventHub);

        var anna = new NewsUser("Анна");
        var boris = new NewsUser("Борис");
        var vera = new NewsUser("Вера");

        Console.WriteLine("\n--- 1. Оформление подписок ---");
        eventHub.Subscribe(techSource.Name, anna);
        eventHub.Subscribe(scienceSource.Name, anna);
        eventHub.Subscribe(techSource.Name, boris);
        eventHub.Subscribe(citySource.Name, boris);
        eventHub.Subscribe(scienceSource.Name, vera);

        Console.WriteLine("\n--- 2. Публикация новостей (Observer) ---");
        var n1 = techSource.Publish(
            "Вышло обновление популярного приложения",
            "В обновлении исправлены ошибки и добавлены новые функции.",
            new DateTime(2026, 10, 14, 10, 30, 0),
            popularity: 120);

        var n2 = scienceSource.Publish(
            "Учёные представили новый материал",
            "Материал может применяться при создании более лёгких устройств.",
            new DateTime(2026, 10, 13, 16, 15, 0),
            popularity: 75);

        var n3 = citySource.Publish(
            "В центре города откроют пешеходную зону",
            "Проект начнут реализовывать в следующем месяце.",
            new DateTime(2026, 10, 14, 8, 0, 0),
            popularity: 45);

        var n4 = techSource.Publish(
            "Объявлена дата выхода новой игры",
            "Разработчики показали трейлер и сообщили дату релиза.",
            new DateTime(2026, 10, 12, 19, 45, 0),
            popularity: 200);

        Console.WriteLine("\n--- 3. Просмотр персональных лент ---");
        var annaFeed = new NewsFeed(anna, new SortByDateStrategy());
        var borisFeed = new NewsFeed(boris, new SortByDateStrategy());
        var veraFeed = new NewsFeed(vera, new SortByDateStrategy());

        annaFeed.Show();
        borisFeed.Show();
        veraFeed.Show();

        Console.WriteLine("\n--- 4. Переключение сортировки (Strategy) ---");
        annaFeed.SetSortStrategy(new SortByPopularityStrategy());
        annaFeed.Show();

        annaFeed.SetSortStrategy(new SortBySourceStrategy());
        annaFeed.Show();

        Console.WriteLine("\n--- 5. Отписка от источника ---");
        eventHub.Unsubscribe(techSource.Name, anna);
        techSource.Publish(
            "Новая функция для заметок",
            "В приложении появился удобный режим организации заметок.",
            new DateTime(2026, 10, 14, 17, 0, 0),
            popularity: 30);

        Console.WriteLine("\n--- 6. Проверка ленты после отписки ---");
        annaFeed.Show();

        Console.WriteLine("\n--- 7. Подписка после публикации ---");
        eventHub.Subscribe(citySource.Name, vera);
        citySource.Publish(
            "В парке обновят освещение",
            "Работы планируется завершить до конца месяца.",
            new DateTime(2026, 10, 14, 18, 0, 0),
            popularity: 20);

        veraFeed.Show();

        Console.WriteLine("\n======================================");
        Console.WriteLine("Демонстрация завершена.");
        Console.WriteLine("======================================");
    }
}
