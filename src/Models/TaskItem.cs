namespace Models;

/// <summary>
/// Тестовое название
/// </summary>
public class TaskItem
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Title { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public string Description { get; set; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    public DateTime DueData { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsCompleted { get; set; } 
    /// <summary>
    /// 
    /// </summary>
    public bool IsOverDue => DueData < DateTime.Now && !IsCompleted;
    
    /// <summary>
    /// 
    /// </summary>
    public Priority Precedency { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public TaskItem () {}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="title"></param>
    /// <param name="dueData"></param>
    /// <param name="id"></param>
    /// <param name="priority"></param>
    /// <param name="description"></param>
    public TaskItem(string title, DateTime dueData, int id, Priority priority, string description)
    {
        Title = title;
        DueData = dueData;
        Id = id;
        Description = description;
        Precedency = priority;
        IsCompleted = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"==========\nЗадача №{Id}\nНазвание: {Title}\n-{Description}\nСрок: {DueData:dd}.{DueData:MM}.{DueData:yyyy}\nВажность: {Precedency}\nВыполнено: {(IsCompleted ? "✅": "❌")}\n{(IsOverDue ? "ПРОСРОЧЕНО\n": "")}==========";
    }
}