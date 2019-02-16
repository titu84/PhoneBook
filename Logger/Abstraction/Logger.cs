using System;
using System.IO;

namespace Logger.Abstraction
{
    public abstract class Logger
    {
        protected readonly string FileName;
        protected static string Dir;

        protected Logger(string fileName)
        {
            FileName = fileName;
            setDirectory();
        }
        private void setDirectory()
        {
            Dir = Dir ?? System.Configuration.ConfigurationManager.AppSettings["dir"];
            if (Dir == null)          
                throw new Exception("Add 'dir' key with value '[you're directory for log files]' to Web.config file");          
            CreateDirectory();
        }
        private static void CreateDirectory()
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
        }
        public abstract void Log(string text);
    }
}
