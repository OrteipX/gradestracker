using System;
using System.IO;

namespace GradesTracker.Logic
{
    public static class FileManip
    {
        public static void CreateFile(string path)
        {
            FileStream stream = File.Create(path);
            stream.Close();
        }
    }
}
