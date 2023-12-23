using Systeme.CarDriver.Extensions;

namespace Systeme.CarDriver
{
    public class CarModel : ICarDriver
    {
        /// <summary>Модель автомобиля</summary>
        public string Name { get; }

        /// <summary>Время</summary>
        public DateTime Date { get; }

        public CarModel(string carName, DateTime date)
        {
            Name = carName;
            Date = date;
        }
        public CarModel() { }

        public static bool operator == (CarModel p1, ICarDriver p2)
        {
            return p1.Date.TrimMilliseconds().CompareTo(p2.Date.TrimMilliseconds()) ==0;
        }
        public static bool operator != (CarModel p1, ICarDriver p2)
        {
            return !(p1 == p2);
        }
    }
}
