using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using Koopakiller.Apps.Brainstorming.Server.Annotations;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
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