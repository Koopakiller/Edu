using System.Windows;

namespace StringAnalyzer.WindowFactories
{
    public interface IMessageBoxProvider
    {
        MessageBoxResult Show(string caption, string text);
        MessageBoxResult Show(string caption, string text, MessageBoxButton buttons);
        MessageBoxResult Show(string caption, string text, MessageBoxImage image);
        MessageBoxResult Show(string caption, string text, MessageBoxButton buttons, MessageBoxImage image);
    }
}