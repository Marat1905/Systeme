using AppWpf.Model;
using AppWpf.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Systeme.CarDriver;
using Systeme.CarDriver.Extensions;
using Systeme.DAL.Extensions;
using Systeme.DAL.Interfaces;
using Systeme.Domain.Entityes;
using Systeme.Logger.Enums;
using Systeme.Logger.Interfaces;

namespace AppWpf.ViewModels
{
    internal class SecondWindowViewModel :ViewModel
    {
        private readonly IRepository<Car> _carDb;
        private readonly IRepository<Driver> _driverDb;
        private readonly ILogger _logger;

        IProgress<ObservableCollection<AutoModel>> Progress { get; }

        #region Заголовок окна. 
        /// <summary>Заголовок окна.</summary>
        private string _Title = "Второе окно";

        /// <summary>Заголовок окна.</summary>
        public string Title { get => _Title; set { Set(ref _Title, value); } }
        #endregion

        #region Коллекция совпадений. 
        /// <summary> Коллекция совпадений..</summary>
        private ObservableCollection<AutoModel> _AutoModels;

        /// <summary> Коллекция совпадений..</summary>
        public ObservableCollection<AutoModel> AutoModels { get => _AutoModels; set { Set(ref _AutoModels, value); } }
        #endregion

        public SecondWindowViewModel(IRepository<Car> carDb, IRepository<Driver> driverDb, ILogger logger)
        {
            _logger = logger;
            _logger.Write(LogLevel.Information, "Запустили второе окно");
            _carDb = carDb;
            _driverDb = driverDb;
           
            AutoModels =new ObservableCollection<AutoModel>();

            //Progress = new Progress<ObservableCollection<AutoModel>>(p => AutoModels=p);

            Task.Run(()=>UpdateTable(Progress));
        }

        //public Task UpdateTable(IProgress<ObservableCollection<AutoModel>> progress = null,
        //   CancellationToken Cancel = default)
        //{
        //    while (true)
        //    {

        //        var resultJoint = _carDb.Items.AsEnumerable().FullOuterJoinJoin(   
        //        _driverDb.Items.AsEnumerable(),                        
        //             p => p.Date.TrimMilliseconds(),                               
        //             a => a.Date.TrimMilliseconds(),                            
        //             (p, a) => new { MyCar = p, MyDriver = a })     
        //             .Select(a => new AutoModel(
        //                 a.MyDriver != null ? a.MyDriver.Name : "",
        //                 a.MyCar != null ? a.MyCar.Model : "",
        //                 a.MyCar != null ? a.MyCar.Date : (a.MyDriver != null ? a.MyDriver.Date : default)
        //                 )
        //             ).OrderByDescending(p=>p.Date).ToList();

        //        progress?.Report(new ObservableCollection<AutoModel>(resultJoint));
        //        Task.Delay(2000);
        //    }
        //}

        public Task UpdateTable(IProgress<ObservableCollection<AutoModel>> progress = null,
           CancellationToken Cancel = default)
        {
            while (true)
            {
                AutoModels = new ObservableCollection<AutoModel>
                    (
                   _carDb.Items.AsEnumerable().FullOuterJoinJoin(
                _driverDb.Items.AsEnumerable(),
                     p => p.Date.TrimMilliseconds(),
                     a => a.Date.TrimMilliseconds(),
                     (p, a) => new { MyCar = p, MyDriver = a })
                     .Select(a => new AutoModel(
                         a.MyDriver != null ? a.MyDriver.Name : "",
                         a.MyCar != null ? a.MyCar.Model : "",
                         a.MyCar != null ? a.MyCar.Date : (a.MyDriver != null ? a.MyDriver.Date : default)
                         )
                     ).OrderByDescending(p => p.Date).ToList()
                    );
                Task.Delay(2000);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _logger.Write(LogLevel.Information, "Закрыли второе окно");
            base.Dispose(disposing);
        }
    }

}
