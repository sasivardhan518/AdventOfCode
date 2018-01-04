using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralExample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Point> points = new List<Point>();
            points.Add(new Point(0, 0, 1));
            int count = 2;
            while(getNextPoint(points,count, true).value<= 368078)
            {
                count++;
            }
            var point = getNextPoint(points, count, false);
            Console.WriteLine(point.x + "," + point.y);
            var paths = Math.Abs(0 - point.x) + Math.Abs(point.y - 0);
            Console.WriteLine("The minimum paths are :" + paths +" and the greatest value is:"+ point.value);
            Console.Read();
        }

        private static Point getNextPoint(List<Point> points, int count, bool addToPoints)
        {
            var point = createPoints(points, count);
            if (addToPoints)
                points.Add(point);
            return point;
        }

        private static Point createPoints(List<Point> points,int num)
        {
            var point = new Point(0, 0, 0);
            int curentleftCount = 0;
            int curentupCount = 0;
            int curentdownCount = 0;
            int curentrightCount = 0;
            int initialCount = 1;
            int leftCount = 1;
            int upCount = 1;
            int downCount = 2;
            int rightCount = 2;
            string nextOperation = "left";
            while (initialCount < num)
            {
                if (curentleftCount < leftCount && nextOperation == "left")
                {
                    point.left();
                    curentleftCount++;
                    if (curentleftCount == leftCount)
                    {
                        curentleftCount = 0;
                        leftCount += 2;
                        nextOperation = "up";
                    }
                }
                else if (curentupCount < upCount && nextOperation == "up")
                {
                    point.up();
                    curentupCount++;
                    if (curentupCount == upCount)
                    {
                        curentupCount = 0;
                        upCount += 2;
                        nextOperation = "right";
                    }
                }
                else if (curentrightCount < rightCount && nextOperation == "right")
                {
                    point.right();
                    curentrightCount++;
                    if (curentrightCount == rightCount)
                    {
                        curentrightCount = 0;
                        rightCount += 2;
                        nextOperation = "down";
                    }
                }

                else if (curentdownCount < downCount && nextOperation == "down")
                {
                    point.down();
                    curentdownCount++;
                    if (curentdownCount == downCount)
                    {
                        curentdownCount = 0;
                        downCount += 2;
                        nextOperation = "left";
                    }
                }
                initialCount++;
            }
            int value = getValueOfPoint(points, point);
            return point;
        }

        private static int getValueOfPoint(List<Point> points, Point point)
        {
            var value = 0;
            var p1 = new Point(point.x + 1, point.y + 0, 0);
            var p2 = new Point(point.x + 0, point.y + 1, 0);
            var p3 = new Point(point.x + 1, point.y + 1, 0);
            var p4 = new Point(point.x - 1, point.y + 0, 0);
            var p5 = new Point(point.x + 0, point.y - 1, 0);
            var p6 = new Point(point.x - 1, point.y - 1, 0);
            var p7 =new Point(point.x - 1, point.y + 1, 0);
            var p8 = new Point(point.x + 1, point.y - 1, 0);
            var point1 = points.FirstOrDefault(pnt => pnt.x == p1.x && pnt.y == p1.y);
            var point2 = points.FirstOrDefault(pnt => pnt.x == p2.x && pnt.y == p2.y);
            var point3 = points.FirstOrDefault(pnt => pnt.x == p3.x && pnt.y == p3.y);
            var point4 = points.FirstOrDefault(pnt => pnt.x == p4.x && pnt.y == p4.y);
            var point5 = points.FirstOrDefault(pnt => pnt.x == p5.x && pnt.y == p5.y);
            var point6 = points.FirstOrDefault(pnt => pnt.x == p6.x && pnt.y == p6.y);
            var point7 = points.FirstOrDefault(pnt => pnt.x == p7.x && pnt.y == p7.y);
            var point8 = points.FirstOrDefault(pnt => pnt.x == p8.x && pnt.y == p8.y);
            if (point1 != null)
            {
                value += point1.value; 
            }
            if (point2 != null)
            {
                value += point2.value;
            }
            if (point3 != null)
            {
                value += point3.value;
            }
            if (point4 != null)
            {
                value += point4.value;
            }
            if (point5 != null)
            {
                value += point5.value;
            }
            if (point6 != null)
            {
                value += point6.value;
            }
            if (point7 != null)
            {
                value += point7.value;
            }
            if (point8 != null)
            {
                value += point8.value;
            }
            point.value = value;
            return value;
        }

        private static int getSides(int x)
        {
            int sides = 0;
            for (int i = 1; i < x / 2; i += 2)
            {
                if (x <= i * i)
                {
                    sides = i;
                    break;
                }
            }
            return sides;
        }
    }
    class Point
    {
        public int x { get; set; }

        public int y { get; set; }
        public int value { get; set; }
        public Point(int x1, int y1, int value)
        {
            this.x = x1;
            this.y = y1;
            this.value = value;
        }
        public void left()
        {
            this.x += 1;
        }
        public void up()
        {
            this.y += 1;
        }
        public void down()
        {
            this.y -= 1;
        }
        public void right()
        {
            this.x -= 1;
        }
    }
}
