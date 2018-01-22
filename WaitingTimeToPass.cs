using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS
{
    public class WaitingTimeToPass
    {
        public void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t = new Task(() =>
            {
                Console.WriteLine("Press any key to disarm the bomb. You have 5 seconds!");

                // Vai ficar parado nessa linha durando 5 segundos
                bool cancelled = token.WaitHandle.WaitOne(5000);
                Console.Write(cancelled ? "Bomb disarmed." : "BOOM!");
            }, token);

            t.Start();

            Console.ReadKey();
            cts.Cancel();

            Console.ReadKey();
        }
    }
}