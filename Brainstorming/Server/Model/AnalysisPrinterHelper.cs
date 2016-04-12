using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Koopakiller.Apps.Brainstorming.Server.ViewModel;

namespace Koopakiller.Apps.Brainstorming.Server.Model
{
    public class AnalysisPrinterHelper
    {
        private readonly IReadOnlyCollection<SuggestionItemGroup> _groups;
        private readonly string _topic;

        public AnalysisPrinterHelper(IReadOnlyCollection<SuggestionItemGroup> groups, string topic)
        {
            this._groups = groups;
            this._topic = topic;
        }

        public FlowDocument CreateFlowDocument(double pageWidth)
        {
            var doc = new FlowDocument { FontFamily = new FontFamily("Calibri") };

            doc.Blocks.Add(new Paragraph(new Run(this._topic) {FontSize = 18, FontWeight = FontWeights.Bold}));

            doc.Blocks.Add(this.CreateDataList());

            //doc.Blocks.Add(this.CreateColumnChart(pageWidth - 2 * 96));
            //doc.Blocks.Add(this.CreatePieChart(pageWidth - 2 * 96));

            return doc;
        }

        public Block CreateDataList()
        {
            var list = new List {MarkerStyle = TextMarkerStyle.None};
            foreach (var sug in this._groups.OrderByDescending(x=>x.Count))
            {
                var p = new Paragraph();

                p.Inlines.Add(new Run(new string('▄', sug.Count)));
                p.Inlines.Add(new LineBreak());

                p.Inlines.Add(new Run(sug.SuggestionItems.First().Text) { FontWeight = FontWeights.Bold });
                p.Inlines.Add(new LineBreak());

                if (sug.SuggestionItems.Count > 1)
                {
                    p.Inlines.Add(new Run("Synonyme: "));
                    p.Inlines.Add(new Run(sug.SuggestionItems.Skip(1).First().Text) { FontStyle = FontStyles.Italic });
                    foreach (var syn in sug.SuggestionItems.Skip(2))
                    {
                        p.Inlines.Add(new Run(", "));
                        p.Inlines.Add(new Run(syn.Text) { FontStyle = FontStyles.Italic });
                    }
                    p.Inlines.Add(new LineBreak());
                }

                p.Inlines.Add(new Run("Anzahl: "));
                p.Inlines.Add(new Run(sug.Count.ToString()) { FontWeight = FontWeights.Bold });
                p.Inlines.Add(new LineBreak());

                var li = new ListItem();
                li.Blocks.Add(p);
                list.ListItems.Add(li);
            }
            return list;
        }

        public Block CreateColumnChart(double width)
        {
            var cs = new ColumnSeries
            {
                ItemsSource = this._groups,
                IndependentValueBinding = new Binding("SuggestionItems[0].Text"),
                DependentValueBinding = new Binding("Count"),
            };
            cs.Refresh();
            return new BlockUIContainer(new Chart
            {
                DataContext = this._groups,
                Width = width,
                Height = width * 0.60,
                Series =
                {
                   cs,
                }
            });
        }
        public Block CreatePieChart(double width)
        {
            return new BlockUIContainer(new Chart
            {
                DataContext = this._groups,
                Width = width,
                Height = width * 0.60,
                Series =
                {
                    new PieSeries
                    {
                        ItemsSource = this._groups,
                        IndependentValueBinding = new Binding("SuggestionItems[0].Text"),
                        DependentValueBinding = new Binding("Count"),
                    },
                }
            });
        }

        public void Print()
        {
            var printDlg = new PrintDialog();
            if (printDlg.ShowDialog() != true) { return; }
            var capabilities = printDlg.PrintQueue.GetPrintCapabilities(printDlg.PrintTicket);

            var doc = this.CreateFlowDocument(capabilities.PageImageableArea.ExtentWidth);
            doc.PagePadding = new Thickness(96);
            //    doc.ColumnGap = 10;
            //    doc.ColumnWidth = capabilities.PageImageableArea.ExtentWidth / 3 - 20;
            IDocumentPaginatorSource idpSource = doc;
            printDlg.PrintDocument(idpSource.DocumentPaginator, $"Auswertung zum Thema {this._topic}");
        }
    }
}
