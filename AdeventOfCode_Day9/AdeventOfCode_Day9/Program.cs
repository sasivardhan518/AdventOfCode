using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdeventOfCode_Day9
{
    class Program
    {
        static void Main(string[] args)
        {
            var givenString = System.IO.File.ReadAllText(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day 9.txt");
            Regex regEx = new Regex(@"!.");
            var result1 = Regex.Replace(givenString, @"!.", "");
            string garbage = "";
            var result2 = result1;
            string result = "";
            var found = false;
            foreach (var character in result2)
            {
                if (found == false && character != '<')
                    result += character;
                if(found == true)
                {
                    garbage += character;
                }
                if(character == '<' && found == false)
                {
                    found = true;
                }
                if(character == '>')
                {
                    found = false;
                    result+= '@';
                }
            }
            result = Regex.Replace(result, @"{@}", "{}");
            result = Regex.Replace(result, @"@,@", "");
            result = Regex.Replace(result, @",@", "");
            result = Regex.Replace(result, @"@,", "");
            var sum = 0;
            var incrementor = 1;
            foreach(var character in result)
            {
                if(character == '{')
                {
                    sum += incrementor;
                    incrementor++;
                }
                else if(character =='}')
                {
                    incrementor--;
                }
            }
            var groups = new List<string>();
            //var result3 = Regex.Replace(result2, @"<.*>", "");
        }
    }
}
