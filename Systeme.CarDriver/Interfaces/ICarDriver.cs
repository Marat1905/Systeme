namespace Systeme.CarDriver
{
    public interface ICarDriver 
    {
        /// <summary>Имя </summary>
        public string Name { get; internal set; }

        /// <summary>Дата</summary>
        public DateTime Date { get; internal set; }

    }
}
