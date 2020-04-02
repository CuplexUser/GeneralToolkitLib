using System;
using System.IO;
using System.Reflection;
using JetBrains.Annotations;
using Serilog;
using Serilog.Events;

namespace GeneralToolkitLib.Logging
{
    /// <summary>
    /// Basic log config and logimplementation for small projects
    /// </summary>
    public class SerilogAutoConfig
    {
        /// <summary>
        /// The auto-config settings
        /// </summary>
        private readonly LogSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogAutoConfig"/> class.
        /// </summary>
        /// <param name="logSettings">The log settings.</param>
        public SerilogAutoConfig(LogSettings logSettings)
        {
            _settings = logSettings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SerilogAutoConfig"/> class.
        /// </summary>
        /// <param name="logFilepath">The log filepath.</param>
        /// <param name="minLevel">The minimum level.</param>
        public SerilogAutoConfig(string logFilepath, LogEventLevel minLevel)
        {
            _settings = new LogSettings(logFilepath, minLevel);
        }

        [UsedImplicitly]
        public void InitializeLogger()
        {
            ILogger logger;

            if (_settings.UseRollingFile)
            {
                logger = new Serilog.LoggerConfiguration().WriteTo.RollingFile(pathFormat: _settings.LogFilePath, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                              fileSizeLimitBytes: _settings.MaxFileSize, retainedFileCountLimit: _settings.MaxDaysToKeepOldFiles)
                                                          .Enrich.WithThreadId()
                                                          .Enrich.FromLogContext()
                                                          .MinimumLevel.Is(_settings.MinLevel)
                                                          .CreateLogger();

            }
            else
            {
                logger = new Serilog.LoggerConfiguration().WriteTo.File(_settings.LogFilePath, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                                                              fileSizeLimitBytes: _settings.MaxFileSize)
                                                          .Enrich.WithThreadId()
                                                          .Enrich.FromLogContext()
                                                          .MinimumLevel.Is(_settings.MinLevel)
                                                          .CreateLogger();

            }

            Logger = logger;
            Log.Logger = logger;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        [UsedImplicitly]
        public LogSettings Settings { get => _settings; }

        public ILogger Logger { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public enum LogSink
        {
            /// <summary>
            /// The console
            /// </summary>
            Console,
            /// <summary>
            /// The file
            /// </summary>
            File,
            /// <summary>
            /// The rolling file
            /// </summary>
            RollingFile
        }

        /// <summary>
        /// 
        /// </summary>
        public sealed class LogSettings
        {
            /// <summary>
            /// The unique identifier
            /// </summary>
            private readonly Guid _guid;

            /// <summary>
            /// Initializes a new instance of the <see cref="LogSettings"/> class.
            /// </summary>
            public LogSettings()
            {
                _guid = Guid.NewGuid();
                SetDefaultValues();
            }
            /// <summary>
            /// Initializes a new instance of the <see cref="LogSettings"/> class.
            /// </summary>
            /// <param name="logFilePath">The log file path.</param>
            /// <param name="minLevel">The minimum level.</param>
            public LogSettings(string logFilePath, LogEventLevel minLevel)
            {
                SetDefaultValues();

                if (string.IsNullOrEmpty(logFilePath))
                {
                    throw new ArgumentException($"Parameter: {nameof(logFilePath)} must be set");
                }

                LogFilePath = logFilePath;
                MinLevel = minLevel;
            }

            private void SetDefaultValues()
            {
                UseRollingFile = true;
                MinLevel = LogEventLevel.Information;
                MaxFileSize = 1048576;
                MaxDaysToKeepOldFiles = 30;
                string path = Assembly.GetCallingAssembly().Location;
                LogFilePath = Path.GetDirectoryName(path);
            }

            /// <summary>
            /// Gets the instance identifier.
            /// </summary>
            /// <value>
            /// The instance identifier.
            /// </value>
            public Guid InstanceId { get => _guid; }

            /// <summary>
            /// Gets the log file path.
            /// </summary>
            /// <value>
            /// The log file path.
            /// </value>
            public string LogFilePath { get; set; }


            /// <summary>
            /// This only applies when using a rolling log file.
            /// Gets the maximum days to keep old files.
            /// </summary>
            /// <value>
            /// The maximum days to keep old files.
            /// </value>
            public int MaxDaysToKeepOldFiles { get; set; }

            /// <summary>
            /// Gets the maximum size of the file.
            /// For Rolling logfile it applies to the Current log
            /// </summary>
            /// <value>
            /// The maximum size of the file.
            /// </value>
            public long MaxFileSize { get; set; }

            /// <summary>
            /// Gets the minimum level.
            /// </summary>
            /// <value>
            /// The minimum level.
            /// </value>
            public LogEventLevel MinLevel { get; set; }



            public bool UseRollingFile { get; set; }

        }
    }

}