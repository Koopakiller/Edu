using System;
using System.Collections.Generic;
using System.Linq;

namespace StringAnalyzer.Helper
{
    public class Occurence
    {
        private int _distinctCharacterCount;

        public Occurence(string text)
        {
            this.Text = text;
            this.Indices = new List<int>();
        }

        public string Text { get; }

        public IList<int> Indices { get; }

        public void SetWeightingParameters(int distinctCharacterCount)
        {
            this._distinctCharacterCount = distinctCharacterCount;
        }

        public double Weighting => Math.Pow(this._distinctCharacterCount, this.Text.Length) * this.Indices.Count;

        public override string ToString()
        {
            return $"{this.Text} - {string.Join(", ", this.Indices)}";
        }

        public override int GetHashCode()
        {
            return this.Text.GetHashCode() ^ this.Indices.Aggregate(0, (current, i) => current ^ i * 32);
        }

        public override bool Equals(object obj)
        {
            var occurence = obj as Occurence;
            return occurence != null && occurence.Text == this.Text && this.Indices.OrderBy(x => x).SequenceEqual(occurence.Indices.OrderBy(x => x));
        }
    }
}