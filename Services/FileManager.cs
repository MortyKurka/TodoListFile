namespace Services;

using System.Text.Unicode;
using System.Text.Json;
using System.Text.Encodings.Web;

public static class FileManager
{
    public static async Task<List<T>?> LoadFileAsync<T>(string filepath)
    {
        var options = new JsonSerializerOptions
        {   
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        using(FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate))
        {
            if (fs.Length>0)
            {
                return await JsonSerializer.DeserializeAsync<List<T>>(fs);
            }
            else
            {
                return new List<T>();
            }
        }
    }

    public static async Task SaveFileAsync<T>(string filepath, List<T> Tasks)
    {
        var options = new JsonSerializerOptions
        {   
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true
        };
        using(var fs = new FileStream(filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync(fs, Tasks, options);
        }
    }
}