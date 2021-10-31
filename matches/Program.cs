using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace notter
{
    class Program
    {

        static byte[] numerals = new byte[]{ 6, 2, 5, 5, 4, 5, 6, 3, 7, 6 };

        static IDictionary<int, BigInteger> answers = Enumerable.Range(0, 10).ToDictionary(i => i, i => (BigInteger)0);

        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var nofMatches = int.Parse(args[0]);

            for (var i = 0; i <= nofMatches; i++)
            {
                answers[i] = GetMax(i);
            }

            var answer = answers[nofMatches];

            // Remove leading zeroes
            for (var zeroMatches = 6; zeroMatches < nofMatches; zeroMatches += 6)
            {
                answer -= answers[nofMatches - zeroMatches];
            }

            stopwatch.Stop();

            await Console.Out.WriteLineAsync($"{nofMatches} -> {answer} in {stopwatch.Elapsed.TotalMilliseconds} ms");
        }

        private static BigInteger GetMax(int nofMatches) {
            BigInteger sum = 0;
            for (var numeral = 0; numeral < 10; numeral++)
            {
                var matchCount = numerals[numeral];
                if (matchCount <= nofMatches) {
                    sum += answers[nofMatches-matchCount] + 1;
                }

            }
            return sum;

            // return Enumerable.Range(0, 10).Select(matchCount => matchCount[matchCount]).Where(matchCount => matchCount > i).Sum(x => answers[i - x]);
        }
    }
}
