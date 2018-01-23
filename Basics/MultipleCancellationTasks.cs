using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS.Basics
{
    public class MultipleCancellationTasks
    {

        public void Run()
        {
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();
            
            // Mais de um token será adicionado à Task
            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token
            );

            var t = new Task(() =>
            {
                int i = 0;
                while(true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    
                    Console.WriteLine($"{i++}");
                    
                    Thread.Sleep(100);
                }
            }, paranoid.Token);

            t.Start();

            // Waittoken também poderia ser utilizado ao invés do Register
            planned.Token.Register(() => Console.WriteLine("Planned token required!"));
            preventative.Token.Register(() => Console.WriteLine("Planned token required!"));
            emergency.Token.Register(() => Console.WriteLine("Planned token required!"));
            paranoid.Token.Register(() => Console.WriteLine("Paranoid token required!"));

            Console.ReadKey();
            emergency.Cancel();
        }
    }
}