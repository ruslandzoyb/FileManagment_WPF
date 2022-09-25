using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Infrastructure
{
    public class FileContentWriter
    {
        public void CreateFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                using (var file = File.Create(filePath))
                {
                }
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("File with such path doesn't exist");
                }
            }
        }

        public void WriteToFile(string path, string text)
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(text))
            {
                using (var writer = new StreamWriter(path))
                {
                    writer.Write(text);
                }
            }

        }
    }
}