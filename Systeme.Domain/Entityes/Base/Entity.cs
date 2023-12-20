using Systeme.Domain.Interfaces;

namespace Systeme.Domain.Entityes.Base
{
    public abstract class Entity : IEntity
    {
       // [Index(nameof(Identificator), IsUnique = true)]
        /// <summary>Идентификатор</summary>
        public int Id { get; set; }
    }
}
