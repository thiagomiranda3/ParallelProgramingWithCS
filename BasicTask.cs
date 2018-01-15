using System;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS
{
    public class BasicTask
    {
        public static void Write(string c)
        {
            int x = 0;

            while(x++ < 500)
            {
                Console.Write(c);
                x++;
            }
        }

        public static void Main()
        {
            Task.Factory.StartNew(() => Write("x"));

            var t = new Task(() => Write("Y"));
            t.Start();

            Write("-");

            Console.ReadKey();
        }
    }
}
