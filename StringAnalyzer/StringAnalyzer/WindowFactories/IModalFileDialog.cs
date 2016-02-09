namespace StringAnalyzer.WindowFactories
{
    public interface IModalFileDialog : IModalDialog<bool?>
    {
        string Filter { get; set; }
        string FileName { get; set; }
    }
}