using StringAnalyzer.Windows;

namespace StringAnalyzer.WindowFactories
{
    public class RepetitionsWindowFactory : IDialogFactory<IDialog>
    {
        public IDialog GetInstance()
        {
            return new RepetitionsWindow();
        }
    }
}