using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_Day_18
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day18.txt");
            var variables = input.Select(x => x.Split(' ')[1]).Distinct().ToList();
            Dictionary<string, double> varaiblesWithValues = new Dictionary<string, double>();
            var recentlyPlayedSound = 0.0;
            var isRecoveredFirstTime = false;
            var recoveryCount = 0;
            foreach(var variable in variables)
            {
                try
                {
                    Convert.ToDouble(variable);
                }
                catch
                {
                    varaiblesWithValues.Add(variable, 0);
                }
            }
            //for(var i= 0 ; i < input.Count(); i++)
            int i = 0;
            while(!isRecoveredFirstTime)
            {
                var instruction = input[i];
                var instructions = instruction.Split(' ').ToList();
                switch (instructions[0])
                {
                    case "set":
                        try
                        {
                            Convert.ToDouble(instructions[2]);
                            varaiblesWithValues[instructions[1]] = Convert.ToDouble(instructions[2]);
                        }
                        catch
                        {
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[2]];
                        }
                        i++;
                        break;
                    case "add":
                        try
                        {
                            Convert.ToDouble(instructions[2]);
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[1]] + Convert.ToDouble(instructions[2]);
                        }
                        catch
                        {
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[1]] + varaiblesWithValues[instructions[2]];
                        }
                        i++;
                        break;
                    case "mul":
                        try
                        {
                            Convert.ToDouble(instructions[2]);
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[1]] * Convert.ToDouble(instructions[2]);
                        }
                        catch
                        {
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[1]] * varaiblesWithValues[instructions[2]];
                        }
                        i++;
                        break;
                    case "mod":
                        try
                        {
                            Convert.ToDouble(instructions[2]);
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[1]] % Convert.ToDouble(instructions[2]);
                        }
                        catch
                        {
                            varaiblesWithValues[instructions[1]] = varaiblesWithValues[instructions[1]] % varaiblesWithValues[instructions[2]];
                        }
                        i++;
                        break;
                    case "snd":
                        recentlyPlayedSound = !isRecoveredFirstTime ? varaiblesWithValues[instructions[1]] : recentlyPlayedSound;
                        i++;
                        break;
                    case "rcv":
                        if(recoveryCount == 0)
                        {
                            if (varaiblesWithValues[instructions[1]] != 0)
                            {
                                recoveryCount++;
                                isRecoveredFirstTime = true;
                            }
                        }
                        i++;
                        break;
                    case "jgz":
                        try
                        {
                            if(Convert.ToDouble(instructions[1]) > 0)
                            {
                                try
                                {
                                    Convert.ToDouble(instructions[2]);
                                    i = Convert.ToInt32(Convert.ToDouble(i) + Convert.ToDouble(instructions[2]));
                                }
                                catch
                                {
                                    i = Convert.ToInt32(Convert.ToDouble(i) + varaiblesWithValues[instructions[2]]);
                                }
                            }
                            else
                            {
                                i++;
                            }
                            
                        }
                        catch
                        {
                            if (varaiblesWithValues[instructions[1]] > 0)
                            {
                                try
                                {
                                    Convert.ToDouble(instructions[2]);
                                    i = Convert.ToInt32(Convert.ToDouble(i) + Convert.ToDouble(instructions[2]));
                                }
                                catch
                                {
                                    i = Convert.ToInt32(Convert.ToDouble(i) + varaiblesWithValues[instructions[2]]);
                                }
                            }
                            else
                            {
                                i++;
                            }
                        }
                        break;
                }
            }
        }
    }
}
