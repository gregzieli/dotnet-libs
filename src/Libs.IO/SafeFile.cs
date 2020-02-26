using System;
using System.IO;
using static Libs.IO.Constants;

namespace Libs.IO
{
    public static class SafeFile
    {
        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name.
        /// </summary>
        /// <param name="sourceFilePath">The name of the file to move. Can include a relative or absolute path.</param>
        /// <param name="destinationDirectoryPath">The new path to the destination directory.</param>
        /// <param name="fileName">The new name for the file.</param>
        /// <remarks>
        /// This method will create a directory at destination if it doesn't exist.
        /// If the file already exists at the destination, this method will move the new file with a timestamp appended to the name.
        /// </remarks>
        public static string Move(string sourceFilePath, string destinationDirectoryPath, string fileName)
        {
            if (!Directory.Exists(destinationDirectoryPath))
            {
                Directory.CreateDirectory(destinationDirectoryPath);
            }

            var destination = Path.Combine(destinationDirectoryPath, fileName);

            if (File.Exists(destination))
            {
                var fileNameOnly = Path.GetFileNameWithoutExtension(destination);
                var extension = Path.GetExtension(destination);

                destination = Path.Combine(destinationDirectoryPath, $"{fileNameOnly}_{DateTime.Now:yyyyMMddhhmmssfff}{extension}");
            }

            File.Move(sourceFilePath, destination);

            return destination;
        }

        /// <summary>
        /// Checks if the file is not locked by another process so it can be opened for exclusive access.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>status of the file.</returns>
        public static string GetStatus(string path)
        {
            try
            {
                using var inputStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);
                return inputStream.Length == 0 ? EmptyStream : OK;
            }
            catch (UnauthorizedAccessException)
            {
                return UnauthorizedAccess;
            }
            catch (ArgumentException)
            {
                return BadArgument;
            }
            catch (IOException)
            {
                return Constants.IOException;
            }
            catch (Exception)
            {
                return Unknown;
            }
        }
    }
}
