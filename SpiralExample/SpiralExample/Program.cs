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
            /* words input problem*/
            //var count = 0;
            //List<string> lines = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\passphrase.txt").ToList();
            //foreach(var line in lines)
            //{

            //    var strings = line.Split(' ').ToList();
            //    if(strings.Count == strings.Distinct().Count())
            //    {
            //        bool cond = true;
            //        for(int i = 0; i < strings.Count-1; i++)
            //        {
            //            for(int j = i+1; j<strings.Count; j++)
            //            {
            //                List<char> string1 = strings[i].ToList();
            //                string1.Sort();
            //                List<char> string2 = strings[j].ToList();
            //                string2.Sort();
            //                if (String.Join("",string1) == String.Join("", string2))
            //                {
            //                    cond = false;
            //                }
            //            }
            //        }
            //        if(cond)
            //        count++;
            //    }
            //}
            /* words input problem*/
            /*spiral problem level 1*/
            //int x = Convert.ToInt32(Console.ReadLine());
            //var sides = getSides(x);
            //int element = Convert.ToInt32(Console.ReadLine());
            //var position = getPosition(element, sides);
            ////Console.Write(count);
            /*spiral problem level 1*/


            /*check sum problem*/
            var lines = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\checksum.txt").ToList().Select(x => x.Split('\t').ToList().OrderBy(y=> Convert.ToInt32(y)).ToList()).ToList();
            var sum = lines.Aggregate(0, (accumulator, line) => accumulator - Convert.ToInt32(line[0]) + Convert.ToInt32(line[line.Count - 1]));
            var total = 0;
            foreach(var line in lines)
            {
                for(int i=0;i< line.Count - 1; i++)
                {
                    for(int j=i+1; j< line.Count; j++)
                    {
                        if (Convert.ToInt32(line[j]) % Convert.ToInt32(line[i]) == 0)
                        {
                            total += Convert.ToInt32(line[j]) / Convert.ToInt32(line[i]);
                        }
                    }
                }
            }
           // var list = lines.Select()
            /*check sum problem*/
        }

        private static Point getPosition(int num, int sides)
        {
            var x = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(sides) / 2));
            var point = new Point(x, x);
            bool left = true;
            bool right = true;
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
            while(initialCount < num)
            {
                if(curentleftCount < leftCount && nextOperation == "left")
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
            int paths = Math.Abs(point.x - x) + Math.Abs(point.y - x);
            return point;


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
        public Point(int x1, int y1)
        {
            this.x = x1;
            this.y = y1;
        }
        public void left()
        {
            this.x += 1;
        }
        public void up()
        {
            this.y -= 1;
        }
        public void down()
        {
            this.y += 1;
        }
        public void right()
        {
            this.x -= 1;
        }
    }
}
