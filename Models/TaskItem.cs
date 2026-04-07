using System.Reflection.PortableExecutable;

namespace Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime DueData { get; set; }
    public bool IsCompleted { get; set; } 
    public bool IsOverDue => DueData < DateTime.Now && !IsCompleted;
    
    public Priority Precedency { get; set; }

    public TaskItem () {}
    public TaskItem(string title, DateTime dueData, int id, Priority priority, string description)
    {
        Title = title;
        DueData = dueData;
        Id = id;
        Description = description;
        Precedency = priority;
        IsCompleted = false;
    }

    public override string ToString()
    {
        return $"==========\nЗадача №{Id}\nНазвание: {Title}\n-{Description}\nСрок: {DueData:dd}.{DueData:MM}.{DueData:yyyy}\nВажность: {Precedency}\nВыполнено: {(IsCompleted ? "✅": "❌")}\n{(IsOverDue ? "ПРОСРОЧЕНО\n": "")}==========";
    }
}