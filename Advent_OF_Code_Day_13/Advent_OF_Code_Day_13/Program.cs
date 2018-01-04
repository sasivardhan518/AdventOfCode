using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent_OF_Code_Day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            int caughtCount = 0;
            int delayCount = 0;
            var input = System.IO.File.ReadAllLines(@"..\..\input.txt");
            var pattern = @"([0-9][0-9]?): ([0-9][0-9]?)\|?";
            List<DepthRange> depthRange = new List<DepthRange>();
            var rx = new Regex(pattern);
            MatchCollection matches = rx.Matches(String.Join("|", input));
            foreach (Match match in matches)
            {
                GroupCollection g = match.Groups;
                depthRange.Add(new DepthRange() { depth = Convert.ToInt32(g[1].Value), range = Convert.ToInt32(g[2].Value), index = 0 });
            }
            int noOfDepths = depthRange.Select(x => x.depth).Max();

            // while (true)
            // {
            // bool caught = false;
            int severity = 0;
            for (int i = delayCount; i <= delayCount + noOfDepths; i++)
            {
                DepthRange dr = depthRange.FirstOrDefault(x => x.depth == i - delayCount);
                int? range = dr?.range;
                if (range == null)
                {
                    continue;
                }
                else
                {
                    if (i % ((range - 1) * 2) == 0)
                    {
                        severity += dr.depth * dr.range;
                        // caught = true;
                        // break;
                    }
                }
            }

            //     if (!caught)
            //     {
            //         break;
            //     }
            // 
            //     delayCount++;
            // }
            Console.WriteLine("Final Delay {0}", severity);
            //int noOfDepths = depthRange.Select(x => x.depth).Max();
            //bool notCaught = false;
            //while (!notCaught)
            //{
            //    caughtCount = 0;

            //    for (int i = 1; i <= noOfDepths + 1 + delayCount; i++)
            //    {
            //        DepthRange elementCaught = depthRange.FirstOrDefault(x => x.index == 0 && x.depth == (i - 1 - delayCount));
            //        if (elementCaught != null)
            //        //if (i - 1 - delayCount >= 0 && i - 1 - delayCount < depthRange.Count && depthRange[i - 1 - delayCount].index == 0)
            //        {
            //            caughtCount++;
            //            //count += (elementCaught.depth * elementCaught.range);
            //            break;
            //        }
            //        //depthRange = depthRange.Select(x => { x.index = (i / (x.range - 1)) % 2 == 0 ? 0 + (i % (x.range - 1)) : ((x.range - 1) - (i % (x.range - 1))); return x; }).ToList();
            //        depthRange.ForEach(x => x.index = (i / (x.range - 1)) % 2 == 0 ? 0 + (i % (x.range - 1)) : ((x.range - 1) - (i % (x.range - 1))));
            //    }
            //    if (caughtCount == 0)
            //    {
            //        notCaught = true;
            //    }
            //    delayCount++;
            //}

            //while (!notCaught)
            //{
            //    caughtCount = 0;
            //    depthRange.ForEach(x => x.index = ((delayCount) / (x.range - 1)) % 2 == 0 ? 0 + ((delayCount) % (x.range - 1)) : ((x.range - 1) - ((delayCount) % (x.range - 1))));
            //    for (int i = 1; i <= noOfDepths + 1 ; i++)
            //    {
            //        DepthRange elementCaught = depthRange.FirstOrDefault(x => x.index == 0 && x.depth == (i - 1 - delayCount));
            //        if (elementCaught != null)
            //        //if (i - 1 - delayCount >= 0 && i - 1 - delayCount < depthRange.Count && depthRange[i - 1].index == 0)
            //        {
            //            caughtCount++;
            //            //count += (elementCaught.depth * elementCaught.range);
            //            break;
            //        }
            //        //depthRange = depthRange.Select(x => { x.index = (i / (x.range - 1)) % 2 == 0 ? 0 + (i % (x.range - 1)) : ((x.range - 1) - (i % (x.range - 1))); return x; }).ToList();
            //       depthRange.ForEach(x => x.index = (i / (x.range - 1)) % 2 == 0 ? 0 + (i % (x.range - 1)) : ((x.range - 1) - (i % (x.range - 1))));
            //    }
            //    if (caughtCount == 0)
            //    {
            //        notCaught = true;
            //    }
            //    delayCount++;
            //    if (delayCount % 10000 == 0)
            //    {
            //        Console.WriteLine("Delay {0}", delayCount);
            //    }
            //}
        }
    }
    class DepthRange
    {
        public int depth { get; set; }
        public int range { get; set; }
        public int index { get; set; }
    }
}
