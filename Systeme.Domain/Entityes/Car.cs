using Systeme.Domain.Entityes.Base;

namespace Systeme.Domain.Entityes
{
    /// <summary>Сущность автомобиля </summary>
    public class Car:Entity
    {
        /// <summary>Модель машины </summary>
        public string Model { get; set; }

        /// <summary>Время </summary>
        public DateTime Date { get; set; }
    }
}
