using Systeme.Domain.Entityes;

namespace Systeme.CarDriver.Extensions
{
    public static class EntityExtension
    {

        public static Car ToCar(this CarModel source)
        {
            Car car = new Car();
            car.Model = source.Name;
            car.Date= source.Date;
            return car;
        }

        public static Driver ToDriver(this DriverModel source)
        {
            Driver driver = new Driver();
            driver.Name = source.Name;
            driver.Date = source.Date;
            return driver;
        }
    }
}
