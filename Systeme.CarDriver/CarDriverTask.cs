using System.Diagnostics;
using System.Runtime.CompilerServices;
using Systeme.CarDriver.Interfaces;
using Systeme.Domain.Entityes;

namespace Systeme.CarDriver
{
    internal class CarDriverTask: ICarDriverTask
    {
        private IProgress<ICarDriver> Progress { get; }

        private  CancellationTokenSource _CarCancellation;

        private  CancellationTokenSource _DriverCancellation;

        public event ICarDriverTask.ProgressHandler Notify;

        public CarDriverTask()
        {
            Progress = new Progress<ICarDriver>(p => Notify?.Invoke(this, p));
        }


        public delegate void ProgressHandler(CarDriverTask sender, ICarDriver carDriver);

       

        public async Task StartCarAsync(int Timeout,string[] cars)
        {
            _CarCancellation?.Cancel();
            var cancellation = new CancellationTokenSource();
            _CarCancellation = cancellation;

            var cancel = cancellation.Token;

            await foreach (var car in DoWorkAsync<CarModel>(Timeout, cars, cancel))
                Notify?.Invoke(this, car);
            // await Task.Run(() => DoWorkCarAsync(Timeout,cars, Progress, cancel), cancel);
        }

        public async Task StartDriverAsync(int Timeout, string[] drivers)
        {
            _DriverCancellation?.Cancel();
            var cancellation = new CancellationTokenSource();
            _DriverCancellation = cancellation;

            var cancel = cancellation.Token;

           

            await foreach (var driver in DoWorkAsync<DriverModel>(Timeout, drivers, cancel))
                Notify?.Invoke(this, driver);
            //await Task.Run(() => DoWorkDriverAsync(Timeout, drivers, Progress, cancel), cancel);
            //Notify?.Invoke(this, (ICarDriver)await Task.Run(() => DoWorkAsync<DriverModel>(Timeout, drivers, cancel), cancel));
        }

        public void StopCar()
        {
            _CarCancellation.Cancel();
        }

        public void StopDriver()
        {
            _DriverCancellation.Cancel();
        }

        /// <summary>Метод для выполнения перебора</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Timeout">Тайм аут между перебором </param>
        /// <param name="array">Массив</param>
        /// <param name="Cancel">Токен отмены</param>
        /// <returns></returns>
        public async IAsyncEnumerable<T> DoWorkAsync<T>(int Timeout, string[] array, [EnumeratorCancellation] CancellationToken Cancel = default) where T: class, ICarDriver, new()
        {
            Cancel.ThrowIfCancellationRequested();

            var thread_id = Thread.CurrentThread.ManagedThreadId;

            for (int j = 0; j < array.Length; j++)
            {
                if (!Cancel.IsCancellationRequested)
                {
                    await Task.Delay(Timeout, Cancel);
                    Debug.WriteLine($"{array[j]} - поток: {thread_id}");
                    yield return new T()
                    {
                        Name= array[j], 
                        Date= DateTime.Now
                    };
                    //делаем бесконечный цикл если не надо удалить
                    if (j == array.Length - 1)
                    {
                        j = -1;
                    }
                }
            }
        }
        #region Хлам после рефакторинга
        ///// <summary>Метод для выполнения перебора автомобилей</summary>
        ///// <param name="Timeout">Тайм аут между перебором </param>
        ///// <param name="car">Массив автомобилей</param>
        ///// <param name="Progress">Возвращаем прогресс выполнения</param>
        ///// <param name="Cancel">Токен отмены</param>
        ///// <returns></returns>
        //private async Task DoWorkCarAsync(int Timeout, string[] car,
        //   IProgress<ICarDriver> Progress = null,
        //   CancellationToken Cancel = default)
        //{
        //    Cancel.ThrowIfCancellationRequested();

        //    var thread_id = Thread.CurrentThread.ManagedThreadId;

        //    for (int j = 0; j < car.Length; j++)
        //    {
        //        if (!Cancel.IsCancellationRequested)
        //        {
        //            await Task.Delay(Timeout, Cancel);
        //            Debug.WriteLine($"{car[j]} - поток: {thread_id}");
        //            Progress?.Report(
        //                new CarModel(car[j], DateTime.Now));
        //            //делаем бесконечный цикл если не надо удалить
        //            if (j == car.Length - 1)
        //            {
        //                j = -1;
        //            }
        //        }
        //    }
        //}

        ///// <summary>Метод для выполнения перебора водителей</summary>
        ///// <param name="Timeout">Тайм аут между перебором </param>
        ///// <param name="driver">Массив водителей</param>
        ///// <param name="Progress">Возвращаем прогресс выполнения</param>
        ///// <param name="Cancel">Токен отмены</param>
        ///// <returns></returns>
        //private async Task DoWorkDriverAsync(int Timeout, string[] driver,
        //  IProgress<ICarDriver> Progress = null,
        //  CancellationToken Cancel = default)
        //{
        //    Cancel.ThrowIfCancellationRequested();

        //    var thread_id = Thread.CurrentThread.ManagedThreadId;

        //    for (int j = 0; j < driver.Length; j++)
        //    {
        //        if (!Cancel.IsCancellationRequested)
        //        {
        //            await Task.Delay(Timeout, Cancel);
        //            Debug.WriteLine($"{driver[j]} - поток: {thread_id}");
        //            Progress?.Report(
        //                new DriverModel(driver[j], DateTime.Now));
        //            //делаем бесконечный цикл если не надо удалить
        //            if (j == driver.Length - 1)
        //            {
        //                j = -1;
        //            }
        //        }
        //    }
        //}
        #endregion


    }
}
