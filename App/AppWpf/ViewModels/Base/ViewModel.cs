using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Markup;
using System.Xaml;

namespace AppWpf.ViewModels.Base
{
    internal abstract class ViewModel : MarkupExtension, INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));

            //var handlers = PropertyChanged;
            //if (handlers is null) return;

            //var invocation_list = handlers.GetInvocationList();

            //var arg = new PropertyChangedEventArgs(PropertyName);
            //foreach (var action in invocation_list)
            //    if (action.Target is DispatcherObject disp_object)
            //        disp_object.Dispatcher.Invoke(action, this, arg);
            //    else
            //        action.DynamicInvoke(this, arg);
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public override object ProvideValue(IServiceProvider sp)
        {
            var value_target_service = sp.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var root_object_service = sp.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;

            OnInitialized(
                value_target_service?.TargetObject,
                value_target_service?.TargetProperty,
                root_object_service?.RootObject);

            return this;
        }

        private WeakReference _TargetRef;
        private WeakReference _RootRef;

        public object TargetObject => _TargetRef.Target;

        public object RootObject => _RootRef.Target;

        protected virtual void OnInitialized(object Target, object Property, object Root)
        {
            _TargetRef = new WeakReference(Target);
            _RootRef = new WeakReference(Root);
        }

        #region IDisposable

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
        }

        /// <summary>Признак того, что объект уже уничтожен</summary>
        private bool _Disposed;

        /// <summary>Освобождение ресурсов</summary>
        /// <param name="disposing">Если истина, то требуется освободить управляемые объекты. Освободить неуправляемые объекты в любом случае</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_Disposed) return;
            if (disposing) DisposeManagedObject();
            DisposeUnmanagedObject();
            _Disposed = true;
        }

        /// <summary>Освободить управляемые объекты</summary>
        protected virtual void DisposeManagedObject() { }
        /// <summary>Освободить неуправляемые объекты</summary>
        protected virtual void DisposeUnmanagedObject() { }

        #endregion
    }
}
