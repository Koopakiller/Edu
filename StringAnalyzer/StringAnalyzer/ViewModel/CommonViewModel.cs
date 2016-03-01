using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using StringAnalyzer.Helper;
using StringAnalyzer.WindowFactories;

namespace StringAnalyzer.ViewModel
{
    public class CommonViewModel : ViewModelBase, IDisposable
    {
        public CommonViewModel()
        {
            this.OpenTextfileCommand = new RelayCommand(this.OnExecuteOpenTextfileCommand);
            this.SaveTextfileCommand = new RelayCommand(this.OnExecuteSaveTextfileCommand);
            this.AboutCommand = new RelayCommand(this.OnExecuteAboutCommand);
            this.ShowRepetitionsWindowCommand = new RelayCommand(this.OnExecuteShowRepetitionsWindowCommand);
            this.FindRepetitionsCommand = new RelayCommand(this.OnExecuteFindRepetitionsCommand);
            this.DisposeCommand = new RelayCommand(this.OnExecuteDisposeCommand);
            this.UpdateTextFromFlowDocumentCommand = new RelayCommand(this.OnExecuteUpdateTextFromFlowDocumentCommand);
            this.UpdateHighlightCommand = new RelayCommand(this.OnExecuteUpdateHighlightCommand);

            if (this.IsInDesignMode)
            {
                this.Text = "CATTATTAGGA";
            }
        }

        #region Fields

        private BackgroundWorker _bw;

        private string _text;
        private bool _ignoreLinebreaks = true;
        private bool _ignoreTabs = true;
        private bool _ignoreSpaces;
        private string _highlightedText;
        private int _minimalRepetitionCount = 2;
        private int _minimalCharacterCountForRepetition = 3;
        private ObservableCollection<Occurence> _foundOccurences;

        private bool _showHighlightedText;
        private bool _flowDocumentUpdateInProgress;

        private IDialog _textHighlightWindow;
        private IDialog _repetitionsWindow;

        private bool _allowUserInteraction = true;
        private int _progressMaximum;
        private int _progressValue;
        private string _progressInfo;

        #endregion

        #region Properties

        #region VM Properties

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
        public int MinimalRepetitionCount
        {
            get { return this._minimalRepetitionCount; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                if (this._minimalRepetitionCount == value) return;

                this._minimalRepetitionCount = value;
                this.RaisePropertyChanged();
            }
        }
        public int MinimalCharacterCountForRepetition
        {
            get { return this._minimalCharacterCountForRepetition; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                if (this._minimalCharacterCountForRepetition == value) return;

                this._minimalCharacterCountForRepetition = value;
                this.RaisePropertyChanged();
            }
        }

        public string Text
        {
            get { return this._text; }
            set
            {
                if (this._text == value) return;

                this._text = value;
                this.RaisePropertyChanged();
                this.ResetFlowDocumentHighlight(true);
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
                this.ResetFlowDocumentHighlight(false);
            }
        }
        public FlowDocument FlowDocument { get; } = new FlowDocument();

        public ObservableCollection<Occurence> FoundOccurences
        {
            get { return this._foundOccurences; }
            set
            {
                if (this._foundOccurences == value) return;

                this._foundOccurences = value;
                this.RaisePropertyChanged();
            }
        }

        public bool AllowUserInteraction
        {
            get { return this._allowUserInteraction; }
            set
            {
                if (this._allowUserInteraction == value) return;

                this._allowUserInteraction = value;
                this.RaisePropertyChanged();
            }
        }
        public int ProgressMaximum
        {
            get { return this._progressMaximum; }
            set
            {
                if (this._progressMaximum == value) return;

                this._progressMaximum = value;
                this.RaisePropertyChanged();
            }
        }
        public int ProgressValue
        {
            get { return this._progressValue; }
            set
            {
                if (this._progressValue == value) return;

                this._progressValue = value;
                this.RaisePropertyChanged();
            }
        }
        public string ProgressInfo
        {
            get { return this._progressInfo; }
            set
            {
                if (this._progressInfo == value) return;

                this._progressInfo = value;
                this.RaisePropertyChanged();
            }
        }

        #endregion

        #region Commands

        public ICommand OpenTextfileCommand { get; }
        public ICommand SaveTextfileCommand { get; }
        public ICommand AboutCommand { get; }
        public ICommand ShowRepetitionsWindowCommand { get; }
        public ICommand FindRepetitionsCommand { get; }
        public ICommand DisposeCommand { get; }
        public ICommand UpdateTextFromFlowDocumentCommand { get; }
        public ICommand UpdateHighlightCommand { get; }

        #endregion

        #region Window Factories

        public IModalDialogFactory<IModalDialog<bool?>, bool?> AboutWindowFactory { get; set; }
        public IModalFileDialogFactory SaveFileDialogFactory { get; set; }
        public IModalFileDialogFactory OpenFileDialogFactory { get; set; }
        public IMessageBoxProvider MessageBoxProvider { get; set; }
        public IDialogFactory<IDialog> RepetitionsWindowFactory { get; set; }

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

        private void OnExecuteShowRepetitionsWindowCommand()
        {
            if (this._repetitionsWindow == null)
            {
                this._repetitionsWindow = this.RepetitionsWindowFactory.GetInstance();
            }
            this._repetitionsWindow.Show();
        }

