using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InverseCaptcha_AdevntOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Maze of Twisty Trampolines.txt").ToList().Select(x=>Convert.ToInt32(x)).ToList();
            var total = 0;
            int position = 0;
            int count = input.Count;
            //for(int i=0; i<input.Count()-1; i++)
            //{
            //    for(int j=i+1; j<i+2; j++)
            //    {
            //        if (input[i] == input[j])
            //        {
            //            total += Convert.ToInt32(input[i]);
            //        }
            //    }
            //}
            //if(input[input.Count()-1] == input[0])
            //{
            //    total += Convert.ToInt32(input[0]);
            //}
            //int additor = input.Count() / 2;
            //int count = input.Count();
            //for (int i = 0; i < input.Count() - 1; i++)
            //{
            //    int index = i + additor < count ? i + additor : i + additor - count;
            //    if(input[i] == input[index])
            //    {
            //        total += input[i];
            //    }
            //}
            while(position <= count-1 && position >= 0)
            {
                var tempInput = input[position];
                
               input[position] = tempInput >= 3 ? tempInput-1 : tempInput + 1;
                position += tempInput;
                total++;
            }
        }
    }
}
