using System.Text;
using Systeme.Logger.Enums;
using Systeme.Logger.Interfaces;

namespace Systeme.Logger.Loggers
{
    /// <summary>Логгер в Файл</summary>
    public class FileLoggers : ILogger
    {
        private readonly string fileName;
        private readonly string fileExt;
        object obj = new object();

        /// <summary>Полный путь к каталогу</summary>
        public string Directory { get; }

        /// <summary>Период создания нового файла</summary>
        public int Minute { get; }

        /// <summary>Информация о последнем файле</summary>
        public FileInfo LastFilesLog { get; private set; }

        /// <summary><inheritdoc cref="FileLoggers"/></summary>
        /// <param name="directory">Название каталога(Сокращенный вариант)</param>
        /// <param name="minute"><inheritdoc cref="Minute" path="/summary"/></param>
        /// <param name="fileName">Начальное название файла</param>
        /// <param name="fileExt">Расширение файла</param>
        public FileLoggers(string directory, int minute = 15, string fileName = "logs", string fileExt = ".txt")
        {
            Directory = Path.Combine(Environment.CurrentDirectory, directory);
            Minute = minute;
            this.fileName = fileName;
            this.fileExt = fileExt;
            CheckingDirectory(Directory);
            FilesLastCreate(Directory, minute);

        }


        public void Write(LogLevel logLevel, string message)
        {
            var file = FilesLastCreate(Directory, Minute);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{DateTime.Now}\t{logLevel.ToString()} \t{message}");

            lock (obj)
            {
                using (FileStream fstream = new FileStream(file.FullName, FileMode.Append))
                {
                    // преобразуем строку в байты
                    byte[] input = Encoding.Default.GetBytes(sb.ToString());
                    // запись массива байтов в файл
                    fstream.Write(input, 0, input.Length);
                }
            }
        }

        #region Приватные методы

        /// <summary>Метод проверки на наличие каталога. Если нет то создаем</summary>
        /// <param name="directory">Путь к каталогу</param>
        private void CheckingDirectory(string directory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }

        /// <summary>Проверяем файл на наличие</summary>
        /// <param name="directory"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        private FileInfo FilesLastCreate(string directory, int minute)
        {
            if (LastFilesLog == null || LastFilesLog.Exists || LastFilesLog.CreationTime < DateTime.Now.Subtract(TimeSpan.FromMinutes(minute)))
            {

                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                var files = directoryInfo.GetFiles().Where(file => file.
                CreationTime >= (DateTime.Now.Subtract(TimeSpan.FromMinutes(minute)))
                && file.CreationTime <= DateTime.Now);
                // Проверяем есть ли соответствие условию
                if (!files.Any())
                    return CreateNewFile(directory, fileName);
                else
                {
                    var file = files.OrderByDescending(file => file.CreationTime).First();
                    LastFilesLog = file;
                    return file;
                }
            }
            else
                return LastFilesLog;
        }

        /// <summary>Создаем файл в кталоге </summary>
        /// <param name="directory"><inheritdoc cref="Directory" path="/summary"/></param>
        /// <param name="fileName">Начальное название файла</param>
        /// <returns></returns>
        private FileInfo CreateNewFile(string directory, string fileName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(fileName + DateTime.Now.ToString("_yyyy_dd_M__HH_mm_ss") + fileExt);
            FileInfo file = new FileInfo(Path.Combine(directory, sb.ToString()));
            return file;
        }
        #endregion
    }
}
