using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace StringAnalyzer.Controls
{
    public class BindableRichTextBox : RichTextBox
    {
        public static readonly DependencyProperty DocumentProperty = DependencyProperty.Register("Document", typeof(FlowDocument), typeof(BindableRichTextBox), new FrameworkPropertyMetadata(null, OnDocumentChanged));

        public new FlowDocument Document
        {
            get
            {
                return (FlowDocument)this.GetValue(DocumentProperty);
            }

            set
            {
                this.SetValue(DocumentProperty, value);
            }
        }

        public static void OnDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var rtb = (RichTextBox)d;
            rtb.Document = (FlowDocument)e.NewValue;
        }
    }
}
