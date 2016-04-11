using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Koopakiller.Apps.Brainstorming.Shared.ViewModel
{
    public class MessageViewModelBase : ViewModelBase 
    {
        public MessageViewModelBase()
        {
            this.CloseCommand = new RelayCommand(this.RaiseClose);
        }
        public event Action<MessageViewModelBase> Close;

        protected void RaiseClose()
        {
            this.Close?.Invoke(this);
        }

        public ICommand CloseCommand { get; }
    }
}
