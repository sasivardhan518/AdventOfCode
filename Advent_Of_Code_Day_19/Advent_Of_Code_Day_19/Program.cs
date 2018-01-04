using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_Day_19
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day19.txt").ToList();
            List<List<char>> arrayInput = new List<List<char>>();
            foreach(var inputString in input){
                arrayInput.Add(inputString.ToList());
            }
            string characterString = Regex.Replace(System.IO.File.ReadAllText(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day19.txt"), @"[\+]*[\-]*[\|]*[\s]*", "");
            int characterCount = characterString.Count();
            var count = 0;
            var lastCount = arrayInput.Count * arrayInput[0].Count();
            var arrayIndex = 0;
            var insideArrayIndex = 0;
            insideArrayIndex = arrayInput[0].IndexOf('|');
            var direction = "down";
            var totalCount = 0;
            var characterList = new List<Char>();
            //for (var i = 0; i < lastCount; i++)
            while(characterList.Count != characterCount)
            {
                totalCount++;
                if (arrayInput[arrayIndex][insideArrayIndex] != '|' && arrayInput[arrayIndex][insideArrayIndex] != '-' && arrayInput[arrayIndex][insideArrayIndex] != '+' && arrayInput[arrayIndex][insideArrayIndex] != ' ')
                {
                    characterList.Add(arrayInput[arrayIndex][insideArrayIndex]);
                    if(direction == "left")
                    {
                        insideArrayIndex = insideArrayIndex -1 == -1 ? 0 : insideArrayIndex - 1;
                    }
                    else if (direction == "right")
                    {
                        insideArrayIndex = insideArrayIndex +1 >= arrayInput[0].Count ? insideArrayIndex : insideArrayIndex + 1;
                    }
                    else if (direction == "up")
                    {
                        arrayIndex = arrayIndex - 1 == -1 ? 0 : arrayIndex - 1;
                    }
                    else if (direction == "down")
                    {
                        arrayIndex = arrayIndex + 1 >= arrayInput.Count ? arrayIndex : arrayIndex + 1;
                    }
                }
                else if(arrayInput[arrayIndex][insideArrayIndex] == '+')
                {
                    if(direction == "up" || direction == "down")
                    {
                       if(arrayInput[arrayIndex][insideArrayIndex+1] != ' ')
                        {
                            insideArrayIndex = insideArrayIndex + 1 >= arrayInput[0].Count ? insideArrayIndex : insideArrayIndex + 1;
                            direction = "right";
                        }
                       else
                        {
                            insideArrayIndex = insideArrayIndex - 1 == -1 ? 0 : insideArrayIndex - 1;
                            direction = "left";
                        }
                       
                    }
                    else if(direction =="left" || direction == "right")
                    {
                        if (arrayInput[arrayIndex-1][insideArrayIndex] != ' ')
                        {
                            arrayIndex = arrayIndex - 1 == -1 ? 0 : arrayIndex - 1;
                            direction = "up";
                        }
                        else
                        {
                            arrayIndex = arrayIndex + 1 >= arrayInput.Count ? arrayIndex : arrayIndex + 1;
                            direction = "down";
                        }
                    }
                }
                else if(direction == "up")
                {
                    arrayIndex = arrayIndex - 1 == -1 ? 0 : arrayIndex - 1;
                }
                else if(direction == "down")
                {
                    arrayIndex = arrayIndex + 1 >= arrayInput.Count ? arrayIndex : arrayIndex + 1;
                }
                else if(direction == "left")
                {
                    insideArrayIndex = insideArrayIndex - 1 == -1 ? 0 : insideArrayIndex - 1;
                }
                else if (direction == "right")
                {
                    insideArrayIndex = insideArrayIndex + 1 >= arrayInput[0].Count ? insideArrayIndex : insideArrayIndex + 1;

                }
            }
            
        }
    }
}
