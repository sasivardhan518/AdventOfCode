using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOC.INFI.NL
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"..\..\input.txt");
            var pattern = @"(\[.*\])(\(.*\))";
            string robotPattern = @"\[([+|-]?\d+),([+|-]?\d+)\]";
            string pathPattern = @"\(([+|-]?\d+),([+|-]?\d+)\)";
            MatchCollection matchCollection = Regex.Matches(input, pattern);
            string robots="", paths="";
            foreach(Match match in matchCollection)
            {
                GroupCollection groupCollection = match.Groups;
                robots = groupCollection[1].Value.ToString();
                paths = groupCollection[2].Value.ToString();
            }
            List<Input> robotsList = new List<Input>();
            List<Input> pathsList = new List<Input>();
            MatchCollection robotCollection = Regex.Matches(robots, robotPattern);
            MatchCollection pathsCollection = Regex.Matches(paths, pathPattern);
            foreach(Match robotMatch in robotCollection)
            {
                GroupCollection groupCollection = robotMatch.Groups;
                robotsList.Add(new Input(Convert.ToInt32(groupCollection[1].Value), Convert.ToInt32(groupCollection[2].Value)));
            }
            foreach (Match pathMatch in pathsCollection)
            {
                GroupCollection groupCollection = pathMatch.Groups;
                pathsList.Add(new Input(Convert.ToInt32(groupCollection[1].Value), Convert.ToInt32(groupCollection[2].Value)));
            }
            int robotsCount = robotsList.Count;
            int count = 0;
            for(int i=0; i<pathsList.Count; i++)
            {
                if(i%robotsCount == 0)
                {
                    if(robotsList.Distinct().Count() == 1)
                    {
                        System.IO.File.AppendAllText(@"..\..\output.txt", "\n" + robotsList[0].x.ToString() + "," + robotsList[0].y.ToString());
                        count++;
                    }
                    
                    
                }
                robotsList[i % robotsCount].x += pathsList[i].x;
                robotsList[i % robotsCount].y += pathsList[i].y;
            }
        }
    }

    class Input
    {
        public int x { get; set; }
        public int y { get; set; }
        public Input(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public override bool Equals(object obj)
        {
            if (obj is Input)
            {
                var tmpobj = obj as Input;
                return this.x == tmpobj.x && this.y == tmpobj.y;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return this.x + this.y;
        }
    }
}
