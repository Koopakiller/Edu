using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Shared.ViewModel
{
    public class MessageViewModelBase<T> : ViewModelBase where T : MessageViewModelBase<T>
    {
        public MessageViewModelBase()
        {
            this.CloseCommand = new RelayCommand(this.RaiseClose);
        }
        public event Action<MessageViewModelBase<T>> Close;

        protected void RaiseClose()
        {
            this.Close?.Invoke(this);
        }

        public ICommand CloseCommand { get; }
    }
}
