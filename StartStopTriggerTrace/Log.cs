using System;
using System.IO;

namespace StartStopTriggerTrace
{
    public class MessageLoggedEventArgs : EventArgs
    {
        public MessageLoggedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class Log
    {
        public delegate void MessageLoggedDelegate(string message);
        public event MessageLoggedDelegate MessageLogged;

        #region Singleton impementation
        private static readonly Lazy<Log> Lazy = new Lazy<Log>(() => new Log());

        private static FileStream logfilestream;
        private static StreamWriter log;

        public static Log Instance
        {
            get => Lazy.Value;
        }

        private Log()
        {
            logfilestream = new FileStream($"logfile{DateTime.Now.ToString("yyyyMMddHHmmssffff")}.txt", FileMode.Create, FileAccess.ReadWrite);
            log = new StreamWriter(logfilestream);
        }
        #endregion

        public void WriteLog(string message)
        {
            try
            {
                var t = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var tmessage = $"{t} : {message} {Environment.NewLine}";
                MessageLogged?.Invoke(tmessage);
                log.Write(tmessage);
                log.Flush();
            }
            catch(Exception)
            { }
        }
    }
}
