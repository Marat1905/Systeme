using AppWpf.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Systeme.DAL.Interfaces;
using Systeme.Domain.Entityes;
using Systeme.Logger.Enums;
using Systeme.Logger.Interfaces;

namespace AppWpf.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        private readonly IRepository<Car> _carDb;
        private readonly ILogger _logger;

        #region Заголовок окна. 
        /// <summary>Заголовок окна.</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна.</summary>
        public string Title { get => _Title; set { Set(ref _Title, value); } }
        #endregion

        public MainWindowViewModel(IRepository<Car> carDb, ILogger logger)
        {
            _carDb = carDb;
            _logger = logger;
            _carDb.Add(new Car { Model = "Москвич", Date = DateTime.Now });

            Task.Run(() =>
            {
                int i = 0;
                while (true)
                {
                    i++;
                    _logger.Write(LogLevel.Warning, $"Сообщение {i}");
                    Task.Delay(1000).Wait();
                }
            });
        }
    }
}
