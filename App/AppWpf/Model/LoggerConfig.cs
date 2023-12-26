namespace AppWpf.Model
{
    /// <summary>Для сервиса логгера</summary>
    public class LoggerConfig
    {
        /// <summary>Каталог</summary>
        public string Directory { get; set; }

        /// <summary>Период для создания нового файла</summary>
        public int PeriodMinute { get; set; }

        /// <summary>Расширение файла</summary>
        public string FileExt { get; set; }
    }
}
