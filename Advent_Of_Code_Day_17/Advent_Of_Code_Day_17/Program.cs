using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_Day_17
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            int input = 377;
            list.Add(0);
            int currentPosition = 0;
            int valueAfterZero = 0;
            int listCount = 1;
            for(int i = 1; i < 50000000; i++)
            {
                var index = -1;
                for (var j = 1; j <= 377; j++)
                {
                    currentPosition = currentPosition + 1 < listCount ? currentPosition + 1 : 0;
                    index = currentPosition;
                }
                currentPosition = index+1;
                valueAfterZero = currentPosition == 1 ? i : valueAfterZero;
                listCount++;
                //list.Insert(currentPosition, i);
            }
            
            

        }
    }
}
