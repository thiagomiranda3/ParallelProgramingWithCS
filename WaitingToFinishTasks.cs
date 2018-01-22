using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS
{
    public class WaitingToFinishTasks
    {
        public void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            
            var t = new Task(() => Thread.Sleep(5000), token);
            t.Start();

            var t2 = Task.Factory.StartNew(() => Thread.Sleep(3000));

            var t3 = Task.Factory.StartNew(() => Thread.Sleep(2000));

            Task.WaitAny(new[] {t, t2}, 1000);
            Console.WriteLine("WaitAny with timespan Finished");
            Console.WriteLine($"t = {t.Status}, t2 = {t2.Status}\n");

            t3.Wait();
            Console.WriteLine("t3 finished\n");

            Task.WaitAny(new[] {t, t2});
            Console.WriteLine("WaitAny Finished");
            Console.WriteLine($"t = {t.Status}, t2 = {t2.Status}\n");

            Task.WaitAll(t, t2);
            Console.WriteLine("WaitAll Finished");
            Console.WriteLine($"t = {t.Status}, t2 = {t2.Status}");
        }
    }
}