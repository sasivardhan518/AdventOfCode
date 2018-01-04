using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_10_Advent_Of_Code
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();
            var currentPosition = 0;
            var skipSize = 0;
            var stringGiven = "147,37,249,1,31,2,226,0,161,71,254,243,183,255,30,70";
            var lengthList = stringGiven.Split(',').ToList().Select(x => Convert.ToInt32(x)).ToList();
            for(var i = 0; i < 256; i++)
            {
                list.Add(i);
            }
            foreach(var length in lengthList)
            {
                ReverseMyList(list, currentPosition, length);
                currentPosition = currentPosition+length + skipSize >= list.Count ? currentPosition + length + skipSize  - list.Count : length + skipSize;
                skipSize++;
            }
        }

        private static void ReverseMyList(List<int> list, int currentPosition, int length)
        {
            var listLength = list.Count;
            var tempList = new List<int>();
            var count = length;
            for (var i =currentPosition; count!=0; i++)
            {
                
                if (i < listLength)
                {
                    tempList.Add(list[i]);
                }
                else
                {
                    tempList.Add(list[i % listLength]);
                }
                count--;
            }
            tempList.Reverse();
            count = length;
            for (int i = currentPosition, j=0; count!=0; i++,j++)
            {
                if (i < listLength)
                {
                    list[i] = tempList[j];
                }
                else
                {
                    list[i % listLength] = tempList[j];
                }
                count--;
            }
        }
    }
}
