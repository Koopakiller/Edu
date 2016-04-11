namespace Koopakiller.Apps.Brainstorming.Shared.ViewModel
{
    public class MessageBoxViewModel : MessageViewModelBase
    {
        public MessageBoxViewModel()
        {
            if (this.IsInDesignMode)
            {
                this.Title = "Title";
                this.Text = "Text";
            }
        }
        private string _title;
        private string _text;

        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                this.RaisePropertyChanged();
            }
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
    }
}
