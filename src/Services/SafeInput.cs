namespace Services;

using Models;

/// <summary>
/// 
/// </summary>
public static class SafeInput
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static string GetString(string prompt)
    {
        Console.Write(prompt);
        string? result = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(result))
        {
            Console.WriteLine("Ошибка: Пустой ввод!");
            Console.Write(prompt);
            result = Console.ReadLine();
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static int GetInt(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        int result;
        while(!int.TryParse(input, out result))
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: Пустой ввод");
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат");
            }
            Console.Write(prompt);
            input = Console.ReadLine();
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static Priority GetPriority(string prompt)
    {
        Priority result;
        Console.Write(prompt);
        string? input = Console.ReadLine();
        while (!Enum.TryParse<Priority>(input, out result))
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: Пустой ввод");
            }
            else
            {
                Console.WriteLine("Ошибка: Неверный формат");
            }
            Console.Write(prompt);
            input = Console.ReadLine();
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    public static DateTime GetDate(string prompt)
    {
        DateTime result;
        string? input;
        while(true)
        {
            Console.Write(prompt);
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Ошибка: Пустой ввод");
                continue;
            }
            if (DateTime.TryParse(input, out result))
            {
                return result; 
            }
            Console.WriteLine("Ошибка: Неверный формат");   
        }
    }
}



