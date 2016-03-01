using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringAnalyzer.Helper;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new[] { 1, 2, 4 };
            var s = new[]
            {
             //   Tuple.Create( "DNS", new[]
             //   {
                //    "TGCCTTGTCAGTGCTATTACCTACATGTAAGCCTTAACGAACCAATAGAGGGCGGAGACACGTACCCTCGAGAAATGTAATTTCGATTCTGAAGGGGAGT",
                //    "CATCATACCGAGACGATATGAGCAACTCGGAGCGACCGAAGTTGATGTCACGCATGTGAGTGTGGGACCCTATTTCGGACTTAGCCCAGGCGGGATACACGAGTCAGGAGTCGTTTGACAATGGAAGCGTCCGGGGCCCAGGAATAATGCCTTTCTCTTAGCAACCGAAAGCTACCTGTTTGGCGCGGCTCCCAGCGTTGCAGGGATGGTTTATCTTTGTGAGCTCTTGATTGCGTATGCCTTGTCAGTGCTATTACCTACATGTAAGCCTTAACGAACCAATAGAGGGCGGAGACACGTACCCTCGAGAAATGTAATTTCGATTCTGAAGGGGAGTGAACGACACGCGTAAGGGGCCGGGAACCCCACACGGTGGCGGGGGTAAGATAGCCTGGAGTTTTCACGCCTATAGGATACTATCGATTTGAACTGGTGACCCTACTACGGTGGTTGAGCCAGGGCGGAGTTCTGTCCTTGTGACTCGGATGCCTCTTATGCAG",
                //  "CATCATACCGAGACGATATGAGCAACTCGGAGCGACCGAAGTTGATGTCACGCATGTGAGTGTGGGACCCTATTTCGGACTTAGCCCAGGCGGGATACACGAGTCAGGAGTCGTTTGACAATGGAAGCGTCCGGGGCCCAGGAATAATGCCTTTCTCTTAGCAACCGAAAGCTACCTGTTTGGCGCGGCTCCCAGCGTTGCAGGGATGGTTTATCTTTGTGAGCTCTTGATTGCGTATGCCTTGTCAGTGCTATTACCTACATGTAAGCCTTAACGAACCAATAGAGGGCGGAGACACGTACCCTCGAGAAATGTAATTTCGATTCTGAAGGGGAGTGAACGACACGCGTAAGGGGCCGGGAACCCCACACGGTGGCGGGGGTAAGATAGCCTGGAGTTTTCACGCCTATAGGATACTATCGATTTGAACTGGTGACCCTACTACGGTGGTTGAGCCAGGGCGGAGTTCTGTCCTTGTGACTCGGATGCCTCTTATGCAGTTCTATCAAGGCGGCTAATTTACATAAGCAACTTAGCCCGTGCTGCGACAGGCCTGCCATTGTACCGGCTGGGCTATGGCGTCACGAGAGATAGGCTCGATATGGTTTCTTGGGCACTTGCTGGGGCAGGCCACCCTGCCTACCTCCGAGATGTTTCATACGCGCCGCATAGCCGAGCTACTATGCTACCAGGCTCTGTGTTCCGCCAATCACTGGGCAACCTTTCTACCCAGCCCGTCCTTAGATCTGCTCGTACAGCACCGTCAGACCAGTTAGCCGTAACAGTCTATCTCCTGACAACACCCACTGGAGTGACTCACGGAGTACGTCCAAGCTGATCGGACTGAGCGTTAGCGATCCGTTCCACCCAGGGGTAGGGAAGCCGGAGAGTTGGTGGGATAGCATAACGCAGAACAGGTTATAGATTATACACTACAACCCGTTGCGGCGAATCTCCTTAAACCTTACATTCACCCTAATCCTCAACAAGTCACAACCTA",
            //    }),
                Tuple.Create( "ABC", new[]
                {
            //        "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut l",
                    "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorde ipkum dogor sit amet, cunsitetur sediptcing ekitr, spd djam nouumy eismod tmhnor invwdunt ut lwbire et dowore magga aliquaam erjt, sed dirz voluptua. Af taro eos et ahhusam et jzsto dpo dolaros te j",
                //  "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu fur",
                }),
            };

            Console.WriteLine("Parallel");
            foreach (var s2 in s)
            {
                foreach (var s1 in s2.Item2)
                {
                    var subs = OccurenceFinder.GetAllSubstringsGroupedByLength(s1, 5);
                    var x = OccurenceFinder.FindRepetitions(subs).ToArray();
                    foreach (var a1 in a)
                    {
                        var sw = new Stopwatch();
                        sw.Start();

                        var y = OccurenceFinder.FilterRepetitions(x, x, a1).ToArray();

                        sw.Stop();
                        Console.WriteLine($"Art: {s2.Item1}\t Textlength: {s1.Length}\t Cores: {a1}\t Time: {sw.ElapsedMilliseconds}ms");
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Sequenziell");
            foreach (var s2 in s)
            {
                foreach (var s1 in s2.Item2)
                {
                    var subs = OccurenceFinder.GetAllSubstringsGroupedByLength(s1, 5);
                    var x = OccurenceFinder.FindRepetitions(subs).ToArray();
                    foreach (var a1 in a)
                    {
                        var sw = new Stopwatch();
                        sw.Start();

                        var y = OccurenceFinder.FilterRepetitions(x, x).ToArray();

                        sw.Stop();
                        Console.WriteLine($"Art: {s2.Item1}\t Textlength: {s1.Length}\t Cores: {a1}\t Time: {sw.ElapsedMilliseconds}ms");
                    }
                }
            }
            Console.ReadKey();
        }
        //static void Main(string[] args)
        //{
        //    var a = new[] { 1, 2, 4 };
        //    var s = new[]
        //    {
        //        Tuple.Create( "DNS", new[]
        //        {
        //            "TGCCTTGTCAGTGCTATTACCTACATGTAAGCCTTAACGAACCAATAGAGGGCGGAGACACGTACCCTCGAGAAATGTAATTTCGATTCTGAAGGGGAGT",
        //            "CATCATACCGAGACGATATGAGCAACTCGGAGCGACCGAAGTTGATGTCACGCATGTGAGTGTGGGACCCTATTTCGGACTTAGCCCAGGCGGGATACACGAGTCAGGAGTCGTTTGACAATGGAAGCGTCCGGGGCCCAGGAATAATGCCTTTCTCTTAGCAACCGAAAGCTACCTGTTTGGCGCGGCTCCCAGCGTTGCAGGGATGGTTTATCTTTGTGAGCTCTTGATTGCGTATGCCTTGTCAGTGCTATTACCTACATGTAAGCCTTAACGAACCAATAGAGGGCGGAGACACGTACCCTCGAGAAATGTAATTTCGATTCTGAAGGGGAGTGAACGACACGCGTAAGGGGCCGGGAACCCCACACGGTGGCGGGGGTAAGATAGCCTGGAGTTTTCACGCCTATAGGATACTATCGATTTGAACTGGTGACCCTACTACGGTGGTTGAGCCAGGGCGGAGTTCTGTCCTTGTGACTCGGATGCCTCTTATGCAG",
        //            "CATCATACCGAGACGATATGAGCAACTCGGAGCGACCGAAGTTGATGTCACGCATGTGAGTGTGGGACCCTATTTCGGACTTAGCCCAGGCGGGATACACGAGTCAGGAGTCGTTTGACAATGGAAGCGTCCGGGGCCCAGGAATAATGCCTTTCTCTTAGCAACCGAAAGCTACCTGTTTGGCGCGGCTCCCAGCGTTGCAGGGATGGTTTATCTTTGTGAGCTCTTGATTGCGTATGCCTTGTCAGTGCTATTACCTACATGTAAGCCTTAACGAACCAATAGAGGGCGGAGACACGTACCCTCGAGAAATGTAATTTCGATTCTGAAGGGGAGTGAACGACACGCGTAAGGGGCCGGGAACCCCACACGGTGGCGGGGGTAAGATAGCCTGGAGTTTTCACGCCTATAGGATACTATCGATTTGAACTGGTGACCCTACTACGGTGGTTGAGCCAGGGCGGAGTTCTGTCCTTGTGACTCGGATGCCTCTTATGCAGTTCTATCAAGGCGGCTAATTTACATAAGCAACTTAGCCCGTGCTGCGACAGGCCTGCCATTGTACCGGCTGGGCTATGGCGTCACGAGAGATAGGCTCGATATGGTTTCTTGGGCACTTGCTGGGGCAGGCCACCCTGCCTACCTCCGAGATGTTTCATACGCGCCGCATAGCCGAGCTACTATGCTACCAGGCTCTGTGTTCCGCCAATCACTGGGCAACCTTTCTACCCAGCCCGTCCTTAGATCTGCTCGTACAGCACCGTCAGACCAGTTAGCCGTAACAGTCTATCTCCTGACAACACCCACTGGAGTGACTCACGGAGTACGTCCAAGCTGATCGGACTGAGCGTTAGCGATCCGTTCCACCCAGGGGTAGGGAAGCCGGAGAGTTGGTGGGATAGCATAACGCAGAACAGGTTATAGATTATACACTACAACCCGTTGCGGCGAATCTCCTTAAACCTTACATTCACCCTAATCCTCAACAAGTCACAACCTA",
        //        }),
        //        Tuple.Create( "ABC", new[]
        //        {
        //            "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut l",
        //            "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et e",
        //            "Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Duis autem vel eum iriure dolor in hendrerit in vulputate velit esse molestie consequat, vel illum dolore eu fur",
        //        }),
        //    };

        //    Console.WriteLine("Parallel");
        //    foreach (var s2 in s)
        //    {
        //        foreach (var a1 in a)
        //        {
        //            foreach (var s1 in s2.Item2)
        //            {
        //                var sw = new Stopwatch();
        //                sw.Start();
        //                var subs = OccurenceFinder.GetAllSubstringsGroupedByLength(s1, 5);
        //                var x = OccurenceFinder.FindRepetitions(subs, a1).ToArray();
        //                sw.Stop();
        //                Console.WriteLine($"Art: {s2.Item1}\t Textlength: {s1.Length}\t Cores: {a1}\t Time: {sw.ElapsedMilliseconds}ms");
        //            }
        //        }
        //    }
        //    Console.WriteLine();
        //    Console.WriteLine("Sequenziell");
        //    foreach (var s2 in s)
        //    {
        //        foreach (var a1 in a)
        //        {
        //            foreach (var s1 in s2.Item2)
        //            {
        //                var sw = new Stopwatch();
        //                sw.Start();
        //                var subs = OccurenceFinder.GetAllSubstringsGroupedByLength(s1, 5);
        //                var x = OccurenceFinder.FindRepetitions(subs).ToArray();
        //                sw.Stop();
        //                Console.WriteLine($"Art: {s2.Item1}\t Textlength: {s1.Length}\t Cores: {a1}\t Time: {sw.ElapsedMilliseconds}ms");
        //            }
        //        }
        //    }
        //    Console.ReadKey();
        //}
    }
}
