namespace Services;

using Models;

/// <summary>
/// 
/// </summary>
public class TaskManager
{
    /// <summary>
    /// 
    /// </summary>
    private static int _idCounter;
    /// <summary>
    /// 
    /// </summary>
    private readonly string _filepath;
    /// <summary>
    /// 
    /// </summary>
    public List<TaskItem> Tasks { get; set;}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filepath"></param>
    public TaskManager(string filepath)
    {
        _filepath = filepath;
        Tasks = new List<TaskItem>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="dueData"></param>
    /// <param name="priority"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public async Task AddAsync(string title, DateTime dueData, Priority priority,string description="Нет описания")
    {
        var task = new TaskItem(title, dueData,++_idCounter, priority, description);
        Tasks.Add(task);
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task RemoveAsync(int id)
    {
        Tasks.RemoveAt(Tasks.FindIndex(x=>x.Id==id));
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    public async Task UpdateTitleAsync(int id, string title)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Title = title;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task UpdateDataAsync(int id, DateTime data)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].DueData = data;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public async Task UpdateDescriptionAsync(int id, string description)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Description = description;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task UpdateStatusAsync(int id)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].IsCompleted = !Tasks[Tasks.FindIndex(x=>x.Id==id)].IsCompleted;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="priority"></param>
    /// <returns></returns>
    public async Task UpdatePriorityAsync (int id, Priority priority)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Precedency = priority;
        await FileManager.SaveFileAsync(_filepath, Tasks);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public List<TaskItem> Search(string prompt)
    {
        var title = Tasks.Where(x=>x.Title.Contains(prompt)).ToList<TaskItem>();
        var description = Tasks.Where(x=>x.Description.Contains(prompt)).ToList<TaskItem>();
        return title.Union(description).ToList<TaskItem>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public List<TaskItem> GetAll()
    {
        return Tasks;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public TaskItem GetById(int id)
    {
        return Tasks.FirstOrDefault(x=>x.Id==id) ?? throw new KeyNotFoundException("Not found");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    public List<TaskItem> GetByStatus(bool status)
    {
        return Tasks.Where(x=>x.IsCompleted==status).ToList() ?? throw new KeyNotFoundException("Not found");
    }

    /// <summary>
    /// 
    /// </summary>
    public void GetByPriority()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string GetStatistics()
    {
        int count = Tasks.Count();
        int completed = Tasks.Where(x=>x.IsCompleted==true).Count();
        int uncompleted = Tasks.Where(x=>x.IsCompleted==false).Count();
        int overdue = Tasks.Where(x=>x.IsOverDue).Count();
        return $"Всего задач: {count}\nВыполнено: {completed}\nНевыполнено: {uncompleted}\nПросрочено: {overdue}";
    }

}