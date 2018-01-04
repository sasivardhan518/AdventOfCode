using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_Of_Code_Day_20
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = System.IO.File.ReadAllLines(@"C:\Users\shashi.dadi\Desktop\Advent of code\Day20.txt").ToList();
            List<List<string>> pva = new List<List<string>>();
            List<Particle> particles = new List<Particle>();
            List<double> distances = new List<double>();
            foreach(var item in input)
            {
                var tempList = item.Split(new string[] { ">," }, StringSplitOptions.None).ToList();
                List<int> positions = Regex.Replace(tempList[0], @"p=<", "").Split(',').Select(x=> Convert.ToInt32(x)).ToList();
                List<int> velocities = Regex.Replace(tempList[1], @"v=<", "").Split(',').Select(x => Convert.ToInt32(x)).ToList(); ;
                List<int> acceleartions = Regex.Replace(tempList[2], @"a=<(?<acc>.*)>", "${acc}").Trim().Split(',').Select(x => Convert.ToInt32(x)).ToList();
                particles.Add(new Particle(positions[0], positions[1],positions[2] , velocities[0], velocities[1], velocities[2] ,  acceleartions[0],acceleartions[1],acceleartions[2] ));
            } 
            for(var i = 0; i < 100; i++)
            {
                
                foreach (var particle in particles)
                {
                    Execute(particle);
                }
                foreach (var particle in particles)
                {
                    if (particles.Where(x => x.position.x == particle.position.x && x.position.y == particle.position.y && x.position.z == particle.position.z).Count() > 1)
                    {
                        particles = particles.Where(x => !(x.position.x == particle.position.x && x.position.y == particle.position.y && x.position.z == particle.position.z)).ToList();
                    }
                }
            }

            var group = distances.GroupBy(x => x);
            foreach (var grp in group)
                Console.WriteLine("{0} : {1}", grp.Key, grp.Count());
            Console.Read();
        }

        private static void Execute(Particle particle)
        {
            particle.velocity.x += particle.acceleration.x;
            particle.velocity.y += particle.acceleration.y;
            particle.velocity.z += particle.acceleration.z;
            particle.position.x += particle.velocity.x;
            particle.position.y += particle.velocity.y;
            particle.position.z += particle.velocity.z;
            particle.distance = Math.Sqrt(Math.Pow(particle.position.x, 2) + Math.Pow(particle.position.y, 2) + Math.Pow(particle.position.z, 2));
        }
    }
    class Particle
    {
        public Coordinate position { get; set; }
        public Coordinate velocity { get; set; }
        public Coordinate acceleration { get; set; }
        public double distance { get; set; }
        public Particle()
        {
            this.position = new Coordinate();
            this.distance= Math.Sqrt(Math.Pow(position.x, 2) + Math.Pow(position.y, 2) + Math.Pow(position.z, 2));
        }
        public Particle(int x, int y , int z, int v1, int v2, int v3, int a1, int a2, int a3)
        {
            this.position = new Coordinate();
            this.velocity = new Coordinate();
            this.acceleration = new Coordinate();
            this.position.x = x;
            this.position.y = y;
            this.position.z = z;
            this.velocity.x = v1;
            this.velocity.y = v2;
            this.velocity.z = v3;
            this.acceleration.x = a1;
            this.acceleration.y = a2;
            this.acceleration.z = a3;
            this.distance =Math.Sqrt(Math.Pow(this.position.x, 2) + Math.Pow(this.position.y, 2) + Math.Pow(this.position.z, 2));
        }
    }

    class Coordinate
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public Coordinate()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
        }
    }
}
