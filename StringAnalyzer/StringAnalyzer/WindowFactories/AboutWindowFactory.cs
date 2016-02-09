using StringAnalyzer.Windows;

namespace StringAnalyzer.WindowFactories
{
    public class AboutWindowFactory : IModalDialogFactory<IModalDialog<bool?>, bool?>
    {
        public IModalDialog<bool?> GetInstance()
        {
            return new AboutWindow();
        }
    }
}