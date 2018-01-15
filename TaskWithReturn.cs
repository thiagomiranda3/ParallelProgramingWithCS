using System;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS
{
    public class TaskWithReturn
    {
        public int Length(string stringToCount)
        {
            Console.WriteLine($"Running task {Task.CurrentId}");
            var stringSplited = stringToCount.Split();
            
            return stringToCount.Length;
        }
        public void Run()
        {
            string text1 = "mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm ooooooooooooooooooooooooooooooooooo zzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzzz";
            string text2 = "222222222222222222222222222000000000000000000000000ffffffffffffffffffmmmmmmmmmmmmmmmmmmmmm444444444444444444oooooM";

            var t1 = new Task<int>(() => Length(text1));
            t1.Start();

            var t2 = Task.Factory.StartNew(() => Length(text2));

            Console.WriteLine($"Length of text1 is {t1.Result}");
            Console.WriteLine($"Length of text2 is {t2.Result}");

            Console.ReadLine();
        }
    }
}