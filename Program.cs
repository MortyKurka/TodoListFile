using Services;

Console.Clear();
Console.WriteLine("Приветствую! Это ваш Todo-лист с храненим задач в json");
var manager = new TaskManager("./Task.json");
manager.Start();
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
            Console.Clear();
            foreach (var task in manager.GetAll())
            {
                Console.WriteLine(task);
            }
            break;
        case "2":
            Console.Clear();
            Console.WriteLine("Введите название, описание, срок выполнения(dd:mm:yyyy) и важность(Low, Medium, Hard) задачи");
            Console.Write("Название:> ");
            string? title = Console.ReadLine();
            Console.Write("Описание:> ");
            string? description = Console.ReadLine();
            Console.Write("Срок:> ");
            string? dueData = Console.ReadLine();
            await manager.AddAsync(title, dueData, description);
            break;
        case "3":
            Console.Clear();
            Console.WriteLine("Что хотите отредактировать:");
            Console.WriteLine("1.Название");
            Console.WriteLine("2.Описание");
            Console.WriteLine("3.Дату");
            Console.WriteLine("4.Статус выполнения");
            Console.Write("> ");
            string? inp_choose_red = Console.ReadLine();
            int inp_id;
            string? inp_red;
            Console.Clear();
            switch(inp_choose_red)
            {
                case "1":
                    Console.Write("Введите Id задачи:> ");
                    int.TryParse(Console.ReadLine(), out inp_id);
                    Console.Write("Введите новое название: ");
                    inp_red = Console.ReadLine();
                    await manager.UpdateTitleAsync(inp_id, inp_red);
                    break;
                case "2":
                    Console.Write("Введите Id задачи:> ");
                    int.TryParse(Console.ReadLine(), out inp_id);
                    Console.Write("Введите новое описание: ");
                    inp_red = Console.ReadLine();
                    await manager.UpdateDescriptionAsync(inp_id, inp_red);
                    break;
                case "3":
                    Console.Write("Введите Id задачи:> ");
                    int.TryParse(Console.ReadLine(), out inp_id);
                    Console.Write("Введите новый срок: ");
                    inp_red = Console.ReadLine();
                    await manager.UpdateDataAsync(inp_id, inp_red);
                    break;
                case "4":
                    Console.Write("Введите Id задачи:> ");
                    int.TryParse(Console.ReadLine(), out inp_id);
                    await manager.UpdateStatusAsync(inp_id);
                    break;
            }
            break;
        case "4":
            Console.Clear();
            Console.WriteLine("Введите Id задачи которую хотите удалить");
            Console.Write("Id:> ");
            int id;
            int.TryParse(Console.ReadLine(), out id);
            manager?.RemoveAsync(id);
            break;
        case "5":
            Console.Clear();
            Console.Write("Введите шаблон для поиска:> ");
            string inp_src = Console.ReadLine();
            foreach (var task in manager.Search(inp_src))
            {
                Console.WriteLine(task);
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