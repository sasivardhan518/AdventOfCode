using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace hashcode_and_equals
{
    class Program
    {
        static void Main(string[] args)
        {
            Test t1 = new Test(1, 2);
            Test t2 = new Test(1, 2);
            Console.WriteLine(t1.Equals(new Test2()));
            Type test1 = typeof(Test);
            PropertyInfo p =test1.GetProperty("S", BindingFlags.NonPublic|BindingFlags.Public| BindingFlags.Instance);
            p.SetValue(t1, 99);
            string s = t1.ExecCallBack(AddToString);
            List<int> nums = new List<int>();
            nums.Where(Iseven);
        }

        static bool Iseven(int x)
        {
            return x % 2 == 0;
        }

        static string AddToString(int a, int b)
        {
            int sum = a + b;
            return sum.ToString();
        }
    }

    class Test2
    {
        
    }

    class Test : Test2
    {
        private int S { get; set; }
        public int MyProperty1 { get; set; }
        public int MyProperty2 { get; set; }
        public Test(int x, int y)
        {
            this.MyProperty1 = x;
            this.MyProperty2 = y;
            this.S = 0;
        }
        public override bool Equals(object obj)
        {
            if (obj is Test)
            {
                var tmpobj = obj as Test;
                return this.MyProperty1 == tmpobj.MyProperty1 && this.MyProperty2 == tmpobj.MyProperty2;
            }
            else
                return false;
        }
        public override int GetHashCode()
        {
            return this.MyProperty2 + this.MyProperty1;
        }
        
        public string ExecCallBack(Func<int, int, string> func)
        {
            return func(this.MyProperty1, this.MyProperty2);
        }
    }
}
