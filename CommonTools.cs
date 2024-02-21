using System;
using System.IO;
using System.Net;


namespace UralicNLP
{
    /**
     * Shared methods
     */
    public class CommonTools
    {
        /**
         * Deletes a directory and its contents
         * @param dirPath Directory to be deleted
         */
        public static void DeleteDir(string dirPath)
        {
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            if (dir.Exists)
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subDir in dirs)
                {
                    DeleteDir(subDir.FullName);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
            }

            dir.Delete(false);
        }

        /**
         * Downloads a text file and returns it as a String
         * @param targetURL URL of the text file
         * @return The contents of the file as a string
         * @throws Exception Fails if the file cannot be downloaded
         */
        public static string ReadToString(string targetURL)
        {
            using (WebClient client = new WebClient())
            {
                string content = client.DownloadString(targetURL);
                return content.Trim();
            }
        }

        /**
         * Downloads a file from the internet and saves it to the disk
         * @param url URL of the file
         * @param filePath Path where to save the file
         * @param showProgress true to print out a progress bar
         * @throws Exception May fail if the URL cannot be reached or the file cannot be written
         */
        public static void DownloadToFile(string url, string filePath, bool showProgress)
        {
            using (WebClient client = new WebClient())
            {
                if (showProgress)
                {
                    // Assuming ShellProgressBar is used for progress indication in C#
                    IProgressBar progressBar = new ConsoleProgressBar(100);

                        client.DownloadProgressChanged += (sender, e) =>
                        {
                            progressBar.ShowProgress(e.ProgressPercentage);
                        };

                        client.DownloadFile(url, filePath);
                    
                }
                else
                {
                    Console.WriteLine("Not showing download progress");
                    client.DownloadFile(url, filePath);
                }
            }
        }
    }

    public class ConsoleProgressBar : IProgressBar
 {
     private const ConsoleColor ForeColor = ConsoleColor.Green;
     private const ConsoleColor BkColor = ConsoleColor.Gray;
     private const int DefaultWidthOfBar = 32;
     private const int TextMarginLeft = 3;

     private readonly int _total;
     private readonly int _widthOfBar;

     public ConsoleProgressBar(int total, int widthOfBar = DefaultWidthOfBar)
     {
         _total = total;
         _widthOfBar = widthOfBar;
     }

     private bool _intited;
     public void Init()
     {
         _lastPosition = 0;

         //Draw empty progress bar
         Console.CursorVisible = false;
         Console.CursorLeft = 0;
         Console.Write("["); //start
         Console.CursorLeft = _widthOfBar;
         Console.Write("]"); //end
         Console.CursorLeft = 1;

         //Draw background bar
         for (var position = 1; position < _widthOfBar; position++) //Skip the first position which is "[".
         {
             Console.BackgroundColor = BkColor;
             Console.CursorLeft = position;
             Console.Write(" ");
         }
     }

     public void ShowProgress(int currentCount)
     {
         if (!_intited)
         {
             Init();
             _intited = true;
         }
         DrawTextProgressBar(currentCount);
     }

     private int _lastPosition;

     public void DrawTextProgressBar(int currentCount)
     {
         //Draw current chunk.
         var position = currentCount * _widthOfBar / _total;
         if (position != _lastPosition)
         {
             _lastPosition = position;
             Console.BackgroundColor = ForeColor;
             Console.CursorLeft = position >= _widthOfBar ? _widthOfBar - 1 : position;
             Console.Write(" ");
         }

         //Draw totals
         Console.CursorLeft = _widthOfBar + TextMarginLeft;
         Console.BackgroundColor = ConsoleColor.Black;
         Console.Write(currentCount + " of " + _total + "    "); //blanks at the end remove any excess
     }
 }

 public interface IProgressBar
 {
     public void ShowProgress(int currentCount);
 }
}
