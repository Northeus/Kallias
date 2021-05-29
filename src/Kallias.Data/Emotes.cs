using System.IO;
using System.Reflection;

namespace Kallias.Data
{
    private readonly string _path = 
        Assembly.GetExecutingAssembly().Location
        + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
        + $"{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}.."
        + $"{Path.DirectorySeparatorChar}Assets{Path.DirectorySeparatorChar}Emotes.json";

    public static class Emotes
    {
        
    }
}