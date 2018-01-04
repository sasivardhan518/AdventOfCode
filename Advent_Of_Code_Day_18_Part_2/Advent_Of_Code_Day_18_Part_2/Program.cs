using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_Of_Code_Day_18_Part_2
{
    class Program
    {
        private static List<double> program1Queue = new List<double>();
        private static List<double> program0Queue = new List<double>();
        private static bool program1DeadLock = false;
        private static bool program2DeadLock = false;
        private static int noOfSendsByP1 = 0;
        private static int noOfSendsByP2 = 0;
        static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day18.txt");
            var variables = input.Select(x => x.Split(' ')[1]).Distinct().ToList();
            Dictionary<string, double> varaiblesWithValues1 = new Dictionary<string, double>();
            Dictionary<string, double> varaiblesWithValues2 = new Dictionary<string, double>();
            
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
                    varaiblesWithValues1.Add(variable, 0);
                    varaiblesWithValues2.Add(variable, variable == "p" ? 1 : 0);
                }
            }
            //for(var i= 0 ; i < input.Count(); i++)
            int i = 0;
            int j = 0;
            bool isDeadLocked = false;
            while(!program1DeadLock || !program2DeadLock)
            {
                var instruction1 = input[i];
                var instructions1 = instruction1.Split(' ').ToList();
                var instruction2 = input[j];
                var instructions2 = instruction2.Split(' ').ToList();
                i = execute(instructions1, varaiblesWithValues1, i, 0);
                j = execute(instructions2, varaiblesWithValues2, j, 1);
            }
        }

        private static int execute(List<string> instructions,Dictionary<string, double> varaiblesWithValues, int i, int Pid)
        {
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
                    try
                    {
                        Convert.ToDouble(instructions[1]);
                        if(Pid == 0)
                        {
                            program0Queue.Add(Convert.ToDouble(instructions[1]));
                            noOfSendsByP1++;
                        }
                        else
                        {
                            program1Queue.Add(Convert.ToDouble(instructions[1]));
                            noOfSendsByP2++;
                        }
                    }
                    catch
                    {
                        if (Pid == 0)
                        {
                            program0Queue.Add(varaiblesWithValues[instructions[1]]);
                            noOfSendsByP1++;
                        }
                        else
                        {
                            program1Queue.Add(varaiblesWithValues[instructions[1]]);
                            noOfSendsByP2++;
                        }
                    }
                    
                    i++;
                    break;
                case "rcv":
                    if (Pid == 0 && program1Queue.Count > 0) 
                    {
                        varaiblesWithValues[instructions[1]] = program1Queue.First();
                        program1Queue.RemoveAt(0);
                        program2DeadLock = program1Queue.Count == 0;
                        i++;
                    }
                    else if(Pid == 1 && program0Queue.Count > 0)
                    {
                        varaiblesWithValues[instructions[1]] = program0Queue.First();
                        program0Queue.RemoveAt(0);
                        program1DeadLock = program0Queue.Count == 0;
                        i++;
                    }
                    else
                    {
                        if (Pid == 0)
                        {
                            program2DeadLock = true;
                        }
                        else
                        {
                            program1DeadLock = true;
                        }
                    }
                    break;
                case "jgz":
                    try
                    {
                        if (Convert.ToDouble(instructions[1]) > 0)
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
            return i;
        }
    }
}

