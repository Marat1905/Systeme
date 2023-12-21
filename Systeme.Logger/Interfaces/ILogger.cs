using Systeme.Logger.Enums;

namespace Systeme.Logger.Interfaces
{
    /// <summary>Интерфейс логгера </summary>
    public interface ILogger
    {
        /// <summary>Метод для записи в лог</summary>
        /// <param name="logLevel"><inheritdoc cref="LogLevel" path="/summary"/></param>
        /// <param name="message">Сообщение</param>
        void Write(LogLevel logLevel, string message);
    }
}
