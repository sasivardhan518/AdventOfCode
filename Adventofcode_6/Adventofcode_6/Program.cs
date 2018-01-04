using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventofcode_6
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = System.IO.File.ReadAllText(@"C:\Users\shashi.dadi\Desktop\Advent of code\problem6.txt").ToString().Split('\t').ToList().Select(x=> Convert.ToInt32(x)).ToList();
            List<string> patterns = new List<string>();
            int length = numbers.Count;
            int count = 0;
            while(patterns.FirstOrDefault(pattern => pattern == string.Join("-",numbers))== null)
            {
                patterns.Add(string.Join("-", numbers));
                var max = numbers.Max();
                var index = numbers.IndexOf(max);
                var increment = (max / (length-1));
                //if(increment > 0)
                //{
                //    numbers = numbers.Select(x => x + increment).ToList();
                //    numbers[index] = numbers[index] - (increment * (numbers.Count - 1)) - increment;
                //}
                //else
                //{
                //    for(int i = 0; i < numbers[index] && numbers[index] != 0; i++)
                //    {
                //        //if(i!=index && numbers[index] != 0)
                //        for(int j=0; j < numbers.Count && numbers[index]!=0; j++)
                //        {
                //            numbers[j] += 1;
                //            numbers[index] -= 1;
                //        }
                //    }
                //}
                //count++;
                var tempCount = max;
                numbers[index] = 0;
                var initialIndex = index + 1;
                while(tempCount != 0)
                {
                    for(int i= initialIndex; i <numbers.Count && tempCount!=0; i++)
                    {
                        numbers[i] += 1;
                        tempCount -= 1;
                    }
                    initialIndex = 0;
                }
                count++;
            }
            Console.WriteLine(count);
            Console.WriteLine(count - patterns.IndexOf(string.Join("-", numbers)));
            Console.Read();
        }
    }
}
