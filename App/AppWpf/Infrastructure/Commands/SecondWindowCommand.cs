using AppWpf.Views.Windows;
using System;
using System.Windows;
using AppWpf.Infrastructure.Commands.Base;

namespace AppWpf.Infrastructure.Commands
{
    internal class SecondWindowCommand : Command
    {
        private SecondWindow _Window;

        public override bool CanExecute(object parameter) => _Window == null;

        public override void Execute(object parameter)
        {
            var window = new SecondWindow
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;

            window.Show();
        }

        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window)Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}
