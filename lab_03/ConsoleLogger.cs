using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_03
{
    class ConsoleLogger : WritterLogger
    {
        public ConsoleLogger()
        {
            writer = Console.Out;
        }
        public override void Dispose()
        {
            writer.Dispose();
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
