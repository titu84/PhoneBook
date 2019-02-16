using System;
using System.IO;

namespace Logger
{
    public class InfoLogger : Abstraction.Logger
    {
        public InfoLogger() : base($"{nameof(InfoLogger)}.txt") { }
        public InfoLogger(string fileName) : base(fileName) { }
        public override void Log(string text)
        {
            try
            {
                File.AppendAllText(Path.Combine(Dir, FileName),
                    $"{Environment.NewLine} Data: {DateTime.Now} Informacja: {text}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
