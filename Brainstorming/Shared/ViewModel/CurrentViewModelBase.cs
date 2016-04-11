using System;
using GalaSoft.MvvmLight;

namespace Koopakiller.Apps.Brainstorming.Shared.ViewModel
{
    public class CurrentViewModelBase : ViewModelBase
    {
        protected void NavigateToViewModel(CurrentViewModelBase viewModel)
        {
            this.NavigationStarted?.Invoke(this, viewModel);
        }

        internal event EventHandler<CurrentViewModelBase> NavigationStarted;

        protected void ShowMessage(MessageViewModelBase viewModel)
        {
            this.ShowMessageStarted?.Invoke(this, viewModel);
        }
        protected void ShowMessage(string title, string text)
        {
            this.ShowMessageStarted?.Invoke(this, new MessageBoxViewModel() { Text = text, Title = title });
        }

        internal event EventHandler<MessageViewModelBase> ShowMessageStarted;

        
    }
}
