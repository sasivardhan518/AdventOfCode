using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Recursive_Circus_Day_7_Advent_Of_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> lines = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Recursive Circus.txt").ToList();
            List<string> childs = new List<string>();
            Dictionary<string, Dictionary<int, List<string>>> givenList = new Dictionary<string, Dictionary<int, List<string>>>();
            string pattern = @"\(([^)]+)\)";
            foreach (var line in lines)
            {
               var splittedLines = Regex.Split(line, pattern);
                var temp = new Dictionary<int, List<string>>();
                temp.Add(Convert.ToInt32(splittedLines[1]), splittedLines[2].Replace("->", "").Replace(" ", "").Split(',').ToList().Where(x => x != "").ToList());
                childs.AddRange(splittedLines[2].Replace("->", "").Replace(" ","").Split(',').ToList().Where(x=> x!= ""));
                givenList.Add(splittedLines[0], temp);
            }
            List<string> bottomPrograms = new List<string>();
            List<int> sums = new List<int>();
            foreach(var key in givenList.Keys)
            {
               if(childs.IndexOf(key.Trim()) == -1)
                {
                    bottomPrograms.Add(key.Trim());
                }
            }
            List<int> childsSumList = new List<int>();
            int difference = 0;
            var bottomProgram = givenList.FirstOrDefault(x => x.Key.Trim() == bottomPrograms[0]);
            while (childsSumList.Distinct().Count() != 1 || childsSumList.Count == 0)
            {
                var distinctValueIndex = 0;
                foreach(var childSum in childsSumList)
                {
                    if(childsSumList.Where(x=> x == childSum).ToList().Count == 1){
                        distinctValueIndex = childsSumList.IndexOf(childSum);
                    }
                } 
                bottomProgram = childsSumList.Count == 0 ? givenList.FirstOrDefault(x => x.Key.Trim() == bottomPrograms[0]) : givenList.FirstOrDefault(x => x.Key.Trim() == bottomProgram.Value.First().Value[distinctValueIndex].Trim());
                childsSumList = new List<int>();
                foreach (var child in bottomProgram.Value.First().Value)
                {
                    var childProgram = givenList.FirstOrDefault(x => x.Key.Trim() == child);
                    childsSumList.Add(findWeights(childProgram.Key, givenList));
                }
                difference = childsSumList.Distinct().Count() > 1 ? Math.Abs(childsSumList.Distinct().ToList().First() - childsSumList.Distinct().ToList()[1]) : difference;
            }
            
            Console.WriteLine("Level 1 = "+bottomPrograms[0]);
            Console.WriteLine("Level 2 = "+ (bottomProgram.Value.First().Key - difference));
            Console.Read();
        }

        private static int findWeights(string parent, Dictionary<string, Dictionary<int, List<string>>> givenList, int result = 0)
        {
            var sum = 0;
            var program = givenList.FirstOrDefault(x => x.Key.Trim() == parent.Trim());
            sum = program.Value.First().Key;
            foreach (var child in program.Value.First().Value)
            {
                sum += findWeights(child, givenList);
            }
            return sum;
        }
    }
}
