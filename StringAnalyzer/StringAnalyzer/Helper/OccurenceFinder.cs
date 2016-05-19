using System;
using System.Collections.Generic;
using System.Linq;

namespace StringAnalyzer.Helper
{
    public class OccurenceFinder
    {
        /// <summary>
        /// Filtert eine Liste von Vorkommen danach, ob die Vorkommen in anderen 
        /// Teilzeichenfolgen enthalten sind, die genauso oft vorkommen.
        /// </summary>
        /// <param name="repetitions">Eine zu filtrnde Liste von Teilzeichenfolgen. 
        /// Diese wird einmal durchlaufen</param>
        /// <param name="repetitions2">Die Vergleichsliste zu <paramref name="repetitions"/>. 
        /// Die Listen sollten die gleichen Elemente enbthalten, diese hier 
        /// wird jedoch mehrfach durchlaufen.</param>
        /// <param name="cores">Die Anzahl der zu benutzenden CPU-Kerne. 0 verwendet 
        /// alle zur Verfügung stehenden.</param>
        /// <returns>Eine Auflistung mit den Elementen aus <paramref name="repetitions"/>
        /// ohne denen, deren Teilzeichenfolge in einer anderen, gleichhäufig enthaltenen, 
        /// auftritt.</returns>
        public static IEnumerable<Occurence> FilterRepetitions(IEnumerable<Occurence> repetitions, IEnumerable<Occurence> repetitions2, int cores = 0)
        {
            var enu = repetitions.AsParallel();
            if (cores > 0)
            {
                enu = enu.WithDegreeOfParallelism(cores);
            }
            return enu.Where(first => !repetitions2.Any(second => first.Indices.Count == second.Indices.Count
                                                               && first.Text.Length != second.Text.Length
                                                               && second.Text.Contains(first.Text)));
        }

        /// <summary>
        /// Filtert eine Liste von Vorkommen nach der Anzahl der Vorkommen einer Teilzeichenfolge.
        /// </summary>
        /// <param name="occurences">Eine zu filtrnde Liste von Teilzeichenfolgen.</param>
        /// <param name="minCount">Die Mindestanzahl an Vorkommen, damit ein Element aus 
        /// <paramref name="occurences"/> in die Ergebnisauflistung übernommen wird.</param>
        /// <returns>Eine Auflistung mit den Elementen aus <paramref name="occurences"/> 
        /// mit mindestens der angegebenen Anzahl an Vorkommen.</returns>
        public static IEnumerable<Occurence> FilterForFrequency(IEnumerable<Occurence> occurences, int minCount)
        {
            return occurences.Where(occurence => occurence.Indices.Count >= minCount);

            //ohne LinQ Methodensyntax:
            //foreach (var occurence in occurences)
            //{
            //    if (occurence.Indices.Count >= minCount)
            //    {
            //        yield return occurence;
            //    }
            //}
        }

        /// <summary>
        /// Durchsucht die nach länge gruppierte Liste von Teilzeichenolgen nach Wiederholungen.
        /// </summary>
        /// <param name="substringsGroupedByLength">Die nach länge gruppierte Liste von Teilzeichenfolgen.</param>
        /// <param name="cores">Die Anzahl der zu benutzenden CPU-Kerne. 0 verwendet 
        /// alle zur Verfügung stehenden.</param>
        /// <returns>Eine Liste aller Teilzeichenfolgen in <paramref name="substringsGroupedByLength"/>,
        /// wobei wiederholungen in einem Element zusammen gefasst wurden.</returns>
        public static IEnumerable<Occurence> FindRepetitions(IEnumerable<IEnumerable<Substring>> substringsGroupedByLength, int cores = 0)
        {
            var enu = substringsGroupedByLength.AsParallel();
            if (cores > 0)
            {
                enu = enu.WithDegreeOfParallelism(cores);
            }
            foreach (var group in enu)
            {
                var items = group.ToArray();
                for (var index1 = 0; index1 < items.Length; ++index1)
                {
                    if (items[index1] == null) { continue; }
                    var occ = new Occurence(items[index1].Text);
                    occ.Indices.Add(items[index1].Index);
                    for (var index2 = index1 + 1; index2 < items.Length; ++index2)
                    {
                        if (items[index2] == null || items[index1].Text != items[index2].Text) { continue; }

                        occ.Indices.Add(items[index2].Index);
                        items[index2] = null;
                    }
                    yield return occ;
                }
            }
        }

        /// <summary>
        /// Gibt eine nach Länge gruppierte Liste von Teilzeichenfolgen zurück.
        /// </summary>
        /// <param name="input">Der in Teilzeichenolgen zu teilende Eingabetext.</param>
        /// <param name="minLength">Die minimale Länge einer Teilzeichenfolge.</param>
        /// <returns>Eine nach Länge gruppierte Liste von Teilzeichenfolgen.</returns>
        public static IEnumerable<IEnumerable<Substring>> GetAllSubstringsGroupedByLength(string input, int minLength)
        {
            return GetPossiblePartLengths(input.Length, minLength).Select(length => GetSubstringsWithLength(input, length));

            //ohne LinQ Methodensyntax:
            //foreach (var length in GetPossiblePartLengths(input.Length, minLength))
            //{
            //    yield return GetSubstringsWithLength(input, length);
            //}
        }

        /// <summary>
        /// Gibt die Teilzeichenfolgen des angegebenen Texts mit der gegebenen Länge zurück.
        /// </summary>
        /// <param name="input">Der zu teilende Eingabetext.</param>
        /// <param name="length">Die Länger jeder Teilzeichenfolge.</param>
        /// <returns>Eine Auflistung aller möglichen Teilzeichenfolgen mit der angegebenen Länge.</returns>
        private static IEnumerable<Substring> GetSubstringsWithLength(string input, int length)
        {
            var chars = input.ToCharArray();
            for (var index = 0; index <= chars.Length - length; ++index)
            {
                yield return new Substring(new string(chars, index, length), index);
            }
        }

        /// <summary>
        /// Gibt die möglichen Längen der Teilzeichenfolgen zurück.
        /// </summary>
        /// <param name="inputLength">Die Gesamtzahl an Zeichen im Eingabetext.</param>
        /// <param name="minLength">Die Mindestlänge einer Teilzeichenfolge. 
        /// Wenn nicht angegeben, wird 1 verwendet.</param>
        /// <returns>Eine Auflistung aller möglichen Längen von Teilzeichenfolgen.</returns>
        private static IEnumerable<int> GetPossiblePartLengths(int inputLength, int minLength = 1)
        {
            for (var length = minLength; length <= inputLength; ++length)
            {
                yield return length;
            }
        }
    }
}