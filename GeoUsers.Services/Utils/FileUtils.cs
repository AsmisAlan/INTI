using GeoUsers.Services.Properties;
using System;
using System.IO;

namespace GeoUsers.Services.Utils
{
    public class FileUtils
    {
        public static string GetBase64EncodedFileBytes(string path)
        {
            var fullPath = GetFullFilePath(path);

            var fileData = SafelyExecuteIOOperation(() =>
            {
                return File.ReadAllBytes(fullPath);
            });

            return Convert.ToBase64String(fileData);
        }

        public static string SaveNewFile(string extension, byte[] data)
        {
            var fileName = $"{Guid.NewGuid().ToString()}.{extension}";

            var fullPath = GetFullFilePath(fileName);

            SafelyExecuteIOOperation(() =>
           {
               File.WriteAllBytes(fullPath, data);

               return true;
           });

            return fileName;
        }

        public static void DeleteFile(string path)
        {
            var fullPath = GetFullFilePath(path);

            SafelyExecuteIOOperation(() =>
            {
                File.Delete(fullPath);

                return true;
            });
        }

        public static string GetFullFilePath(string path)
        {
            return Path.Combine(Settings.Default.AttachmentFolder, path);
        }

        private static T SafelyExecuteIOOperation<T>(Func<T> operation)
        {
            try
            {
                return operation();
            }
            catch (PathTooLongException e)
            {
                throw new Exception("La ruta del archivo es demasiado larga", e);
            }
            catch (DirectoryNotFoundException e)
            {
                throw new Exception("La ruta del archivo es inexistente", e);
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception("No tiene permisos para modificar el directorio del archivo", e);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}
