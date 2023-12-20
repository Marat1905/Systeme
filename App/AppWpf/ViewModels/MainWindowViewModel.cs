using AppWpf.ViewModels.Base;
using System;
using Systeme.DAL.Interfaces;
using Systeme.Domain.Entityes;

namespace AppWpf.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        #region Заголовок окна. 
        /// <summary>Заголовок окна.</summary>
        private string _Title = "Главное окно";
        private readonly IRepository<Car> _carDb;

        /// <summary>Заголовок окна.</summary>
        public string Title { get => _Title; set { Set(ref _Title, value); } }
        #endregion

        public MainWindowViewModel(IRepository<Car> carDb)
        {
            _carDb = carDb;
            _carDb.Add(new Car { Model = "Москвич", Date = DateTime.Now });
        }
    }
}
