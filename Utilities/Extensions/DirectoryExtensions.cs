using System.IO;
using System.Linq;

namespace DNT.Extensions
{
    public static class DirectoryExtensions
    {
        /// <summary>
        /// Delete files in a folder that are like the searchPattern, don't include subfolders.
        /// Example : 
        /// DirectoryInfo di = new DirectoryInfo(@"c:\temp");
        /// di.DeleteFiles("*.xml");  // Delete all *.xml files 
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <returns>Number of files that have been deleted.</returns>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern)
        {
            return DeleteFiles(di, searchPattern, false);
        }

        /// <summary>
        /// Delete files in a folder that are like the searchPattern
        /// Example : 
        /// DirectoryInfo di = new DirectoryInfo(@"c:\temp");
        /// di.DeleteFiles("*.xml", true);  // Delete all, recursively
        /// </summary>
        /// <param name="di"></param>
        /// <param name="searchPattern">DOS like pattern (example: *.xml, ??a.txt)</param>
        /// <param name="includeSubdirs"></param>
        /// <returns>Number of files that have been deleted.</returns>
        /// <remarks>
        /// This function relies on DirectoryInfo.GetFiles() which will first get all the FileInfo objects in memory. This is good for folders with not too many files, otherwise
        /// an implementation using Windows APIs can be more appropriate. I didn't need this functionality here but if you need it just let me know.
        /// </remarks>
        public static int DeleteFiles(this DirectoryInfo di, string searchPattern, bool includeSubdirs)
        {
            int deleted = 0;
            foreach (FileInfo fi in di.GetFiles(searchPattern, includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                fi.Delete();
                deleted++;
            }

            return deleted;
        }

        /// <summary>
        /// Get Size Of Directory.
        /// Example : 
        /// DirectoryInfo WindowsDir = new DirectoryInfo(@"C:\WINDOWS");
        /// long WindowsSize = WindowsDir.GetSize();
        /// </summary>
        /// <param name="dir"></param>
        /// <returns>Size Of Directory.</returns>
        public static long GetSize(this DirectoryInfo dir)
        {
            var length = dir.GetFiles().Sum(nextfile => nextfile.Exists ? nextfile.Length : 0);
            length += dir.GetDirectories().Sum(nextdir => nextdir.Exists ? GetSize(nextdir) : 0);
            return length;
        }

        /// <summary>
        /// Recursively create directory.
        /// Raya Farayand Method, Example : 
        /// string path = @"C:\temp\one\two\three";
        /// var dir = new DirectoryInfo(path);
        /// dir.CreateDirectory();
        /// </summary>
        /// <param name="dirInfo">Folder path to create.</param>
        public static void CreateDirectory(this DirectoryInfo dirInfo)
        {
            if (dirInfo.Parent != null) CreateDirectory(dirInfo.Parent);
            if (!dirInfo.Exists) dirInfo.Create();
        }

    }
}
