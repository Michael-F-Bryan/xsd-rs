using System;
using System.IO;

namespace XsdCodegen
{
    public class Generator
    {
        public string TopLevelDocument { get; set; } = "File";

        public void Generate(Stream output, Schema schema)
        {
            throw new NotImplementedException();
        }
    }
}
