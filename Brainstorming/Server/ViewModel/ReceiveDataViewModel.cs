using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Input;
using System.Xml.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using Koopakiller.Apps.Brainstorming.Server.Model;
using Koopakiller.Apps.Brainstorming.Shared.ViewModel;
using Microsoft.Win32;

namespace Koopakiller.Apps.Brainstorming.Server.ViewModel
{
    public class ReceiveDataViewModel : CurrentViewModelBase
    {
        #region .ctor

        public ReceiveDataViewModel(Shared.Model.Server server)
        {
            this._server = server;
            this.GroupItemsCommand = new RelayCommand<object>(this.OnGroupItems);

            this.Suggestions = new ObservableCollection<SuggestionItemGroup>();
            this.StartServerCommand = new RelayCommand(this.StartServer);

            this.MenuItems.Insert(0, new MenuItemViewModel("Auswertung")
            {
                SubItems =
                {
                    new MenuItemViewModel("Drucken")
                    {
                        Command =new RelayCommand(this.OnPrintAnalysis),
                    },
                    new MenuItemViewModel("Daten an Clients senden")
                    {
                        Command =new RelayCommand(this.OnSendAnalysisToClients),
                    },
                },
            });
            this.MenuItems.Insert(0, new MenuItemViewModel("Server")
            {
                SubItems =
                {
                    (this._stopServer = new MenuItemViewModel("Server anhalten")
                    {
                        IsCheckable=true,
                        Command = new RelayCommand(this.OnStopServer),
                    }),
                },
            });
            this.MenuItems.Insert(0, new MenuItemViewModel("Datei")
            {
                SubItems =
                {
                    new MenuItemViewModel("Daten Exportieren")
                    {
                        Command =new RelayCommand(this.OnExportData),
                    },
                    new MenuItemViewModel("Daten Importieren")
                    {
                        Command =new RelayCommand(this.OnImportData),
                    },
                },
            });
        }

        private void OnSendAnalysisToClients()
        {
            this._server.SendData("TEST");
        }

        private readonly MenuItemViewModel _stopServer;

        private void OnStopServer()
        {
            this._server.IsStopped = this._stopServer.IsChecked;
        }

        private void OnImportData()
        {
            //TODO: UI auslagern
            try
            {
                var ofd = new OpenFileDialog
                {
                    Filter = "XML-Datei|*.xml"
                };
                if (ofd.ShowDialog() != true)
                {
                    return;
                }

                var doc = XDocument.Load(ofd.FileName);
                this.Suggestions.Clear();
                if (doc.Root == null) { return; }
                foreach (var group in doc.Root.Elements("Group"))
                {
                    var sig = new SuggestionItemGroup();
                    foreach (var item in @group.Elements("Item"))
                    {
                        var si = new SuggestionItem();
                        foreach (var ip in item.Elements("IP"))
                        {
                            si.IPAddresses.Add(IPAddress.Parse(ip.Value));
                        }
                        si.Text = item.Attribute("Text").Value;
                        sig.SuggestionItems.Add(si);
                    }
                    this.Suggestions.Add(sig);
                }
            }
            catch (Exception ex)
            {
                this.ShowMessage("Unerwarteter Fehler",
                    $"Die Datei konnte aus einem unbekannten Grund nicht geöffnet werden. \n\nTechnische Informationen:\n{ex.Message}");
            }
        }

        private void OnExportData()
        {
            //TODO: UI auslagern
            try
            {
                var sfd = new SaveFileDialog
                {
                    Filter = "XML-Datei|*.xml"
                };
                if (sfd.ShowDialog() != true) { return; }

                var doc = new XDocument(
                    new XElement("Root", this.Suggestions.Select(x =>
                        new XElement("Group", x.SuggestionItems.Select(y =>
                            new XElement("Item", y.IPAddresses.Select(z =>
                                new XElement("IP", z.ToString()))
                                .Cast<object>().Concat(new[] { new XAttribute("Text", y.Text), }).ToArray()))
                            .Cast<object>().ToArray()))
                        .Cast<object>().ToArray()));
                doc.Save(sfd.FileName);
            }
            catch (Exception ex)
            {
                this.ShowMessage("Unerwarteter Fehler",
                    $"Die Datei konnte aus einem unbekannten Grund nicht geschrieben werden. \n\nTechnische Informationen:\n{ex.Message}");
            }

        }

        private void OnPrintAnalysis()
        {
            //TODO: UI auslagern
            try
            {
                var aph = new AnalysisPrinterHelper(this.Suggestions, this.Topic);
                aph.Print();
            }
            catch (Exception ex)
            {
                this.ShowMessage("Unerwarteter Fehler",
                    $"Die Daten konnten aus einem unbekannten Grund nicht gedruckt werden. \n\nTechnische Informationen:\n{ex.Message}");
            }
        }

        #endregion

        #region Fields

        private readonly Shared.Model.Server _server;
        private string _topic;

        #endregion

        #region Properties

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

        public ObservableCollection<SuggestionItemGroup> Suggestions { get; }

        public RelayCommand<object> GroupItemsCommand { get; }

        public ICommand StartServerCommand { get; }

        #endregion

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
#if !DEBUG
                    if (item2.IPAddresses.All(x => !Equals(x, ip)))
#endif
                    {
                        item2.IPAddresses.Add(ip);
                    }
                });
            });
        }

    }
}
