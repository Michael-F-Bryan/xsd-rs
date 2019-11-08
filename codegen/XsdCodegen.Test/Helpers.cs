using System;
using System.IO;
using System.Linq;
using System.Text;

namespace XsdCodegen.Test
{
    internal static class Helpers
    {
        public static string XsdSchema => GetResource("XMLSchema.xsd");
        public static string Simple => GetResource("simple.xsd");

        public static string GetResource(string name)
        {
            var asm = typeof(Helpers).Assembly;
            var names = asm.GetManifestResourceNames();

            var fullName = names.First(n => n.EndsWith(name));
            var resourceStream = asm.GetManifestResourceStream(fullName);

            if (resourceStream == null)
            {
                var prettyNames = string.Join(", ", names.Select(n => '"' + n + '"'));
                throw new ArgumentException($"Unable to find \"{name}\" among [{prettyNames}]", nameof(name));
            }

            using (var reader = new StreamReader(resourceStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static string ProjectRoot()
        {
            var entry = new DirectoryInfo(Environment.CurrentDirectory);

            while (entry != null && entry != entry.Root)
            {
                if (entry.GetDirectories().Any(dir => dir.Name == ".git"))
                {
                    return entry.FullName;
                }

                entry = entry.Parent;
            }

            throw new Exception("Unable to find the solution directory");
        }
    }
}
