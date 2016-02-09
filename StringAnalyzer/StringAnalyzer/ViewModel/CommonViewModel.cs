using System;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using StringAnalyzer.WindowFactories;

namespace StringAnalyzer.ViewModel
{
    public class CommonViewModel : ViewModelBase
    {
        public CommonViewModel()
        {
            this.OpenTextfileCommand = new RelayCommand(this.OnExecuteOpenTextfileCommand);
            this.SaveTextfileCommand = new RelayCommand(this.OnExecuteSaveTextfileCommand);
            this.AboutCommand = new RelayCommand(this.OnExecuteAboutCommand);
            this.ShowTextHighlightWindowCommand=new RelayCommand(this.OnExecuteShowTextHighlightWindowCommand);

            if (this.IsInDesignMode)
            {
                this.Text = "CATTATTAGGA";
            }
            else
            {
                // Code runs "for real"
            }
        }


        #region Fields

        private string _text;
        private bool _ignoreLinebreaks = true;
        private bool _ignoreTabs = true;
        private bool _ignoreSpaces;
        private string _highlightedText;

        private IDialog _textHighlightWindow;

        #endregion

        #region Properties

        public string Text
        {
            get { return this._text; }
            set
            {
                if (this._text == value) return;

                this._text = value;
                this.RaisePropertyChanged();
            }
        }
        public bool IgnoreLinebreaks
        {
            get { return this._ignoreLinebreaks; }
            set
            {
                if (this._ignoreLinebreaks == value) return;

                this._ignoreLinebreaks = value;
                this.RaisePropertyChanged();
            }
        }
        public bool IgnoreTabs
        {
            get { return this._ignoreTabs; }
            set
            {
                if (this._ignoreTabs == value) return;

                this._ignoreTabs = value;
                this.RaisePropertyChanged();
            }
        }
        public bool IgnoreSpaces
        {
            get { return this._ignoreSpaces; }
            set
            {
                if (this._ignoreSpaces == value) return;

                this._ignoreSpaces = value;
                this.RaisePropertyChanged();
            }
        }
        public string HighlightedText
        {
            get { return this._highlightedText; }
            set
            {
                if (this._highlightedText == value) return;

                this._highlightedText = value;
                this.RaisePropertyChanged();
            }
        }

        #region Commands

        public ICommand OpenTextfileCommand { get; }
        public ICommand SaveTextfileCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand ShowTextHighlightWindowCommand { get; }

        #endregion

        #region Window Factories

        public IModalDialogFactory<IModalDialog<bool?>, bool?> AboutWindowFactory { get; set; }
        public IModalFileDialogFactory SaveFileDialogFactory { get; set; }
        public IModalFileDialogFactory OpenFileDialogFactory { get; set; }
        public IMessageBoxProvider MessageBoxProvider { get; set; }
        public IDialogFactory<IDialog> TextHighlightWindowFactory { get; set; }

        #endregion

        #endregion

        #region Comand Execution Methods

        private void OnExecuteOpenTextfileCommand()
        {
            var instance = this.OpenFileDialogFactory.GetInstance();
            instance.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien|*.*";
            if (instance.ShowDialog() != true) return;

            try
            {
                this.Text = File.ReadAllText(instance.FileName);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
            {
                this.MessageBoxProvider.Show("Fehler", "Beim öffnen der Textdatei ist ein Fehler aufgetreten.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnExecuteSaveTextfileCommand()
        {
            var instance = this.SaveFileDialogFactory.GetInstance();
            instance.Filter = "Textdateien (*.txt)|*.txt|Alle Dateien|*.*";
            if (instance.ShowDialog() != true) return;

            try
            {
                File.WriteAllText(instance.FileName, this.Text);
            }
            catch (Exception ex) when (ex is UnauthorizedAccessException || ex is SecurityException)
            {
                this.MessageBoxProvider.Show("Fehler", "Beim speichern der Textdatei ist ein Fehler aufgetreten.",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnExecuteAboutCommand()
        {
            var instance = this.AboutWindowFactory.GetInstance();
            instance.ShowDialog();
        }

        private void OnExecuteShowTextHighlightWindowCommand()
        {
            if (this._textHighlightWindow == null)
            {
                this._textHighlightWindow = this.TextHighlightWindowFactory.GetInstance();
            }
            this._textHighlightWindow.Show();
        }

        #endregion

        #region Methods

        public string GetText()
        {
            var chrs = new char[this.Text.Length];
            var counter = 0;
            foreach (var chr in this.Text.Where(chr => (!this.IgnoreSpaces || chr != ' ')
                                                    && (!this.IgnoreTabs || chr != '\t')
                                                    && (!this.IgnoreLinebreaks || (chr != '\r' && chr != '\n'))))
            {
                chrs[counter++] = chr;
            }
            return new string(chrs, 0, counter);
        }

        #endregion
    }
}