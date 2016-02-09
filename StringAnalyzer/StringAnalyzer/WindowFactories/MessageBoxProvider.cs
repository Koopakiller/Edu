using System.Windows;

namespace StringAnalyzer.WindowFactories
{
    public class MessageBoxProvider : IMessageBoxProvider
    {
        public MessageBoxResult Show(string caption, string text)
            => this.Show(caption, text, MessageBoxButton.OK, MessageBoxImage.None);

        public MessageBoxResult Show(string caption, string text, MessageBoxButton buttons)
            => this.Show(caption, text, buttons, MessageBoxImage.None);

        public MessageBoxResult Show(string caption, string text, MessageBoxImage image)
            => this.Show(caption, text, MessageBoxButton.OK, image);

        public MessageBoxResult Show(string caption, string text, MessageBoxButton buttons, MessageBoxImage image)
        {
            return MessageBox.Show(caption, text, buttons, image);
        }
    }
}