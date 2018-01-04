using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_Of_Code_Day11
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = System.IO.File.ReadAllText(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day11.txt");
            while (
                Regex.IsMatch(input, @"\bse,sw\b") ||
                Regex.IsMatch(input, @"\bne,nw\b") ||
                Regex.IsMatch(input, @"\bsw,se\b") ||
                Regex.IsMatch(input, @"\bnw,ne\b") ||
                Regex.IsMatch(input, @"\bse,nw\b") ||
                Regex.IsMatch(input, @"\bnw,se\b") ||
                Regex.IsMatch(input, @"\bne,sw\b") ||
                Regex.IsMatch(input, @"\bsw,ne\b") ||
                Regex.IsMatch(input, @"se,([s][,])*sw") ||
                Regex.IsMatch(input, @"sw,([s][,])*se") ||
                Regex.IsMatch(input, @"ne,([s][,])*nw") ||
                Regex.IsMatch(input, @"nw,([s][,])*ne") ||
                Regex.IsMatch(input, @"@,")
                )
            {
                input = Regex.Replace(input, @"\bse,sw\b", "s");
                input = Regex.Replace(input, @"\bne,nw\b", "n");
                input = Regex.Replace(input, @"\bsw,se\b", "s");
                input = Regex.Replace(input, @"\bnw,ne\b", "n");
                input = Regex.Replace(input, @"\bse,nw\b", "@");
                input = Regex.Replace(input, @"\bnw,se\b", "@");
                input = Regex.Replace(input, @"\bne,sw\b", "@");
                input = Regex.Replace(input, @"\bsw,ne\b", "@");
                input = Regex.Replace(input, @"se,(?<rep>([s][,])*)sw", "${rep}s");
                input = Regex.Replace(input, @"sw,(?<rep>([s][,])*)se", "${rep}s");
                input = Regex.Replace(input, @"ne,(?<rep>([s][,])*)nw", "${rep}s");
                input = Regex.Replace(input, @"nw,(?<rep>([s][,])*)ne", "${rep}s");
                input = Regex.Replace(input, @"@,", "");
            }
            var startPoint = new Point(0, 0);
            List<string> directions = input.Split(',').ToList();
            Point endPoint = new Point(startPoint.x, startPoint.y);
            PointWithDistance highestPoint = new PointWithDistance(0.0,0.0);
            List<PointWithDistance> pointsWithDistance = new List<PointWithDistance>();
            foreach (string direction in directions)
            {
                switch (direction)
                {
                    case "ne"   :   endPoint.x += 0.5;      endPoint.y += 0.5;   break;
                    case "se"   :   endPoint.x += 0.5;      endPoint.y -= 0.5;   break;
                    case "sw"   :   endPoint.x -= 0.5;      endPoint.y -= 0.5;   break;
                    case "nw"   :   endPoint.x -= 0.5;      endPoint.y += 0.5;   break;
                    case "n"    :   endPoint.y += 1.0;                           break;
                    case "s"    :   endPoint.y -= 1.0;                           break;
                }
                pointsWithDistance.Add(new PointWithDistance(endPoint.x, endPoint.y));
                PointWithDistance tempPoint = new PointWithDistance(endPoint.x, endPoint.y);
                if(tempPoint.distance> highestPoint.distance)
                {
                    highestPoint.distance = tempPoint.distance;
                    highestPoint.x = tempPoint.x;
                    highestPoint.y = tempPoint.y;
                    highestPoint.index = directions.IndexOf(direction)+1;
                }
            }
            //while(
            //    Regex.IsMatch(input, @"\bse,sw\b") ||
            //    Regex.IsMatch(input, @"\bne,nw\b") ||
            //    Regex.IsMatch(input, @"\bsw,se\b") ||
            //    Regex.IsMatch(input, @"\bnw,ne\b") ||
            //    Regex.IsMatch(input, @"\bse,nw\b") ||
            //    Regex.IsMatch(input, @"\bnw,se\b") ||
            //    Regex.IsMatch(input, @"\bne,sw\b") ||
            //    Regex.IsMatch(input, @"\bsw,ne\b") ||
            //    Regex.IsMatch(input, @"se,([s][,])*sw") ||
            //    Regex.IsMatch(input, @"sw,([s][,])*se") ||
            //    Regex.IsMatch(input,@"ne,([s][,])*nw")||
            //    Regex.IsMatch(input,@"nw,([s][,])*ne")||
            //    Regex.IsMatch(input, @"@,")
            //    )
            //{
            //    input = Regex.Replace(input, @"\bse,sw\b", "s");
            //    input = Regex.Replace(input, @"\bne,nw\b", "n");
            //    input = Regex.Replace(input, @"\bsw,se\b", "s");
            //    input = Regex.Replace(input, @"\bnw,ne\b", "n");
            //    input = Regex.Replace(input, @"\bse,nw\b", "@");
            //    input = Regex.Replace(input, @"\bnw,se\b", "@");
            //    input = Regex.Replace(input, @"\bne,sw\b", "@");
            //    input = Regex.Replace(input, @"\bsw,ne\b", "@");
            //    input = Regex.Replace(input, @"se,(?<rep>([s][,])*)sw", "${rep}s");
            //    input = Regex.Replace(input, @"sw,(?<rep>([s][,])*)se", "${rep}s");
            //    input = Regex.Replace(input, @"ne,(?<rep>([s][,])*)nw", "${rep}s");
            //    input = Regex.Replace(input, @"nw,(?<rep>([s][,])*)ne", "${rep}s");
            //    input = Regex.Replace(input, @"@,", "");
            //}

        }
    }
    class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class PointWithDistance
    {
        public double x { get; set; }
        public double y { get; set; }
        public double distance { get; set; }
        public int index { get; set; }
        public PointWithDistance(double x,double y)
        {
            this.x = x;
            this.y = y;
            this.distance = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            this.index = -1;
        }
    }

}
