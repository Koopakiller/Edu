namespace StringAnalyzer.WindowFactories
{
    public interface IModalDialogFactory<out TDialog, out TResult> where TDialog : IModalDialog<TResult>
    {
        TDialog GetInstance();
    }
}