using Systeme.Domain.Interfaces;

namespace Systeme.DAL.Interfaces
{
    /// <summary>Интерфейс репозитория</summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class,IEntity,new ()
    {
        /// <summary> Получение коллекцию БД</summary>
        IQueryable<T> Items { get; }

        /// <summary>Авто сохранение данных в БД</summary>
        bool AutoSaveChanges { get; set; }

        /// <summary> Получить объект из БД </summary>
        /// <param name="id">Id - объекта</param>
        /// <returns>Возвращаем объект </returns>
        T Get(int id);

        /// <summary>Получить объект из БД асинхронно </summary>
        /// <param name="id">Id - объекта</param>
        /// <param name="Cancel">Токен отмены операции</param>
        /// <returns></returns>
        Task<T> GetAsync(int id, CancellationToken Cancel = default);

        /// <summary>Добавить в БД объект </summary>
        /// <param name="item">Объект</param>
        /// <returns>Возвращаем добавленный объект</returns>
        T Add(T item);

        /// <summary>Добавить в БД объект асинхронно </summary>
        /// <param name="item">Объект</param>
        /// <param name="Cancel">Токен отмены операции</param>
        /// <returns>Возвращаем добавленный объект</returns>
        Task<T> AddAsync(T item, CancellationToken Cancel = default);

        /// <summary>Добавляем коллекцию в БД</summary>
        /// <param name="item">Передаваемая коллекция</param>
        void AddRange(IEnumerable<T> item);

        /// <summary> Добавляем коллекцию в БД асинхронно </summary>
        /// <param name="item">Передаваемая коллекция</param>
        /// <param name="Cancel">Токен отмены операции</param>
        Task AddRangeAsync(IEnumerable<T> item, CancellationToken Cancel = default);

        /// <summary>Обновление объекта в БД</summary>
        /// <param name="item">Объект</param>
        void Update(T item);

        /// <summary>Обновление объекта в БД асинхронно</summary>
        /// <param name="item">Объект</param>
        /// <param name="Cancel">Токен отмены операции</param>
        Task UpdateAsync(T item, CancellationToken Cancel = default);

        /// <summary>Удаление объекта из БД</summary>
        /// <param name="id">Идентификатор объекта</param>
        void Remove(int id);

        /// <summary>Удаление объекта из БД асинхронно</summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <param name="Cancel">Токен отмены операции</param>
        Task RemoveAsync(int id, CancellationToken Cancel = default);

        /// <summary>Сохранение данных в БД если не было авто сохранения</summary>
        void SaveAs();

        /// <summary>Сохранение данных в БД если не было авто сохранения асинхронно</summary>
        Task SaveAsAsync(CancellationToken Cancel = default);

        /// <summary> Очистка таблицы </summary>
        Task ClearAsync(CancellationToken Cancel = default);
    }
}
