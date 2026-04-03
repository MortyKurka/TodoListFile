Console.WriteLine("Приветствую! Это ваш Todo-лист с храненим задач в json");
var manager = new TaskManager();
await manager.LoadFileAsync("./Task.json");
string? input;
while(true)
{
    Console.WriteLine("Меню: ");
    Console.WriteLine("1.Показать все задачи");
    Console.WriteLine("2.Добавить задачу");
    Console.WriteLine("3.Редактировать задачу");
    Console.WriteLine("4.Удалить задачу");
    Console.WriteLine("5.Поиск");
    Console.WriteLine("6.Статистика");
    Console.WriteLine("7.Выход");
    Console.Write("> ");
    input = Console.ReadLine();
    switch (input)
    {
        case "1":
            foreach (var task in manager.GetAll())
            {
                Console.WriteLine("==========");
                Console.WriteLine($"Задача №{task.Id}");
                Console.WriteLine($"Название: {task.Title}");
                Console.WriteLine($"-{task.Description}");
                Console.WriteLine($"Срок: {task.DueData:dd}.{task.DueData:MM}.{task.DueData:yyyy}");
                Console.WriteLine($"Выполнено: {(task.IsCompleted ? "✅": "❌")}");
                Console.WriteLine("==========");
            }
            break;
        case "2":
            Console.WriteLine("Введите название, описание, срок выполнения(dd:mm:yyyy) и важность(Low, Medium, Hard) задачи");
            Console.Write("Название:> ");
            string? title = Console.ReadLine();
            Console.Write("Описание:> ");
            string? description = Console.ReadLine();
            Console.Write("Срок:> ");
            string? dueData = Console.ReadLine();
            await manager.Add(title, dueData, description);
            break;
        case "3":
            break;
        case "4":
            Console.WriteLine("Введите Id задачи которую хотите удалить");
            Console.Write("Id:> ");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            manager?.Remove(id);
            break;
        case "5":
            break;
        case "6":
            break;
        case "7":
            break;
        default:
            Console.WriteLine("Неизвестная комманда");
            break;
    }
}