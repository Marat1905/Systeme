using System;

namespace AppWpf.Model
{
    internal class AutoModel
    {
        /// <summary>Водитель</summary>
        public string Driver { get; set; }

        /// <summary>Марка автомобиля</summary>
        public string Car { get; set; }

        /// <summary>Время</summary>
        public DateTime Date { get;set; }

        public AutoModel(string driver,string car,DateTime date)
        {
            Driver = driver;
            Car = car;
            Date = date;
        }
        public AutoModel() { }
    }
}
