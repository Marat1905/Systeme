using AppWpf.Data;
using AppWpf.Infrastructure;
using AppWpf.Model;
using AppWpf.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using System.Windows.Input;
using Systeme.CarDriver;
using Systeme.CarDriver.Extensions;
using Systeme.CarDriver.Interfaces;
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
        private readonly ICarDriverTask _carDriverTask;
        private readonly IRepository<Driver> _driverDb;



        #region Заголовок окна. 
        /// <summary>Заголовок окна.</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна.</summary>
        public string Title { get => _Title; set { Set(ref _Title, value); } }
        #endregion

        #region Коллекция совпадений. 
        /// <summary> Коллекция совпадений..</summary>
        private ObservableCollection<AutoModel> _AutoModels;

        /// <summary> Коллекция совпадений..</summary>
        public ObservableCollection<AutoModel> AutoModels { get => _AutoModels; set { Set(ref _AutoModels, value); } }
        #endregion

        #region Тестовое для машин. 
        /// <summary>Тестовое для машин.</summary>
        private string _CarText = "";

        /// <summary>Тестовое для машин.</summary>
        public string CarText { get => _CarText; set { Set(ref _CarText, value); } }
        #endregion


        #region Тестовое для водителей. 
        /// <summary>Тестовое для водителей.</summary>
        private string _DriverText = "";

        /// <summary>Тестовое для водителей</summary>
        public string DriverText { get => _DriverText; set { Set(ref _DriverText, value); } }
        #endregion

        #region Command StartCarCommand - Команда на старт задачи машин

        /// <summary>Команда на старт задачи машин</summary>
        private ICommand _StartCarCommand;

        /// <summary>Команда на старт задачи машин</summary>
        public ICommand StartCarCommand => _StartCarCommand
            ??= new LambdaCommand(OnStartCarCommandCommandExecuted, CanStartCarCommandCommandExecute);

        /// <summary>Проверка возможности выполнения - Команда на старт задачи машин</summary>
        private bool CanStartCarCommandCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Команда на старт задачи машин</summary>
        private  void OnStartCarCommandCommandExecuted(object p)
        {
            StartCar();
        }
        #endregion

        #region Command StartDriverCommand - Команда на старт задачи водителей

        /// <summary>Команда на старт задачи водителей</summary>
        private ICommand _StartDriverCommand;

        /// <summary>Команда на старт задачи водителей</summary>
        public ICommand StartDriverCommand => _StartDriverCommand
            ??= new LambdaCommand(OnStartDriverCommandCommandExecuted, CanStartDriverCommandCommandExecute);

        /// <summary>Проверка возможности выполнения - Команда на старт задачи водителей</summary>
        private bool CanStartDriverCommandCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Команда на старт задачи водителей</summary>
        private  void OnStartDriverCommandCommandExecuted(object p)
        {
            StartDriver();
        }
        #endregion

        #region Command CancelCarCommand - Команда на завершение задачи машин

        /// <summary>Команда на завершение задачи машин</summary>
        private ICommand _CancelCarCommand;

        /// <summary>Команда на завершение задачи машин</summary>
        public ICommand CancelCarCommand => _CancelCarCommand
            ??= new LambdaCommand(OnCancelCarCommandCommandExecuted, CanCancelCarCommandCommandExecute);

        /// <summary>Проверка возможности выполнения - Команда на завершение задачи машин</summary>
        private bool CanCancelCarCommandCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Команда на завершение задачи машин</summary>
        private void OnCancelCarCommandCommandExecuted(object p)
        {
            _carDriverTask?.StopCar();
            _logger.Write(LogLevel.Information, "Остановили поток для автомобилей");
        }
        #endregion

        #region Command CancelDriverCommand - Команда на завершение задачи водителей

        /// <summary>Команда на завершение задачи водителей</summary>
        private ICommand _CancelDriverCommand;

        /// <summary>Команда на завершение задачи водителей</summary>
        public ICommand CancelDriverCommand => _CancelDriverCommand
            ??= new LambdaCommand(OnCancelDriverCommandCommandExecuted, CanCancelDriverCommandCommandExecute);

        /// <summary>Проверка возможности выполнения - Команда на завершение задачи водителей</summary>
        private bool CanCancelDriverCommandCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Команда на завершение задачи водителей</summary>
        private void OnCancelDriverCommandCommandExecuted(object p)
        {
            _carDriverTask?.StopDriver();
            _logger.Write(LogLevel.Information, "Остановили поток для водителей");
        }
        #endregion

        string[] cars = { "Мондео", "Крета", "Приус", "УАЗик", "Вольво", "Фокус", "Октавия", "Запорожец" };
        string[] drivers = { "Петр", "Василий", "Николай", "Марина", "Феодосий", "Карина" };
        

        public MainWindowViewModel(IRepository<Car> carDb,IRepository<Driver> driverDb, ILogger logger,ICarDriverTask carDriverTask)
        {
            _logger = logger;
            _carDriverTask = carDriverTask;
            _logger.Write(LogLevel.Information, "Запустили приложение");
            AutoModels = new ObservableCollection<AutoModel>();
            _carDb = carDb;
            _driverDb = driverDb;

            _carDriverTask.Notify += CarDriverTask_Notify;

            StartCar();
            StartDriver();

        }

        CarModel carModel = new CarModel();
        DriverModel driverModel = new DriverModel();

        private void CarDriverTask_Notify(ICarDriverTask sender, ICarDriver carDriver)
        {
            try
            {
                switch (carDriver)
                {
                    case CarModel car:
                        CarText = $"{car.Name} : {car.Date}";
                        carModel = car;
                         _carDb.Add(car.ToCar());
                        break;
                    case DriverModel driver:
                        DriverText = $"{driver.Name} : {driver.Date}";
                        driverModel = driver;
                         _driverDb.Add(driver.ToDriver());
                        break;
                }
                if (carModel == driverModel)
                {
                    App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        AutoModels.Add(new AutoModel(carModel.Name, driverModel.Name, carModel.Date));
                    });
                    //AutoModels.Add(new AutoModel(carModel.Name, driverModel.Name, carModel.Date));
                    _logger.Write(LogLevel.Information, $"Совпали Марка автомобиля: {carModel.Name}; Водитель {driverModel.Name} ;");
                }
            }
            catch (Exception ex)
            {

            }
           
        }

        #region Приватные методы
        private void StartCar()
        {
            Task.Run(() => _carDriverTask.StartCarAsync(2000,cars));
            _logger.Write(LogLevel.Information, "Запустили поток для автомобилей");
        }

        private void StartDriver()
        {
            Task.Run(() => _carDriverTask.StartDriverAsync(3000,drivers));
            _logger.Write(LogLevel.Information, "Запустили поток для водителей");
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Write(LogLevel.Information, "Остановили приложение");
            base.Dispose(disposing);
        }
        #endregion
    }
}
