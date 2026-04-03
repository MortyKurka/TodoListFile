using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
    public async Task Add(string title, string dueData, string description="Нет описания")
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

    public async Task Remove(int id)
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

    public void Update()
    {
        
    }

    public List<TaskItem> GetAll()
    {
        return Tasks;
    }

    public void GetById()
    {
        
    }

    public void GetByStatus()
    {
        
    }

    public void GetByPriority()
    {
        
    }


}