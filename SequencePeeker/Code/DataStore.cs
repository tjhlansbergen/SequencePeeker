/// <summary>
///  SequencePeeker
///  Tako Lansbergen
///  11-2018
///  
///  Data access class
/// </summary>

using System;
using System.IO;

namespace SequencePeeker.Code
{
    public static class DataStore
    {

        /// <summary>
        /// Returns wheter there is data available for the given ID
        /// </summary>
        public static bool HasData(Guid ID)
        {
            return Directory.Exists(Path.Combine(Common.App_Data, ID.ToString()));
        }

        /// <summary>
        /// Get the contents of the given FileType by ID
        /// </summary>
        public static string GetFileContents(Guid ID, PackageFileType file)
        {
            //get filename for type
            Common.PackageFiles.TryGetValue(file, out string filename);
            string path = Path.Combine(Common.App_Data, ID.ToString(), filename);

            //read and return the file
            return (File.Exists(path)) ? File.ReadAllText(path) : string.Empty;
        }

        /// <summary>
        /// Gets the path of the given FileType by ID
        /// </summary>
        public static string GetPath(Guid ID, PackageFileType file)
        {
            //get filename for type
            Common.PackageFiles.TryGetValue(file, out string filename);
            string path = Path.Combine(Common.App_Data, ID.ToString(), filename);

            //return path as string
            return (File.Exists(path)) ? Path.Combine(Common.App_Data, ID.ToString(), filename) : string.Empty;
        }

        /// <summary>
        /// Clear previously uploaded packages older than one hour
        /// </summary>
        public static void ClearCache()
        {
            foreach (var item in Directory.EnumerateDirectories(Common.App_Data))
            {
                if ((DateTime.UtcNow - Directory.GetCreationTimeUtc(item)).TotalHours > 1)
                    Directory.Delete(item, true);
            }
        }
    }
}