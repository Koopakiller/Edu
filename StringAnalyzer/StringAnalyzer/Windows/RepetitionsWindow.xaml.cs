using System.ComponentModel;
using System.Windows;
using StringAnalyzer.WindowFactories;

namespace StringAnalyzer.Windows
{
    /// <summary>
    /// Interaction logic for RepetitionsWindow.xaml
    /// </summary>
    public partial class RepetitionsWindow : IDialog
    {
        public RepetitionsWindow()
        {
            this.InitializeComponent();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
