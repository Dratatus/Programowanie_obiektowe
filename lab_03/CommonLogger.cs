using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_03
{
    class CommonLogger : ILogger
    {
        private ILogger[] loggers;

        public CommonLogger(ILogger[] loggers)
        {
            this.loggers = loggers;
        }
        public void Dispose() { }

        public void Log(params string[] messages)
        {
            for (int i = 0; i < loggers.Length; i++)
            {
                loggers[i].Log(messages);
            }
        }
    }

}
