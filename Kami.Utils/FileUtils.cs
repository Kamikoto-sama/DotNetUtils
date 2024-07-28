using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kami.Utils;

public static class FileUtils
{
    public static IEnumerable<string> Walk(string path)
    {
        var files = Directory.GetFiles(path).Concat(Directory.GetDirectories(path).SelectMany(Walk));
        foreach (var file in files)
            yield return file;
    }
}