using System;

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

        public static Log Instance
        {
            get => Lazy.Value;
        }

        private Log()
        {
        }
        #endregion

        public void WriteLog(string message)
        {
            try
            {
                MessageLogged?.Invoke(message + Environment.NewLine);
            }
            catch(Exception)
            { }
        }
    }
}
