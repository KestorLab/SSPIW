using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Collections.Generic;
#nullable enable
namespace SSPIW;

public class SSPIW
{
  //protected static string file_name1 { get; set; }
  public static List<byte> last_errors_code = new List<byte> { };
  //protected static object file_data1 { get; set; }

  public static byte Save(string file_name, object file_data, string path)
  {
    try
    {
      
      file_name = file_name.Trim();
      string normaldir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
      string tocnodir = Path.Combine(normaldir, file_name);
      try
      {
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
          Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
          WriteIndented = true
        };
        Directory.CreateDirectory(normaldir);
        string contents = JsonSerializer.Serialize<object>(file_data, serializerOptions);
        File.WriteAllText(tocnodir, contents);
        return 0;
      }
      catch
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("SSPIW: Ошибка сохранения!");
        Console.ResetColor();
        return 1;
      }
    }
    catch
    {
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("SSPIW: Ошибка передачи данных!");
      Console.ResetColor();
      return 2;
    }
  }

  public static T Load<T>(string path, string file_name)
  {
    try
    {
      string path2 = file_name.Trim();
      string tocnodir = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path), path2);
      if (File.Exists(tocnodir))
        return JsonSerializer.Deserialize<T>(File.ReadAllText(tocnodir), (JsonSerializerOptions) null);
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("SSPIW: Не найден файл!");
      Console.ResetColor();
      return default (T);
    }
    catch
    {
      Console.ForegroundColor = ConsoleColor.DarkRed;
      Console.WriteLine("SSPIW: Фатальная ошибка!");
      Console.ResetColor();
      return default (T);
    }
  }
  public static void SaveNeW(string file_name, object file_data, string path, bool error_write)
  {
     
    
    try
    {
      
      file_name = file_name.Trim();
      string normaldir;
      string directory = path; 
      if (string.IsNullOrEmpty(path)) { directory = AppDomain.CurrentDomain.BaseDirectory; }
      string directoryPath = Path.GetDirectoryName(path);
      if (string.IsNullOrEmpty(path))
      {
        normaldir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, file_name);
      }
      else
      {
        normaldir = Path.Combine(path, file_name);
      }
      
      try
      {
        JsonSerializerOptions serializerOptions = new JsonSerializerOptions()
        {
          Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
          WriteIndented = true
        };
        
        
        
        Directory.CreateDirectory(directory);
        string contents = JsonSerializer.Serialize<object>(file_data, serializerOptions);
        File.WriteAllText(normaldir, contents);
        
      }
      catch
      {
        if (error_write)
        {
          Console.ForegroundColor = ConsoleColor.DarkRed;
          Console.WriteLine("SSPIW: Ошибка сохранения!");
          last_errors_code.Insert(0, 1);
          Console.ResetColor();
        } else if (!error_write)
        {
          last_errors_code.Insert(0, 1);
        }
        
        
      }
    }
    catch
    {
      if (error_write)
      {
        Console.ForegroundColor = ConsoleColor.DarkRed;
        Console.WriteLine("SSPIW: Ошибка передачи данных!");
        Console.ResetColor();
        last_errors_code.Insert(0, 2);
      } else if (!error_write) {
        last_errors_code.Insert(0, 2);
      } 
      
       
      
    }
  }
}
