namespace Services;

using Models;


public class TaskManager
{
    private static int _idCounter;
    private string? _filepath;
    public List<TaskItem>? Tasks { get; set;}
    public TaskManager(string filepath)
    {
        _filepath = filepath;
    }

    public async Task Start()
    {
        Tasks = await FileManager.LoadFileAsync<TaskItem>(_filepath);
    }

    public async Task AddAsync(string title, string dueData, string description="Нет описания")
    {
        var task = new TaskItem(title, description);
        DateTime due;
        DateTime.TryParse(dueData, out due);
        task.Id = ++_idCounter;
        task.DueData = due;
        task.IsOverDue = task.DueData>DateTime.Now;
        Tasks?.Add(task);
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task RemoveAsync(int id)
    {
        Tasks?.RemoveAt(Tasks.FindIndex(x=>x.Id==id));
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdateTitleAsync(int id, string title)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Title = title;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    public async Task UpdateDataAsync(int id, string data)
    {
        DateTime due;
        DateTime.TryParse(data, out due);
        Tasks[Tasks.FindIndex(x=>x.Id==id)].DueData = due;
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

    public List<TaskItem> Search(string prompt)
    {
        return Tasks.Where(x=>x.Title.Contains(prompt)).ToList<TaskItem>();
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