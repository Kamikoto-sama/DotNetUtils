using System.IO;

namespace Utils;

public static class StreamUtils
{
    public static byte[] ToArray(this Stream stream, long offset = 0)
    {
        var count = stream.Length > int.MaxValue ? int.MaxValue : (int)stream.Length;
        var buffer = new byte[count];

        if (stream.CanSeek)
            stream.Seek(offset, SeekOrigin.Begin);

        stream.Read(buffer, 0, buffer.Length);
        return buffer;
    }
}