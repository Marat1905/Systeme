using System.Diagnostics;
using System.Runtime.CompilerServices;
using Systeme.CarDriver.Interfaces;

namespace Systeme.CarDriver
{
    internal class CarDriverTask: ICarDriverTask
    {
        private IProgress<ICarDriver> Progress { get; }

        private CancellationTokenSource _CarCancellation;

        private CancellationTokenSource _DriverCancellation;

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
        }

        public async Task StartDriverAsync(int Timeout, string[] drivers)
        {
            _DriverCancellation?.Cancel();
            var cancellation = new CancellationTokenSource();
            _DriverCancellation = cancellation;

            var cancel = cancellation.Token;

            await foreach (var driver in DoWorkAsync<DriverModel>(Timeout, drivers, cancel))
                Notify?.Invoke(this, driver);;
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
    }
}
