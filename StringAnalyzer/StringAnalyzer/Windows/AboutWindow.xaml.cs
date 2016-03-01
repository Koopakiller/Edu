using StringAnalyzer.WindowFactories;

namespace StringAnalyzer.Windows
{
    public partial class AboutWindow: IModalDialog<bool?>
    {
        public AboutWindow()
        {
            this.InitializeComponent();
        }
    }
}
