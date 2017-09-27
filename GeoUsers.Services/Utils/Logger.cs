using System;
using System.IO;

namespace GeoUsers.Services.Utils
{
    public static class Logger
    {
        public static void Log(Exception e)
        {
            var exceptionMessage = $"--- Exception thrown {DateTime.Now}";

            var path = Path.Combine(Environment.CurrentDirectory, "logs.txt");

            using (StreamWriter file = new StreamWriter(path, true))
            {
                file.WriteLine(exceptionMessage);
                file.WriteLine(e.Message);
                file.WriteLine(e.StackTrace);

                file.Close();
            }
        }
    }
}
