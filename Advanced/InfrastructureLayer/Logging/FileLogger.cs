using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced.InfrastructureLayer.Logging
{
    public class FileLogger
    {
        private readonly string _logFilePath = "log.txt";
        public void Log(string message)
        {
            File.AppendAllText(_logFilePath, $"{message}\n");
        }
    }
}
