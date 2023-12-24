using Systeme.CarDriver.Extensions;

namespace Systeme.CarDriver
{
    /// <summary><inheritdoc cref="ICarDriver"/></summary>
    public class DriverModel : ICarDriver
    {
        /// <summary>Имя водителя</summary>
        public string Name { get; }

        /// <summary>Время</summary>
        public DateTime Date { get; }

        /// <summary></summary>
        /// <param name="driverName"><inheritdoc cref="Name" path="/summary"/></param>
        /// <param name="date"><inheritdoc cref="Date" path="/summary"/></param>
        public DriverModel(string driverName, DateTime date)
        {
            Name = driverName;
            Date = date.TrimMilliseconds();
        }
        public DriverModel() { }

        public static bool operator ==(DriverModel p1, ICarDriver p2)
        {
            return p1.Date.CompareTo(p2.Date) == 0;
        }
        public static bool operator !=(DriverModel p1, ICarDriver p2)
        {
            return !(p1 == p2);
        }
    }
}
