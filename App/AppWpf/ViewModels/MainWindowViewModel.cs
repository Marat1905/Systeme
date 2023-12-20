using AppWpf.ViewModels.Base;

namespace AppWpf.ViewModels
{
    internal class MainWindowViewModel: ViewModel
    {
        #region Заголовок окна. 
        /// <summary>Заголовок окна.</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна.</summary>
        public string Title { get => _Title; set { Set(ref _Title, value); } }
        #endregion

    }
}
