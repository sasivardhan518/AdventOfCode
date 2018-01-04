using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace DAY8_AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day 8.txt");
            string pattern = @"/^<|^>|^>=|^<=|^==|^!=/";
            List<Operation> operations = new List<Operation>();
            Dictionary<string, int> variables = new Dictionary<string, int>();
            List<int> highetsNumbers = new List<int>();
            foreach(var line in lines)
            {
                var splittedLines = line.Split(new[] { "if" }, StringSplitOptions.None);
                if (splittedLines[0].Contains("inc"))
                {
                    var operation = splittedLines[0].Split(new[] { "inc" }, StringSplitOptions.None);
                   if(!variables.Keys.Contains(operation[0].Trim()))
                    variables.Add(operation[0].Trim(), 0);
                }
                else
                {
                    var operation = splittedLines[0].Split(new[] { "dec" }, StringSplitOptions.None);
                    if (!variables.Keys.Contains(operation[0].Trim()))
                        variables.Add(operation[0].Trim(), 0);
                }
                if (!variables.Keys.Contains(splittedLines[1].Trim().Split(' ').ToList().First().Trim()))
                    variables.Add(splittedLines[1].Trim().Split(' ').ToList().First().Trim(), 0);
                operations.Add(new Operation() { pattern = splittedLines[0].Replace("inc", "+").Replace("dec", "-").ToString() , condition = splittedLines[1]});
            }
            foreach(var oper in operations)
            {
                var conditionSplitted = oper.condition.Trim().Split(' ').ToList();
                var temVal = variables.Where(x => x.Key.Trim() == conditionSplitted[0].Trim()).First().Value;
                conditionSplitted[0] = temVal.ToString().Trim();
                if (checkConditionValid(conditionSplitted))
                {
                    Evaluate(oper.pattern, variables, highetsNumbers);
                }
            }
        }

        private static void Evaluate(string pattern, Dictionary<string, int> variables, List<int> highestNumbers)
        {
            var conditionSplitted = pattern.Trim().Split(' ').ToList();
            var temVal = variables.Where(x => x.Key.Trim() == conditionSplitted[0].Trim()).First();
            conditionSplitted[0] = temVal.Value.ToString().Trim();
            var num1 = Convert.ToInt32(conditionSplitted[0].Trim());
            var num2 = Convert.ToInt32(conditionSplitted[2].Trim());
            var operatorKey = conditionSplitted[1].Trim();
            var keys = variables.Keys;
            switch (operatorKey)
            {
                case "+": foreach(var key in keys)
                    {
                        if (key == pattern.Trim().Split(' ').ToList().First())
                        {
                            variables[key] = num1 + num2;
                            highestNumbers.Add(num1 + num2);
                            break;
                        }
                    }
                    break;
                case "-":
                    foreach (var key in keys)
                    {
                        if (key == pattern.Trim().Split(' ').ToList().First())
                        {
                            variables[key] = num1 - num2;
                            highestNumbers.Add(num1 - num2);
                            break;
                        }
                    }
                    break;
            }
        }

        private static bool checkConditionValid(List<string> conditionSplitted)
        {
            var num1 = Convert.ToInt32(conditionSplitted[0].Trim());
            var num2 = Convert.ToInt32(conditionSplitted[2].Trim());
            var operatorKey = conditionSplitted[1].Trim();
            var result = false;
            switch (operatorKey)
            {
                case ">":
                    if (num1 > num2)
                    {
                        result = true;
                            }
                    else result = false;
                    break;
                case "<":
                    if (num1 < num2)
                    {
                        result = true;
                            }
                    else result = false;
                    break;
                case ">=":
                    if (num1 >= num2)
                    {
                        result = true;
                            }
                    else result = false;
                    break;
                case "<=":
                    if (num1 <= num2)
                    {
                        result = true;
                            }
                    else result = false;
                    break;
                case "==":
                    if (num1 == num2)
                    {
                        result = true;
                            }
                    else result = false;
                    break;
                case "!=":
                    if (num1 != num2)
                    {
                        result = true;
                            }
                    else result = false;
                    break;

            }
            return result;
        }
    }
    class Operation
    {
        public string pattern { get; set; }
        public string condition { get; set; }
    }
}