        private void OnExecuteFindRepetitionsCommand()
        {
            if (this._bw != null && this._bw.IsBusy)
            {
                throw new InvalidOperationException($"{nameof(this._bw)} is busy");
            }

            this.FoundOccurences = null;
            this.ProgressMaximum = this.Text.Length;
            this.BlockUserInteraction();

            this._bw = new BackgroundWorker();
            this._bw.DoWork += this.bw_DoWork;
            this._bw.WorkerSupportsCancellation = true;
            this._bw.RunWorkerCompleted += (sender, e) =>
            {
                if (e.Error != null)
                {
                    throw e.Error;
                }
                this.FoundOccurences = e.Result as ObservableCollection<Occurence>;
                this.ProgressValue = this.ProgressMaximum;
                this.UnblockUserInteraction();
            };
            this._bw.WorkerReportsProgress = true;
            this._bw.ProgressChanged += (s2, e2) =>
            {
                var count = (ProgressUpdateNotification)e2.UserState;
                this.ProgressValue = count.ProgressStepValue;
                this.ProgressMaximum = count.ProgressStepMaximum;
                this.ProgressInfo = $"Aufgabe {count.ProgressStep} von {count.ProgressStepCount} ({count.Info}) | Fortschritt: {count.ProgressStepValue} von {count.ProgressStepMaximum} ";
            };
            this._bw.RunWorkerAsync();
        }


        private void OnExecuteDisposeCommand()
        {
            this.Dispose();
        }

        private void OnExecuteUpdateTextFromFlowDocumentCommand()
        {
            if (!this._flowDocumentUpdateInProgress)
            {
                var txt = new TextRange(this.FlowDocument.ContentStart, this.FlowDocument.ContentEnd).Text;
                this.Text = txt;//.Substring(0, txt.Length - 2);//Remove \r\n from end
            }
        }

        private void OnExecuteUpdateHighlightCommand()
        {
            this._showHighlightedText = true;
            this.UpdateFlowDocumentHighlights();
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

        protected void BlockUserInteraction()
        {
            this.AllowUserInteraction = false;
        }
        protected void UnblockUserInteraction()
        {
            this.AllowUserInteraction = true;
        }

        private void UpdateFlowDocumentHighlights()
        {
            this._flowDocumentUpdateInProgress = true;

            this.FlowDocument.Blocks.Clear();

            if (this.Text == null || this.HighlightedText == null)
            {
                this.FlowDocument.Blocks.Add(new Paragraph(new Run(this.Text)));
            }
            else
            {
                var p = new Paragraph();
                var parts = this.Text.Split(new[] { this.HighlightedText }, StringSplitOptions.None);
                for (var i = 0; i < parts.Length; ++i)
                {
                    if (i != 0)
                    {
                        p.Inlines.Add(new Run()
                        {
                            Text = this.HighlightedText,
                            Background = Brushes.Yellow,
                        });
                    }

                    p.Inlines.Add(new Run()
                    {
                        Text = parts[i],
                        Background = Brushes.Transparent,
                    });
                }
                this.FlowDocument.Blocks.Add(p);
            }


            this._flowDocumentUpdateInProgress = false;
        }

        private void ResetFlowDocumentHighlight(bool textChganged)
        {
            if (this._showHighlightedText || textChganged)
            {
                this._flowDocumentUpdateInProgress = true;
                this._showHighlightedText = false;
                this.FlowDocument.Blocks.Clear();
                this.FlowDocument.Blocks.Add(new Paragraph(new Run(this.Text)));
                this._flowDocumentUpdateInProgress = false;
            }
        }
        
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            var text = this.GetText();
            var distinctCharacterCount = text.Distinct().Count();

            var subs = OccurenceFinder.GetAllSubstringsGroupedByLength(text, this.MinimalCharacterCountForRepetition).ToArray();//.ReportProgress(1/*every iteration*/, x => this._bw.ReportProgress(0, x));

            this._bw.ReportProgress(0, new ProgressUpdateNotification()
            {
                ProgressStepMaximum = text.Length,
                ProgressStepValue = 0,
                ProgressStep = 1,
                ProgressStepCount = 2,
                Info = "Wiederholungen werden gesucht"
            });

            var it = text.Length/100;
            var reps2 = OccurenceFinder.FindRepetitions(subs.ReportProgress(it, x => this._bw.ReportProgress(0, new ProgressUpdateNotification()
            {
                ProgressStepMaximum = text.Length,
                ProgressStepValue = x,
                ProgressStep = 1,
                ProgressStepCount = 2,
                Info = "Wiederholungen werden gesucht"
            })))
                .Where(x => x.Indices.Count >= this.MinimalRepetitionCount)
                .ToArray();

            if (e.Cancel) { return; }

            this._bw.ReportProgress(0, new ProgressUpdateNotification()
            {
                ProgressStepMaximum = reps2.Length,
                ProgressStepValue = 0,
                ProgressStep = 2,
                ProgressStepCount = 2,
                Info = "Wiederholungen werden gefiltert"
            });

            it = Math.Max(reps2.Length / 1000, 1);

            var occs = OccurenceFinder.FilterRepetitions(reps2.ReportProgress(it, x => this._bw.ReportProgress(0, new ProgressUpdateNotification()
            {
                ProgressStepMaximum = reps2.Length,
                ProgressStepValue = x,
                ProgressStep = 2,
                ProgressStepCount = 2,
                Info = "Wiederholungen werden gefiltert"
            })), reps2);
            if (e.Cancel) { return; }
            e.Result = new ObservableCollection<Occurence>(occs.Select(x =>
            {
                x.SetWeightingParameters(distinctCharacterCount);
                return x;
            }));
        }

        #endregion

        public void Dispose()
        {
            if (this._repetitionsWindow != null)
            {
                this._repetitionsWindow.Close();
                this._repetitionsWindow = null;
            }
            if (this._textHighlightWindow != null)
            {
                this._textHighlightWindow.Close();
                this._textHighlightWindow = null;
            }
        }
    }
}