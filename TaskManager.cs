using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Xml;

public class TaskManager
{
    private static int _idCounter;
    private string? _filepath;
    private List<TaskItem>? Tasks { get; set;}
    public TaskManager()
    {
    }

    public async Task LoadFileAsync(string filepath)
    {
        _filepath = filepath;
        using (FileStream fs = new FileStream(_filepath,FileMode.OpenOrCreate))
        {
            var options = new JsonSerializerOptions
            {   
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            if (fs.Length>0)
            {
                Tasks = await JsonSerializer.DeserializeAsync<List<TaskItem>>(fs);
                _idCounter = Tasks.Max(x=>x.Id);
            }
            else
            {
                Tasks = new List<TaskItem>();
            }
        }
    }
    public async Task AddAsync(string title, string dueData, string description="Нет описания")
    {
        var options = new JsonSerializerOptions
        {   
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        var task = new TaskItem(title, description);
        DateTime due;
        DateTime.TryParse(dueData, out due);
        task.Id = ++_idCounter;
        task.DueData = due;
        Tasks?.Add(task);
        using(var fs = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
    }

    public async Task RemoveAsync(int id)
    {
        var options = new JsonSerializerOptions
        {   
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        Tasks?.RemoveAt(Tasks.FindIndex(x=>x.Id==id));
        using(var fs = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
    }

    public async Task UpdateTitleAsync(int id, string title)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Title = title;
        var options = new JsonSerializerOptions
        {   
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        using(var fs = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
    }

    public async Task UpdateDataAsync(int id, string data)
    {
        DateTime due;
        DateTime.TryParse(data, out due);
        Tasks[Tasks.FindIndex(x=>x.Id==id)].DueData = due;
        var options = new JsonSerializerOptions
        {   
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        using(var fs = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
    }

    public async Task UpdateDescriptionAsync(int id, string description)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].Description = description;
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        using (FileStream fs = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
    }

    public async Task UpdateStatusAsync(int id)
    {
        Tasks[Tasks.FindIndex(x=>x.Id==id)].IsCompleted = !Tasks[Tasks.FindIndex(x=>x.Id==id)].IsCompleted;
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        using (FileStream fs = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
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


}