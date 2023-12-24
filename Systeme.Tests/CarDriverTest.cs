
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Systeme.CarDriver;
using Systeme.CarDriver.Interfaces;
using Systeme.Domain.Entityes;

namespace Systeme.Tests
{
    [TestClass()]
    public class CarDriverTest
    {
        private static IHost __Host;
        private static IServiceCollection Services;

        List<CarModel> _carModels;
        List<DriverModel> _driverModels;
        CarModel _carModel=new CarModel();
        DriverModel _driverModel = new DriverModel();
        List<AutoModel> _autoModels;

        [ClassInitialize]
        public static void TestFixtureSetup(TestContext context)
        {
            Services = new ServiceCollection()
                .AddCarDriverService();
            var serviceProvider = Services.BuildServiceProvider();

        }


        [TestMethod()]
        public async Task T01_CarTask()
        {
            var serviceProvider = Services.BuildServiceProvider();
            string[] cars = { "Мондео", "Крета", "Приус", "УАЗик", "Вольво", "Фокус", "Октавия", "Запорожец" };
            _carModels = new List<CarModel>();
            ICarDriverTask? carDriverTask= serviceProvider.GetService<ICarDriverTask>();
            carDriverTask.Notify += CarDriverTask_Notify;
            Task.Run(() => carDriverTask.StartCarAsync(2000, cars));
            await Task.Delay(4100);
            carDriverTask.Notify -= CarDriverTask_Notify;
            carDriverTask.StopCar();
            Assert.AreEqual(2, _carModels.Count());

        }

        [TestMethod()]
        public async Task T02_DriverTask()
        {
            var serviceProvider = Services.BuildServiceProvider();      
            string[] drivers = { "Петр", "Василий", "Николай", "Марина", "Феодосий", "Карина" };
            _driverModels= new List<DriverModel>();
            ICarDriverTask? carDriverTask = serviceProvider.GetService<ICarDriverTask>();
            carDriverTask.Notify += CarDriverTask_Notify;
            Task.Run(() => carDriverTask.StartDriverAsync(3000, drivers));
            await Task.Delay(6100);
            carDriverTask.StopDriver();
            carDriverTask.Notify -= CarDriverTask_Notify;            
            Assert.AreEqual(2, _driverModels.Count());

        }

        [TestMethod()]
        public async Task T03_DriverCarTask()
        {
            var serviceProvider = Services.BuildServiceProvider();
            string[] drivers = { "Петр", "Василий", "Николай", "Марина", "Феодосий", "Карина" };
            string[] cars = { "Мондео", "Крета", "Приус", "УАЗик", "Вольво", "Фокус", "Октавия", "Запорожец" };
            _carModel = new CarModel();
            _driverModel = new DriverModel();
            _autoModels = new List<AutoModel>();

            ICarDriverTask? carDriverTask = serviceProvider.GetService<ICarDriverTask>();
            carDriverTask.Notify += CarDriverTask_Notify;
            Task.Run(() => carDriverTask.StartCarAsync(2000, cars));
            Task.Run(() => carDriverTask.StartDriverAsync(3000, drivers));
            await Task.Delay(6100);
            carDriverTask.StopCar();
            carDriverTask.StopDriver();
            carDriverTask.Notify -= CarDriverTask_Notify;
            Assert.AreEqual(1, _autoModels.Count());

        }

        private void CarDriverTask_Notify(ICarDriverTask sender, ICarDriver carDriver)
        {
            switch (carDriver)
            {
                case CarModel car:
                    _carModel = car;
                    _carModels?.Add(_carModel);
                    break;
                case DriverModel driver:
                    _driverModel = driver;
                    _driverModels?.Add(_driverModel);
                    break;
            }
            if (_carModel == _driverModel)
            {
                _autoModels.Add(new AutoModel(_carModel.Name, _driverModel.Name, _carModel.Date));
                
            }
        }
    }

}
