using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Koopakiller.Apps.Brainstorming.Server.Annotations;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
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

#if DEBUG
        public int Count => this.SuggestionItems.SelectMany(x => x.IPAddresses).Count();
#else
        public int Count => this.SuggestionItems.SelectMany(x=>x.IPAddresses).Distinct().Count();
#endif

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}