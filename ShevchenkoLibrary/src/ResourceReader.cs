namespace Shevchenko
{
    using System;
    using System.IO;
    using System.Reflection;
    
    public class ResourceReader
    {
        public static string ReadEmbeddedResource(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            // For some reason GenderDetection doesn't work with this:
            //var fullResourceName = $"{assembly.GetName().Name}.{resourceName}";  
            var fullResourceName = $"{resourceName}";

            var stream = assembly.GetManifestResourceStream(fullResourceName);
            
            if (stream == null)
            {
                throw new InvalidOperationException($"Resource '{resourceName}' by address '{fullResourceName}' was not found.");
            }
            
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}