namespace Systeme.CarDriver.Interfaces
{
    public interface ICarDriverTask
    {
        /// <summary>Событие</summary>
        public event ProgressHandler Notify;

        /// <summary>Делегат</summary>
        /// <param name="sender">экземпляр</param>
        /// <param name="carDriver">Возврат интерфейса</param>
        public delegate void ProgressHandler(ICarDriverTask sender, ICarDriver carDriver);

        /// <summary>Старт задачи автомобилей</summary>
        /// <param name="Timeout">Тайм аут между перебором</param>
        /// <param name="cars">Массив автомобилей</param>
        /// <returns></returns>
        public Task StartCarAsync(int Timeout, string[] cars);

        /// <summary>Старт задачи водителей</summary>
        /// <param name="Timeout">Тайм аут между перебором</param>
        /// <param name="drivers">Массив водителей</param>
        /// <returns></returns>
        public  Task StartDriverAsync(int Timeout, string[] drivers);

        /// <summary>Остановка задачи автомобилей</summary>
        public void StopCar();

        /// <summary>Остановка задачи водителей</summary>
        public void StopDriver();
    }
}
