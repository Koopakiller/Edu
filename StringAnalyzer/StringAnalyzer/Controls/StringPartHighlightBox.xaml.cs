using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace StringAnalyzer.Controls
{
    /// <summary>
    /// Interaction logic for StringPartHighlightBox.xaml
    /// </summary>
    public partial class StringPartHighlightBox : UserControl
    {
        public StringPartHighlightBox()
        {
            this.InitializeComponent();
        }



        public string Text
        {
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(StringPartHighlightBox), new PropertyMetadata("", OnTextChanged));



        public string HightlightedText
        {
            get { return (string)this.GetValue(HightlightedTextProperty); }
            set { this.SetValue(HightlightedTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HightlightText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HightlightedTextProperty =
            DependencyProperty.Register("HightlightedText", typeof(string), typeof(StringPartHighlightBox), new PropertyMetadata("", OnHightlightedTextChanged));

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as StringPartHighlightBox;
            control?.UpdateInlines();
        }

        private static void OnHightlightedTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as StringPartHighlightBox;
            control?.UpdateInlines();
        }

        private void UpdateInlines()
        {
            if (this.HightlightedText == null || this.Text == null)
            {
                return;
            }

            var p = new Paragraph();
            var parts = this.Text.Split(new[] { this.HightlightedText }, StringSplitOptions.None);
            for (var i = 0; i < parts.Length; ++i)
            {
                if (i != 0)
                {
                    p.Inlines.Add(new Run()
                    {
                        Text = this.HightlightedText,
                        Background = Brushes.Yellow,
                    });
                }

                p.Inlines.Add(new Run()
                {
                    Text = parts[i],
                    Background = Brushes.Transparent,
                });
            }
            this.TextBlock.Document = new FlowDocument(p);
        }
    }

}
