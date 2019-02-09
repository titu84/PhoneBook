using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{  
    public class ErrorLogger : Abstraction.Logger
    {
        public ErrorLogger() : base($"{nameof(ErrorLogger)}.txt") { }
        public ErrorLogger(string fileName) : base(fileName) { }
        public override void Log(string text)
        {
            {
                try
                {
                    File.AppendAllText(Path.Combine(Dir, FileName),
                        $"{Environment.NewLine} Data: {DateTime.Now} Błąd: {text}");
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
