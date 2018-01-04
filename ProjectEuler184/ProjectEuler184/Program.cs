using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler184
{
    
    class Program
    {
            private static double counter = 0;
            static void Main(string[] args)
            {
                int radius;
                List<Point> points = new List<Point>();
                Console.WriteLine("Enter the radius:");
                radius = Convert.ToInt32(Console.ReadLine());
                for (var i = -(radius - 1); i < radius; i++)
                {
                    for (var j = -(radius - 1); j < radius; j++)
                    {
                        if(i*i + j*j < radius*radius)
                        points.Add(new Point() { x = i, y = j });
                    }
                }
                GetCombinations(points);
                Console.Read();
            }

            private static void GetOriginInsidePoints(Combinations combin, List<Point> points)
            {
                double Area = Math.Abs(((points[combin.v1].x * (points[combin.v2].y - points[combin.v3].y)) + (points[combin.v2].x * (points[combin.v3].y - points[combin.v1].y)) + (points[combin.v3].x * (points[combin.v1].y - points[combin.v2].y))) / 2.0);
                double Area1 = Math.Abs(((0 * (points[combin.v2].y - points[combin.v3].y)) + (points[combin.v2].x * (points[combin.v3].y - 0)) + (points[combin.v3].x * (0 - points[combin.v2].y))) / 2.0);
                double Area2 = Math.Abs(((points[combin.v1].x * (0 - points[combin.v3].y)) + (0 * (points[combin.v3].y - points[combin.v1].y)) + (points[combin.v3].x * (points[combin.v1].y - 0))) / 2.0);
                double Area3 = Math.Abs(((points[combin.v1].x * (points[combin.v2].y - 0)) + (points[combin.v2].x * (0 - points[combin.v1].y)) + (0 * (points[combin.v1].y - points[combin.v2].y))) / 2.0);
            
                if (Area == Area1 + Area2 + Area3 && Area != 0 && Area1 != 0 && Area2 != 0 && Area3 != 0)
                {
                    //combin;
                    counter++;
                    Console.WriteLine("(" + points[combin.v1].x + "," + points[combin.v1].y + ") ->(" + points[combin.v2].x + "," + points[combin.v2].y + ") ->(" + points[combin.v3].x + "," + points[combin.v3].y + ")");
            }
            }
            private static void GetCombinations(List<Point> points)
            {
                var count = points.Count;
                for (var i = 0; i <= count - 1; i++)
                {
                    for (var j = i + 1; j <= count - 1; j++)
                    {
                        for (var k = j + 1; k <= count - 1; k++)
                        {
                            var combination = new Combinations() { v1 = i, v2 = j, v3 = k };
                            GetOriginInsidePoints(combination, points);
                        }
                    }
                }
                Console.WriteLine(counter);
            }
        }
        class Point
        {
            public int x { get; set; }
            public int y { get; set; }
        }
        class Combinations
        {
        public int v1 { get; set; }
            public int v2 { get; set; }
            public int v3 { get; set; }
        }
    }
