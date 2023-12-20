using Systeme.Domain.Entityes.Base;

namespace Systeme.Domain.Entityes
{
    /// <summary>Сущность водителя</summary>
    //// [Table("Drivers")]
    public class Driver: Entity
    {
       /// <summary>Имя водителя </summary>
        public string Name { get; set; }

        /// <summary>Время </summary>
        public DateTime Date { get; set; }
    }
}
