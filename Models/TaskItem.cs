using System.Reflection.PortableExecutable;

namespace Models;

public class TaskItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueData { get; set; }
    public bool IsCompleted { get; set; } 
    public bool IsOverDue { get; set; } 

    public TaskItem(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public override string ToString()
    {
        return $"==========\nЗадача №{Id}\nНазвание: {Title}\n-{Description}\nСрок: {DueData:dd}.{DueData:MM}.{DueData:yyyy}\nВыполнено: {(IsCompleted ? "✅": "❌")}\n==========";
    }
}