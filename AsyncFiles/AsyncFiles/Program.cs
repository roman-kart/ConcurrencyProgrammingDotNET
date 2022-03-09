using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncFiles
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string filePath = @".\text.txt";
            string text = $"Hello, Async. Current datetime utc: {DateTime.UtcNow}{Environment.NewLine}";

            await WriteTextAsync(filePath, text);
        }
        static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] encodedText = Encoding.UTF8.GetBytes(text);

            //using var sourceStream =
            //    new FileStream(
            //        filePath,
            //        FileMode.OpenOrCreate, FileAccess.Write, FileShare.None,
            //        bufferSize: 4096, useAsync: true);

            await File.AppendAllTextAsync(filePath, text, Encoding.UTF8);
            // await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
        }
    }
}
