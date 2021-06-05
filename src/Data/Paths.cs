using System.IO;
using System.Reflection;

namespace Kallias.Data
{
    /// <summary>
    /// Storage for useful paths, you might want to use.
    /// </summary>
    public static class Paths
    {
        /// <summary>
        /// Get path for your base directory with project.
        /// This path should be a bit more platform independent.
        /// </summary>
        public static string SourceDir
            => Assembly.GetExecutingAssembly().Location
               + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
               + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
               + $"{Path.DirectorySeparatorChar}"; 
    }
}