using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Koopakiller.Apps.Brainstorming.Shared.ViewModel
{
    public class CurrentViewModelBase : ViewModelBase
    {
        public CurrentViewModelBase()
        {
            this.MenuItems.Add(new MenuItemViewModel("Hilfe")
            {
                SubItems =
                {
                    new MenuItemViewModel("Über")
                    {
                        Command = new RelayCommand(this.OnShowAbout),
                    }
                }
            });
        }

        private void OnShowAbout()
        {
            this.ShowMessage(new AboutViewModel());
        }

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

        public IList<MenuItemViewModel> MenuItems { get; } = new ObservableCollection<MenuItemViewModel>();
    }
}
