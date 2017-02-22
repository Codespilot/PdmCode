using System;
using System.Windows.Input;

namespace PdmCode.Infrastructure
{
    public sealed class GenericCommand : ICommand
    {
        public Func<object, bool> CanExecuteCallback { get; set; }
        public Action<object> ExecuteCallback { get; set; }

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (CanExecuteCallback != null)
            {
                return CanExecuteCallback(parameter);
            }
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, new EventArgs());
            }
        }

        public void Execute(object parameter)
        {
            if (ExecuteCallback != null)
            {
                ExecuteCallback(parameter);
            }
        }

        #endregion
    }
}
