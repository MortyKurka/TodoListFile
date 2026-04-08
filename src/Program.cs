using Services;
using Models;

Console.Clear();
Console.WriteLine("Приветствую! Это ваш Todo-лист с храненим задач в json");
var manager = new TaskManager("./Task.json");
if (manager == null)
{
    Console.WriteLine("Ошибка запуска программы");
    return;
}
await manager.Start();
string? input;
while(true)
{
    if (manager == null)
    {
        Console.WriteLine("Ошибка работы программы");
        return;
    }      
    Console.WriteLine("Меню: ");
    Console.WriteLine("1.Показать все задачи");
    Console.WriteLine("2.Добавить задачу");
    Console.WriteLine("3.Редактировать задачу");
    Console.WriteLine("4.Удалить задачу");
    Console.WriteLine("5.Поиск");
    Console.WriteLine("6.Статистика");
    Console.WriteLine("7.Выход");
    input = SafeInput.GetString("> ");
    int id;
    switch (input)
    {
        case "1":
            Console.Clear();
            var tasks = manager.GetAll();
            if (!tasks.Any())
            {
                Console.WriteLine("Нет ни одной задачи!");
            }
            else
            {
                foreach (var task in tasks)
                {
                    Console.WriteLine(task);
                }
            }
            break;
        case "2":
            Console.Clear();
            Console.WriteLine("Введите название, описание, срок выполнения(dd.mm.yyyy) и важность(Low, Medium, High) задачи");
            string title = SafeInput.GetString("Название:> ");
            string description = SafeInput.GetString("Описание:> ");
            DateTime dueData = SafeInput.GetDate("Срок:> ");
            Priority precedency = SafeInput.GetPriority("Важность:> ");;
            await manager.AddAsync(title, dueData, precedency,description);
            break;
        case "3":
            Console.Clear();
            Console.WriteLine("Что хотите отредактировать:");
            Console.WriteLine("1.Название");
            Console.WriteLine("2.Описание");
            Console.WriteLine("3.Дату");
            Console.WriteLine("4.Важность");
            Console.WriteLine("5.Статус выполнения");
            string inp_choose_red = SafeInput.GetString("> ");
            id = SafeInput.GetInt("Введите Id задачи:> "); 
            switch(inp_choose_red)
            {
                case "1":
                    string new_title = SafeInput.GetString("Введите новое название:> ");
                    await manager.UpdateTitleAsync(id, new_title);
                    break;
                case "2":
                    string new_description = SafeInput.GetString("Введите новое  описание:> ");
                    await manager.UpdateDescriptionAsync(id, new_description);
                    break;
                case "3":
                    DateTime new_date = SafeInput.GetDate("Введите новый срок:> ");
                    await manager.UpdateDataAsync(id, new_date);
                    break;
                case "4":
                    Priority new_priority = SafeInput.GetPriority("Введите новую важность:> ");
                    await manager.UpdatePriorityAsync(id, new_priority);
                    break;
                case "5":
                    await manager.UpdateStatusAsync(id);
                    break;
            }
            break;
        case "4":
            Console.Clear();
            Console.WriteLine("Введите Id задачи которую хотите удалить");
            id = SafeInput.GetInt("Id:> ");
            manager?.RemoveAsync(id);
            break;
        case "5":
            Console.Clear();
            string inp_src = SafeInput.GetString("Введите шаблон для поиска:> ");
            var searchs = manager.Search(inp_src);
            Console.WriteLine($"Результат поиска по шаблону \"{inp_src}\"");
            if (!searchs.Any())
            {
                Console.WriteLine("Ничего не найдено");
            }
            else
            {
                foreach (var task in manager.Search(inp_src))
                {
                    Console.WriteLine(task);
                }
            } 
            break;
        case "6":
            Console.Clear();
            Console.WriteLine("Статистика: ");
            Console.WriteLine(manager.GetStatistics());
            break;
        case "7":
            Console.Clear();
            Console.WriteLine("До свидания!");
            return;
        default:
            Console.Clear();
            Console.WriteLine("Неизвестная комманда");
            break;
    }
}