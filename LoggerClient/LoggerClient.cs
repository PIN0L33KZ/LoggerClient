using System;
using System.Collections.Generic;

namespace LoggerClient
{
    public class LoggerClient
    {
        public string LogPath { get; set; }
        private readonly List<string> LogBook = new List<string>();

        public LoggerClient(string path)
        {
            LogPath = path;
        }

        public void AddLogEntry( LogLevel logLevel, string logAction, bool useTimeStamp )
        {
            if( useTimeStamp )
            {
                string time = DateTime.Now.ToShortTimeString();
                string date = DateTime.Now.ToShortDateString();
                LogBook.Add( $"{date}, {time} <{logLevel}> : {logAction}" );
            }
            else
            {
                LogBook.Add( $"<{logLevel}> : {logAction}" );
            }
        }

        public void RemoveLogEntryAt( int index )
        {
            LogBook.RemoveAt( index );
        }

        public void PrintLogbook()
        {
            for(int i = 0; i < LogBook.Count; i++)
            {
                Console.WriteLine( LogBook[i] );
            }
        }

        public void PrintLogEntry(int index)
        {
            Console.WriteLine( LogBook[index] );
        }

        public void AddSeparator(string title)
        {
            LogBook.Add( $"----------[{title}]----------" );
        }

        public void AddSeparator()
        {
            LogBook.Add( "-------------------" );
        }

        public string GetLogEntry(int index)
        {
            return LogBook[ index ];
        }

        public List<string> GetLogbook()
        {
            return LogBook;
        }

        public List<string> GetLogbookFilterd(LogLevel logLevel)
        {
            List<string> filterdList = new List<string>();

            for(int i = 0; i < LogBook.Count; i++ )
            {
                if( LogBook[i].Contains(logLevel.ToString()) )
                {
                    filterdList.Add( LogBook[i] );
                }
            }

            return filterdList;
        }

        public List<string> GetLogbookFilterd(LogLevel logLevel1, LogLevel loglevel2)
        {
            List<string> filterdList = new List<string>();

            for(int i = 0; i < LogBook.Count; i++ )
            {
                if( LogBook[i].Contains(logLevel1.ToString()) || LogBook[i].Contains(loglevel2.ToString()) )
                {
                    filterdList.Add( LogBook[ i ] );
                }
            }

            return filterdList;
        }

        public void WriteLogbookToFile()
        {
            using System.IO.StreamWriter writer = new System.IO.StreamWriter( LogPath, true, System.Text.Encoding.Default );
            
            writer.Write( LogBook );
            writer.Dispose();
        }

        public async void WriteLogBookToFileAsync()
        {
            using System.IO.StreamWriter writer = new System.IO.StreamWriter( LogPath, true, System.Text.Encoding.Default );
            
            for( int i = 0; i < LogBook.Count; i++ )
            {
                await writer.WriteAsync( LogBook[ i ] );
            }

            writer.Dispose();
        }

        public void OverwriteToFile()
        {
            using System.IO.StreamWriter writer = new System.IO.StreamWriter( LogPath, false, System.Text.Encoding.Default );
            writer.Write( LogBook );
            writer.Dispose();
        }

        public async void OverwriteToFileAsync()
        {
            using System.IO.StreamWriter writer = new System.IO.StreamWriter( LogPath, false, System.Text.Encoding.Default );
            
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            for(int i = 0; i < LogBook.Count; i++ )
                stringBuilder.Append( LogBook[ i ] );

            await writer.WriteAsync( stringBuilder.ToString() );

            writer.Dispose();
        }

        public string ReadLogFile()
        {
            using System.IO.StreamReader reader = new System.IO.StreamReader( LogPath, System.Text.Encoding.Default );

            return reader.ReadToEnd();
        }
    }

    public enum LogLevel
    {
        ERROR,
        INFO,
        FATAl,
        DEBUG,
        RECOVER,
        WARING
    }
}
