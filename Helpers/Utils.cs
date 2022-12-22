using CommunityToolkit.Maui.Alerts;
using Namiokai.Models;

namespace Namiokai.Helpers;

public static class Utils
{
    public static User ParseUser(string value) => Enum.Parse<User>(value);

    public static async Task<string> ReadFromCacheAsync(string fileName = "debts.txt")
    {
        var content = string.Empty;

        try
        {
            var path = Path.Combine(FileSystem.CacheDirectory, fileName);

            using StreamReader reader = new(path);
            content = await reader.ReadToEndAsync();
        }
        catch
        {
            // ignored
        }

        return content;
    }

    public static string ReadCredentials(string fileName)
    {
        try
        {
            using var stream = FileSystem.Current.OpenAppPackageFileAsync(fileName);
            using var streamRead = new StreamReader(stream.Result);
        
            return streamRead.ReadToEnd();
        }
        catch
        {
          // ignored
        }

        return string.Empty;
    }

    public static async Task<bool> WriteToCacheAsync(string content, string fileName = "debts.txt")
    {
        try
        {
            var path = Path.Combine(FileSystem.CacheDirectory, fileName);

            using StreamWriter streamWriter = new(path, false);
            await streamWriter.WriteAsync(content);

            return true;
        }
        catch
        {
            // ignored
        }

        return false;
    }


}
