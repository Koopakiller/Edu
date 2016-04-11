using System.Net.Mail;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;

namespace Koopakiller.Apps.Brainstorming.Client.ViewModel
{
    public class SendViewModel : CurrentViewModelBase
    {
        public SendViewModel()
        {
            this.SendCommand = new RelayCommand(this.OnSend);
        }

        public Model.Client Client { get; set; }

        public string Topic
        {
            get { return this._topic; }
            set
            {
                this._topic = value;
                this.RaisePropertyChanged();
            }
        }

        private void OnSend()
        {
            this.Client.SendString(this.Text);
            if (this.ClearTextAfterSend)
            {
                this.Text = string.Empty;
            }
        }

        private string _text;
        private bool _clearTextAfterSend = true;
        private string _topic;

        public string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                this.RaisePropertyChanged();
            }
        }

        public bool ClearTextAfterSend
        {
            get { return this._clearTextAfterSend; }
            set
            {
                this._clearTextAfterSend = value;
                this.RaisePropertyChanged();
            }
        }


        public ICommand SendCommand { get; }
    }
}
