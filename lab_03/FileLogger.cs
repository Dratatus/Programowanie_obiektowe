using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_03
{
    class FileLogger: WritterLogger
    {
        private bool disposed;

        protected FileStream stream;

        public FileLogger(string path)
        {
            disposed = true;

            // path: C:\temp\test.txt
            stream = new FileStream(path, FileMode.Append);
            writer = new StreamWriter(stream, Encoding.UTF8);
        }

        ~FileLogger()
        {
            Dispose();
        }

        public override void Dispose()
        {
            if (disposed)
            {
                writer.Flush();
                stream.Dispose();
                writer.Dispose();
            }
        }

        public override void Log(params string[] messages)
        {
            writer.Write($"\n{DateTime.Now} : ");
            for (int i = 0; i < messages.Length; i++)
            {
                writer.Write($"{messages[i]} ");
            }
            writer.Flush();
        } 
    }
}
