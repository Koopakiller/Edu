namespace StringAnalyzer.Helper
{
    public class Substring
    {
        public Substring(string text, int index)
        {
            this.Text = text;
            this.Index = index;
        }

        public string Text { get; }

        public int Index { get; }

        public int Length => this.Text.Length;
    }
}