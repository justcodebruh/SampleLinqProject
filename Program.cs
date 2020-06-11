// Program.cs
using System;
using System.Collections.Generic;
using System.Linq;
using LinqFaroShuffle;

namespace SampleLinqProject
{
    class Program
    {
        // Program.cs
        static void Main(string[] args)
        {
            var startingDeck = Suits()
                .SelectMany(suit => Ranks()
                .Select( rank => new { Suit = suit, Rank = rank } ))
                .LogQuery("Starting Deck")
                .ToArray();

            // Display each card that we've generated and placed in startingDeck in the console
            foreach (var card in startingDeck)
            {
                Console.WriteLine(card);
            }
            
            Console.WriteLine();
            var times = 0;
            // We can re-use the shuffle variable from earlier, or you can make a new one
            var shuffle = startingDeck;
            do
            {
                // Out shuffle
                /*
                shuffle = shuffle.Take(26)
                    .LogQuery("Top Half")
                    .InterleaveSequenceWith(shuffle.Skip(26)
                    .LogQuery("Bottom Half"))
                    .LogQuery("Shuffle");
                */

                // In shuffle

                shuffle = shuffle.Skip(26).LogQuery("Bottom Half")
                .InterleaveSequenceWith(shuffle.Take(26).LogQuery("Top Half"))
                .LogQuery("Shuffle")
                .ToArray();

                foreach (var card in shuffle)
                {
                    Console.WriteLine(card);
                }
                times++;
                Console.WriteLine();

            } while (!startingDeck.SequenceEquals(shuffle));

            Console.WriteLine(times);
        }

        static IEnumerable<string> Suits()
        {
            yield return "clubs";
            yield return "diamonds";
            yield return "hearts";
            yield return "spades";
        }

        static IEnumerable<string> Ranks()
        {
            yield return "two";
            yield return "three";
            yield return "four";
            yield return "five";
            yield return "six";
            yield return "seven";
            yield return "eight";
            yield return "nine";
            yield return "ten";
            yield return "jack";
            yield return "queen";
            yield return "king";
            yield return "ace";
        }
    }
}
