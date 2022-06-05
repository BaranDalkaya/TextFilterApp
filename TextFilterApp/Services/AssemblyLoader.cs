using Application.Services.Interfaces;
using System.Reflection;

namespace Application.Services
{
    public class AssemblyLoader : IAssemblyLoader
    {
        /// <summary>
        /// Loads an embedded resource from the executing assembly
        /// </summary>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        public Stream? LoadEmbeddedResource(string resourceName)
        {
            return Assembly.GetExecutingAssembly()?.GetManifestResourceStream(resourceName);
        }

    }
}
