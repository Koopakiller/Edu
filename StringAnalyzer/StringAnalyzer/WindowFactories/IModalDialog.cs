namespace StringAnalyzer.WindowFactories
{
    public interface IModalDialog<out TResult>
    {
        TResult ShowDialog();
    }
}