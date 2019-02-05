using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace DNT.Extensions
{
    public static class FileExtensions
    {
        /// <summary>
        /// Writes the specified StringBuilder to the file using the specified path. 
        /// If the file already exists, it is overwritten.
        /// Example: 
        /// new StringBuilder("Hello World!!!").CopyToFile(@"c:\helloworld.txt");
        /// </summary>
        /// <param name="sBuilder"></param>
        /// <param name="path"></param>
        public static void CopyToFile(this StringBuilder sBuilder, string path)
        {
            File.WriteAllText(path, sBuilder.ToString());
        }

        /// <summary>
        /// Writes the specified String to the file using the specified path. 
        /// If the file already exists, it is overwritten.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="path"></param>
        public static void CopyToFile(this string s, string path)
        {
            File.WriteAllText(path, s);
        }

        /// <summary>
        /// Converts a file on a given path to a byte array.
        /// Example:
        /// var bytes = @"C:\Temp\Products.pdf".ToBytes();
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException(fileName);

            return File.ReadAllBytes(fileName);
        }

        /// <summary>
        /// Get the file size of a given filename.
        /// Example:
        /// string path = @"D:\WWW\Proj\web.config";
        /// Console.WriteLine("File Size is: {0} bytes.", path.FileSize());
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static long FileSize(this string filePath)
        {
            var oFileInfo = new FileInfo(filePath);
            var bytes = oFileInfo.Length;
            return bytes;
        }

        /// <summary>
        /// Nicely formatted file size. This method will return file size with bytes, KB, MB and GB in it. 
        /// You can use this alongside the Extension method named FileSize.
        /// Example: 
        /// Using another Extension Method: FileSize to get the size of the file
        /// string path = @"D:\WWW\Proj\web.config";
        /// Console.WriteLine("File Size is: {0}.", path.FileSize().FormatSize());
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns></returns>
        public static string FormatFileSize(this long fileSize)
        {
            string[] suffix = { "bytes", "KB", "MB", "GB" };
            long j = 0;

            while (fileSize > 1024 && j < 4)
            {
                fileSize = fileSize / 1024;
                j++;
            }
            return (fileSize + " " + suffix[j]);
        }

        /// <summary>
        /// Delete files in a folder that are like the searchPattern, don't include subfolders.
        /// Example: 
        /// DirectoryInfo di = new DirectoryInfo(@"c:\temp");
        /// di.DeleteFiles("*.xml");  // Delete all *.xml files 
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
        /// Example: 
        /// DirectoryInfo di = new DirectoryInfo(@"c:\temp");
        /// di.DeleteFiles("*.xml", true);  // Delete all, recursively
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
            var deleted = 0;
            foreach (FileInfo fi in di.GetFiles(searchPattern, includeSubdirs ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                fi.Delete();
                deleted++;
            }

            return deleted;
        }

        /// <summary>
        /// This extension method acts similarly to Directory.
        /// GetFiles except that the directory path is expressed as a virtual directory.
        /// Example:
        /// foreach (var file in GetFilesInVirtualDirectory("../Images")
        /// { // Do something interesting. }
        /// </summary>
        /// <param name="targetPage"></param>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetFilesInVirtualDirectory(this Page targetPage, string directoryPath)
       {
           return Directory.GetFiles(targetPage.Server.MapPath(directoryPath)).Select(Path.GetFileName);
       }

        /// <summary>
        /// List/Get all files in a specified folder using LINQ. Doesn't include sub-directory files.
        /// Example:
        /// path = @"C:\temp";
        /// List<![CDATA[<![CDATA[string>]]>]]>> files = path.ListFiles();
        /// </summary>
        /// <param name="folderPath"></param>
        /// <returns></returns>
        public static List<string> ListFiles(this string folderPath)
        {
            if (!Directory.Exists(folderPath)) return null;
            return (from f in Directory.GetFiles(folderPath)
                    select Path.GetFileName(f)).
            ToList();
        }
    }
}
