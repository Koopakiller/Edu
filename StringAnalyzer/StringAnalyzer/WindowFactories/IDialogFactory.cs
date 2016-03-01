namespace StringAnalyzer.WindowFactories
{
    public interface IDialogFactory<out TDialog> where TDialog : IDialog
    {
        TDialog GetInstance();
    }
}