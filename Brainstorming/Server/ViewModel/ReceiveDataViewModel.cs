using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Koopakiller.Apps.Brainstorming.Server.Annotations;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public class ReceiveDataViewModel : CurrentViewModelBase
    {
        private readonly Model.Server _server;
        private string _topic;

        public int Port => this._server.Port;
        public IPAddress ServerAddress => this._server.IPAddress;

        public string Topic
        {
            get { return this._topic; }
            set
            {
                this._topic = value;
                this.RaisePropertyChanged();
            }
        }

        public ReceiveDataViewModel(Model.Server server)
        {
            this._server = server;
            this.GroupItemsCommand = new RelayCommand<object>(this.OnGroupItems);

            this.Suggestions = new ObservableCollection<SuggestionItemGroup>();
            this.StartServerCommand = new RelayCommand(this.StartServer);
        }
        
        private void OnGroupItems(object items)
        {
            var list = ((IList<object>)items).Cast<SuggestionItemGroup>().ToList();
            if (list.Count <= 1)
            {
                return;
            }
            var item = list.First();
            foreach (var item2 in list.Skip(1))
            {
                foreach (var sub in item2.SuggestionItems)
                {
                    item.SuggestionItems.Add(sub);
                }
                this.Suggestions.Remove(item2);
            }
        }

        public ICommand StartServerCommand { get; }

        public void StartServer()
        {
            if (this.IsInDesignMode)
            {
                return;
            }

            this._server.DataReceived += this.OnDataReceived;
            this._server.Start(CancellationToken.None);
        }

        private void OnDataReceived(object sender, string str)
        {
            DispatcherHelper.RunAsync(() =>
            {
                DispatcherHelper.CheckBeginInvokeOnUI(() =>
                {
                    var item = this.Suggestions.FirstOrDefault(x => x.SuggestionItems.Any(y => y.Text == str));
                    if (item == null)
                    {
                        item = new SuggestionItemGroup()
                        {
                            SuggestionItems =
                            {
                                new SuggestionItem()
                                {
                                    Text = str,
                                },
                            },
                        };
                        this.Suggestions.Add(item);
                    }
                    var item2 = item.SuggestionItems.Single(x => x.Text == str);
                    var ip = sender as IPAddress;
                    if (item2.IPAddresses.All(x => !Equals(x, ip)))
                    {
                        item2.IPAddresses.Add(ip);
                    }
                });
            });
        }

        public ObservableCollection<SuggestionItemGroup> Suggestions { get; }

        public RelayCommand<object> GroupItemsCommand { get; }
    }

    public class SuggestionItemGroup : INotifyPropertyChanged
    {
        public SuggestionItemGroup()
        {
            this.SuggestionItems = new ObservableCollection<SuggestionItem>();
            this.SuggestionItems.CollectionChanged += this.OnSuggestionItemsChanged;
        }

        private void OnSuggestionItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (SuggestionItem item in e.NewItems)
                {
                    item.PropertyChanged += this.OnSuggestionItemChanged;
                }
            }
            if (e.OldItems != null)
            {
                foreach (SuggestionItem item in e.OldItems)
                {
                    item.PropertyChanged -= this.OnSuggestionItemChanged;
                }
            }
            this.RaisePropertyChanged(nameof(this.Count));
        }

        private void OnSuggestionItemChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(SuggestionItem.Count):
                    this.RaisePropertyChanged(nameof(this.Count));
                    break;
            }
        }

        public ObservableCollection<SuggestionItem> SuggestionItems { get; }

        public int Count => this.SuggestionItems.Sum(x => x.Count);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class SuggestionItem : INotifyPropertyChanged
    {
        private string _text;

        public SuggestionItem()
        {
            this.IPAddresses = new ObservableCollection<IPAddress>();
            this.IPAddresses.CollectionChanged += this.OnIPAddressesChanged;
        }

        // ReSharper disable once InconsistentNaming
        private void OnIPAddressesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.Count));
        }

        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                this.RaisePropertyChanged();
            }
        }

        public int Count => this.IPAddresses.Count;

        // ReSharper disable once InconsistentNaming
        public ObservableCollection<IPAddress> IPAddresses { get; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
