namespace Services;

using Models;


public class TaskManager
{
    private static int _idCounter;
    private readonly string _filepath;
    public List<TaskItem> Tasks { get; set;}
    public TaskManager(string filepath)
    {
        _filepath = filepath;
        Tasks = new List<TaskItem>();
    }

    public async Task Start()
    {
        var loadedTasks = await FileManager.LoadFileAsync<TaskItem>(_filepath);
        if (loadedTasks != null)
        {
            Tasks = loadedTasks;
        }
        else
        {
            Tasks = new List<TaskItem>();
        }
        if (Tasks.Any())
        {
            _idCounter = Tasks.Max(t => t.Id);
        }
        else
        {
            _idCounter = 0;
        }
    }

    public async Task AddAsync(string title, DateTime dueData, Priority priority,string description="Нет описания")
    {
        var task = new TaskItem(title, dueData,++_idCounter, priority, description);
        Tasks.Add(task);
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task RemoveAsync(int id)
    {
        Tasks.RemoveAt(Tasks.FindIndex(x=>x.Id==id));
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdateTitleAsync(int id, string title)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Title = title;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdateDataAsync(int id, DateTime data)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].DueData = data;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdateDescriptionAsync(int id, string description)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Description = description;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdateStatusAsync(int id)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].IsCompleted = !Tasks[Tasks.FindIndex(x=>x.Id==id)].IsCompleted;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdatePriorityAsync (int id, Priority priority)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Precedency = priority;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public List<TaskItem> Search(string prompt)
    {
        var title = Tasks.Where(x=>x.Title.Contains(prompt)).ToList<TaskItem>();
        var description = Tasks.Where(x=>x.Description.Contains(prompt)).ToList<TaskItem>();
        return title.Union(description).ToList<TaskItem>();
    }

    public List<TaskItem> GetAll()
    {
        return Tasks;
    }

    public TaskItem GetById(int id)
    {
        return Tasks.FirstOrDefault(x=>x.Id==id) ?? throw new KeyNotFoundException("Not found");
    }

    public List<TaskItem> GetByStatus(bool status)
    {
        return Tasks.Where(x=>x.IsCompleted==status).ToList() ?? throw new KeyNotFoundException("Not found");
    }

    public void GetByPriority()
    {
        
    }

    public string GetStatistics()
    {
        int count = Tasks.Count();
        int completed = Tasks.Where(x=>x.IsCompleted==true).Count();
        int uncompleted = Tasks.Where(x=>x.IsCompleted==false).Count();
        int overdue = Tasks.Where(x=>x.IsOverDue).Count();
        return $"Всего задач: {count}\nВыполнено: {completed}\nНевыполнено: {uncompleted}\nПросрочено: {overdue}";
    }

}