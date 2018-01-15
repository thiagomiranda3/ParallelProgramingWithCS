using System;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS
{
    public class BasicTask
    {
        public void Write(string c)
        {
            int x = 0;

            while(x++ < 500)
            {
                Console.Write(c);
                x++;
            }
        }

        public void Main()
        {
            Task.Factory.StartNew(() => Write("x"));

            var t = new Task(() => Write("Y"));
            t.Start();

            Write("-");

            Console.ReadKey();
        }
    }
}
