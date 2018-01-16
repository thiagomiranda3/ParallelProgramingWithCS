using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelProgramingWithCS
{
    public class BasicCancellationTasks
    {

        public void Run()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            // Serve para chamar um último método depois que o cancelamento do token é requisitado
            token.Register(() => Console.WriteLine("Cancellation Required!"));

            var t = new Task(() =>
            {
                int i = 0;
                while(true)
                {
                    if(token.IsCancellationRequested)
                        throw new OperationCanceledException();
                    else
                        Console.WriteLine($"{i++}");
                }
            }, token);

            t.Start();

            // Outra forma, além do Register(), de monitorar o cancelamento de uma Task
            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Cancellation Token Released!");
            });

            Console.ReadKey();
            cts.Cancel();
        }
    }
}

/*
    Quando lançamos a excessão de operação cancelada, a task sabera que foi cancelada e poderá informar isso caso precise mais tarde.
    Depende do programar querer ou não que a task seja informada do seu cancelamento

    if(token.IsCancellationRequested)
        throw new OperationCanceledException();

    Outra forma de encurtar esse if é chamando direto o:
    token.ThrowIfCancellationRequested();
 */